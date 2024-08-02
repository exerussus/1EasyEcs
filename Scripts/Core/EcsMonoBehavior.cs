
using System;
using UnityEngine;
using Exerussus._1Extensions.SignalSystem;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [SelectionBase]
    public abstract class EcsMonoBehavior : MonoBehaviour, IEcsMonoBehavior
    {
        #region SerializedFields
        
        [SerializeField] private int entity;
        [SerializeField] private bool isAlive = true;
        [SerializeField] private bool isInitialized;
        [SerializeField] private int components;
        [SerializeField, HideInInspector] private EcsComponent[] ecsComponents;
        
        private bool _isReused;
        
        #endregion

        #region Members
        
        public int Entity => entity;
        public bool IsAlive => isAlive;
        public Componenter Componenter { get; private set; }
        public Signal Signal { get; private set; }
        public event Action onInitialized;

        #endregion

        #region InitAndDestroy

        public void Initialize(Componenter componenter, Signal signal)
        {
            isInitialized = true;
            isAlive = true;
            Componenter = componenter;
            Signal = signal;
            entity = Componenter.GetNewEntity();
            ref var transformData = ref Componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            foreach (var ecsComponent in ecsComponents) ecsComponent.PreInitialize(entity, Componenter);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize();
            ref var ecsMonoBehData = ref Componenter.Add<EcsMonoBehaviorData>(entity);
            ecsMonoBehData.InitializeValues(this);
            Signal.RegistryRaise(new OnEcsMonoBehaviorInitializedSignal { EcsMonoBehavior = this });
            onInitialized?.Invoke();
            onInitialized = null;
        }
        
        public void DestroyEcsMonoBehavior(float delay)
        {
            if (!IsAlive) return;
            isAlive = false;
            isInitialized = false;
            _isReused = true;
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
            Signal.RegistryRaise(new OnEcsMonoBehaviorStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Componenter.AddOrGet<OnDestroyData>(entity);
            destroyingData.InitializeValues(gameObject, delay);
        }

        #endregion

        #region Methods

        public void SwitchActivated()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        #endregion
        
        #region Editor

#if UNITY_EDITOR
        private void OnValidate()
        {
            ecsComponents = GetComponents<EcsComponent>();
            components = ecsComponents.Length;
        }
#endif
        
        #endregion
    }

    public interface IEcsComponentPreInitialize
    {
        public void PreInitialize(int entity, Componenter componenter);
    }
    
    public interface IEcsComponentInitialize : IEcsComponentPreInitialize
    {
        public void Initialize();
    }
    
    public interface IEcsComponentDestroy : IEcsComponentPreInitialize
    {
        public void Destroy();
    }

    public struct EcsMonoBehaviorData : IEcsData<EcsMonoBehavior>
    {
        public EcsMonoBehavior Value;
        
        public void InitializeValues(EcsMonoBehavior value)
        {
            Value = value;
        }
    }

    public interface IEcsMonoBehavior
    {
        
    }
}