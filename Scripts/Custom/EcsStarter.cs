﻿using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    public abstract class EcsStarter : MonoBehaviour
    {
        [SerializeField] private float tickSystemDelay = 1f;
        protected EcsWorld _world;
        protected Componenter _componenter;
        protected IEcsSystems _coreSystems;
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _tickUpdateSystems;
        protected float _tickTimer;
        public GameShare GameShare { get; } = new();
        private bool _isPreInitialized;
        
        public void PreInitialize()
        {
            if (_isPreInitialized) return;
            
            _world = new EcsWorld();
            _componenter = new Componenter(_world);
            GameShare.AddSharedObject(_componenter);
            GameShare.AddSharedObject(GetSignal());
            
            SetSharingData(GameShare);
            PrepareCoreSystems();
            PrepareInitSystems();
            PrepareFixedUpdateSystems();
            PrepareUpdateSystems();
            PrepareTickUpdateSystems();
            DependencyInject();
        }

        private void DependencyInject()
        {
            InjectSystems(_coreSystems);
            InjectSystems(_initSystems);
            InjectSystems(_fixedUpdateSystems, InitializeType.FixedUpdate);
            InjectSystems(_updateSystems, InitializeType.Update);
            InjectSystems(_tickUpdateSystems, InitializeType.Tick);
        }
        
        private void InjectSystems(IEcsSystems systems, InitializeType initializeType = InitializeType.None)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem easySystem)
                {
                    easySystem.PreInit(GameShare, tickSystemDelay, initializeType);
                }
            }
        }
        
        public void Initialize()
        {
            if (!_isPreInitialized) PreInitialize();
            
            _coreSystems.Init();
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
            _tickUpdateSystems.Init();
        }
        
        protected abstract void SetInitSystems(IEcsSystems initSystems);
        protected abstract void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems);
        protected abstract void SetUpdateSystems(IEcsSystems updateSystems);
        protected abstract void SetTickUpdateSystems(IEcsSystems tickUpdateSystems);
        protected abstract void SetSharingData(GameShare gameShare);
        protected abstract Signal GetSignal();

        private void TryInvokeTick()
        {
            _tickTimer += Time.fixedDeltaTime;
            if (!(_tickTimer >= tickSystemDelay)) return;
            _tickTimer -= tickSystemDelay;
            _tickUpdateSystems?.Run();
        }
        
        private void PrepareCoreSystems()
        {
            _coreSystems = new EcsSystems(_world, GameShare);
        }
        
        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, GameShare);
            SetInitSystems(_initSystems);
        }
        
        private void PrepareFixedUpdateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world, GameShare);
            SetFixedUpdateSystems(_fixedUpdateSystems);
            _fixedUpdateSystems.Add(new DestroySystem());
        }
        
        private void PrepareUpdateSystems()
        {
            _updateSystems = new EcsSystems(_world, GameShare);
            SetUpdateSystems(_updateSystems);
        }
        
        private void PrepareTickUpdateSystems()
        {
            _tickUpdateSystems = new EcsSystems(_world, GameShare);
            SetTickUpdateSystems(_tickUpdateSystems);
        }
        
        protected virtual void OnDestroy() 
        {
            _coreSystems?.Destroy();
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _tickUpdateSystems?.Destroy();
        }

        public void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
            TryInvokeTick();
        }

        public void Update()
        {
            _updateSystems?.Run();
        }
    }
}