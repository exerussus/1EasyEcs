
using System;
using UnityEngine;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1EasyEcs.Scripts.Extensions;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [SelectionBase]
    public abstract class EcsMonoBehavior<TData> : MonoBehaviour, IEcsMonoBehavior where TData : IEcsComponent
    {
        #region SerializedFields
        
        [SerializeField, HideInInspector] private int entity;
        [SerializeField, HideInInspector] private bool isAlive = true;
        [SerializeField, HideInInspector] private bool isInitialized;
        [SerializeField, HideInInspector] private EcsComponent<TData>[] ecsComponents;
        [SerializeField, HideInInspector] private Rigidbody2D rb2D;
        [SerializeField, HideInInspector] private Rigidbody rb3D;
        
        #endregion

        #region Members
        
        public int Entity => entity;
        public bool IsAlive => isAlive;
        public Componenter<TData> Componenter { get; private set; }
        public Signal Signal { get; private set; }
        public event Action onInitialized;

        #endregion

        #region InitAndDestroy

        public void Initialize(Componenter<TData> componenter, Signal signal)
        {
            if (isInitialized) return;
            isInitialized = true;
            isAlive = true;
            Componenter = componenter;
            Signal = signal;
            entity = Componenter.GetNewEntity();
            ref var transformData = ref Componenter.TransformPool.AddOrGet(entity);
            transformData.InitializeValues(transform);
            TryInitializePhysicalBody();
            foreach (var ecsComponent in ecsComponents) ecsComponent.PreInitialize(entity, Componenter);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize();
            ref var ecsMonoBehData = ref Componenter.EcsMonoBehaviourPool.AddOrGet(entity);
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
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
            Signal.RegistryRaise(new OnEcsMonoBehaviorStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Componenter.OnDestroyPool.AddOrGet(entity);
            destroyingData.InitializeValues(gameObject, delay);
        }

        private void TryInitializePhysicalBody()
        {
            if (rb2D != null)
            {
                ref var physicalBodyData = ref Componenter.RigidBody2DPool.AddOrGet(Entity);
                physicalBodyData.Value = rb2D;
            }
            else if (rb3D != null)
            {
                ref var physicalBodyData = ref Componenter.RigidBody3DPool.AddOrGet(Entity);
                physicalBodyData.Value = rb3D;
            }
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
            ecsComponents = GetComponents<EcsComponent<TData>>();
            rb2D = GetComponent<Rigidbody2D>();
            rb3D = GetComponent<Rigidbody>();
        }

#endif

        #endregion
        

        
        public interface IEcsComponentPreInitialize
        {
            public void PreInitialize(int entity, Componenter<TData> componenter);
        }
    
        public interface IEcsComponentInitialize : IEcsComponentPreInitialize
        {
            public void Initialize();
        }
    
        public interface IEcsComponentDestroy : IEcsComponentPreInitialize
        {
            public void Destroy();
        }
    }

    public struct EcsMonoBehaviorData : IEcsComponent
    {
        public IEcsMonoBehavior Value;
        
        public void InitializeValues(IEcsMonoBehavior value)
        {
            Value = value;
        }
    }
    
    public interface IEcsMonoBehavior
    {
        public int Entity { get; }
        public void DestroyEcsMonoBehavior(float delay);
        public Transform transform { get; }
    }
}