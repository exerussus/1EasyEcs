using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    public abstract class EcsStarter<TPooler> : MonoBehaviour
    {
        [SerializeField] protected float tickSystemDelay = 0.5f;
        protected EcsWorld _world;
        protected TPooler _pooler;
        protected Componenter _componenter;
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _lateUpdateSystems;
        protected IEcsSystems _tickUpdateSystems;
        protected float _tickTimer;
        public GameShare GameShare { get; } = new();
        private bool _isPreInitialized;
        private bool _isInitialized;
        
        public void PreInitialize()
        {
            if (_isPreInitialized) return;
            
            _isPreInitialized = true;
            _world = new EcsWorld();
            _componenter = new Componenter(_world);
            _pooler = GetPooler(_world);
            GameShare.AddSharedObject(_pooler);
            GameShare.AddSharedObject(_componenter);
            GameShare.AddSharedObject(GetSignal());
            
            SetSharingData(_world, GameShare);
            PrepareInitSystems();
            PrepareFixedUpdateSystems();
            PrepareUpdateSystems();
            PrepareLateUpdateSystems();
            PrepareTickUpdateSystems();
            DependencyInject();
        }

        private void DependencyInject()
        {
            InjectSystems(_initSystems);
            InjectSystems(_fixedUpdateSystems, InitializeType.FixedUpdate);
            InjectSystems(_updateSystems, InitializeType.Update);
            InjectSystems(_lateUpdateSystems, InitializeType.Update);
            InjectSystems(_tickUpdateSystems, InitializeType.Tick);
        }
        
        private void InjectSystems(IEcsSystems systems, InitializeType initializeType = InitializeType.None)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem<TPooler> easySystem)
                {
                    easySystem.PreInit(GameShare, tickSystemDelay, _world, initializeType);
                }
            }
        }
        
        public void Initialize()
        {
            if (_isInitialized) return;
            if (!_isPreInitialized) PreInitialize();
            
            _isInitialized = true;
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
            _lateUpdateSystems.Init();
            _tickUpdateSystems.Init();
        }
        
        protected abstract void SetInitSystems(IEcsSystems initSystems);
        protected abstract void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems);
        protected abstract void SetUpdateSystems(IEcsSystems updateSystems);
        protected abstract void SetLateUpdateSystems(IEcsSystems lateUpdateSystems);
        protected abstract void SetTickUpdateSystems(IEcsSystems tickUpdateSystems);
        protected abstract void SetSharingData(EcsWorld world, GameShare gameShare);
        protected abstract Signal GetSignal();
        protected abstract TPooler GetPooler(EcsWorld world);

        private void TryInvokeTick()
        {
            _tickTimer += Time.fixedDeltaTime;
            if (!(_tickTimer >= tickSystemDelay)) return;
            _tickTimer -= tickSystemDelay;
            _tickUpdateSystems?.Run();
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
        }
        
        private void PrepareUpdateSystems()
        {
            _updateSystems = new EcsSystems(_world, GameShare);
            SetUpdateSystems(_updateSystems);
        }
        
        private void PrepareLateUpdateSystems()
        {
            _lateUpdateSystems = new EcsSystems(_world, GameShare);
            SetLateUpdateSystems(_lateUpdateSystems);
        }
        
        private void PrepareTickUpdateSystems()
        {
            _tickUpdateSystems = new EcsSystems(_world, GameShare);
            SetTickUpdateSystems(_tickUpdateSystems);
        }
        
        protected virtual void OnDestroy() 
        {
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _updateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
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

        public void LateUpdate()
        {
            _lateUpdateSystems?.Run();
        }
    }
}