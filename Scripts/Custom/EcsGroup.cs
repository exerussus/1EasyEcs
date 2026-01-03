using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Exerussus.GameSharing.Runtime;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    [Serializable]
    public abstract class EcsGroup
    {
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _lateUpdateSystems;
        protected GameShare GameShare;
        protected Signal Signal;
        protected EcsWorld World;
        
        public bool HasAnyInitSystems { get; protected set; }
        public bool HasAnyFixedUpdatesSystems { get; protected set; }
        public bool HasAnyUpdatesSystems { get; protected set; }
        public bool HasAnyLateUpdatesSystems { get; protected set; }
        public IEcsSystems InitSystems => _initSystems;
        public IEcsSystems FixedUpdateSystems => _fixedUpdateSystems;
        public IEcsSystems UpdateSystems => _updateSystems;
        public IEcsSystems LateUpdateSystems => _lateUpdateSystems;
        public GameContext GameContext { get; private set; }
        public abstract IGroupPooler[] Poolers { get; }

        
        protected virtual void SetInitSystems(IEcsSystems initSystems) {}
        protected virtual void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems) { HasAnyFixedUpdatesSystems = false; }
        protected virtual void SetUpdateSystems(IEcsSystems updateSystems) { HasAnyUpdatesSystems = false; }
        protected virtual void SetLateUpdateSystems(IEcsSystems lateUpdateSystems) { HasAnyLateUpdatesSystems = false; }
        
        
        public void PreInitComponents(string starterName, GameContext gameContext, GameShare gameShare, EcsWorld world)
        {
            GameContext = gameContext;
            GameShare = gameShare;
            World = world;
            gameShare.GetSharedObject(ref Signal);
            if (Poolers is { Length: > 0 }) foreach (var pooler in Poolers) GameShare.AddSharedObject(pooler.GetType(), pooler);
            SetSharingData(World, GameShare);
        }

        public void InjectPooler()
        {
            if (Poolers is { Length: > 0 }) foreach (var pooler in Poolers) SharedObjectInjector.InjectSharedObjects(pooler, GameShare);
        }

        public void PreInitGroup()
        {
            if (Poolers is { Length: > 0 }) foreach (var pooler in Poolers)
            {
                OnBeforePoolInitializing(World, pooler);
                pooler.BeforeInitialize(World, GameShare, GameContext);
                pooler.Initialize(World);
            }
            
            _initSystems = new EcsSystems(World, GameShare);
            SetInitSystems(_initSystems);
            
            _fixedUpdateSystems = new EcsSystems(World, GameShare);
            SetFixedUpdateSystems(_fixedUpdateSystems);
            
            _updateSystems = new EcsSystems(World, GameShare);
            SetUpdateSystems(_updateSystems);
            
            _lateUpdateSystems = new EcsSystems(World, GameShare);
            SetLateUpdateSystems(_lateUpdateSystems);
            
            HasAnyInitSystems = InjectSystems(_initSystems);
            HasAnyFixedUpdatesSystems = InjectSystems(_fixedUpdateSystems);
            HasAnyUpdatesSystems = InjectSystems(_updateSystems);
            HasAnyLateUpdatesSystems = InjectSystems(_lateUpdateSystems);
        }
        
        public void InitializeGroup()
        {
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
            _lateUpdateSystems.Init();
        }
        
        private bool InjectSystems(IEcsSystems systems)
        {
            var hasAnySystem = false;
            
            foreach (var system in systems.GetAllSystems())
            {
                hasAnySystem = true;
                if (system is EasySystem easySystem)
                {
                    GameShare.InjectSharedObjects(easySystem);
                    easySystem.PreInit(GameShare, GameContext, World);
                }
            }

            return hasAnySystem;
        }

        public List<IEcsSystem> GetAllSystems()
        {
            var systems = new List<IEcsSystem>();
            if (HasAnyInitSystems) systems.AddRange(_initSystems.GetAllSystems());
            if (HasAnyFixedUpdatesSystems) systems.AddRange(_fixedUpdateSystems.GetAllSystems());
            if (HasAnyUpdatesSystems) systems.AddRange(_updateSystems.GetAllSystems());
            if (HasAnyLateUpdatesSystems) systems.AddRange(_lateUpdateSystems.GetAllSystems());
            return systems;
        }
        
        protected virtual void SetSharingData(EcsWorld world, GameShare gameShare) { }
        
        public void OnDestroy() 
        {
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _updateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
        }
        
        public void FixedUpdate() => _fixedUpdateSystems?.Run();
        public void Update() => _updateSystems?.Run();
        public void LateUpdate() => _lateUpdateSystems?.Run();

        protected virtual void OnBeforePoolInitializing(EcsWorld world, IGroupPooler pooler) { }
    }

    public interface IGroupPooler
    {
        public virtual void BeforeInitialize(EcsWorld world, GameShare gameShare, GameContext gameContext) {}
        public virtual void Initialize(EcsWorld world) {}
    } 
}