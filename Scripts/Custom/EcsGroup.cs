using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    [Serializable]
    public class EcsGroupStarter
    {
        
    }
    
    [Serializable]
    public abstract class EcsGroup : EcsGroupStarter
    {
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _lateUpdateSystems;

        public IEcsSystems InitSystems => _initSystems;
        public IEcsSystems FixedUpdateSystems => _fixedUpdateSystems;
        public IEcsSystems UpdateSystems => _updateSystems;
        public IEcsSystems LateUpdateSystems => _lateUpdateSystems;
        
        public bool HasAnyInitSystems { get; protected set; }
        public bool HasAnyFixedUpdatesSystems { get; protected set; }
        public bool HasAnyUpdatesSystems { get; protected set; }
        public bool HasAnyLateUpdatesSystems { get; protected set; }
        
        protected virtual void SetInitSystems(IEcsSystems initSystems) {}

        protected virtual void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems) { HasAnyFixedUpdatesSystems = false; }
        protected virtual void SetUpdateSystems(IEcsSystems updateSystems) { HasAnyUpdatesSystems = false; }
        protected virtual void SetLateUpdateSystems(IEcsSystems lateUpdateSystems) { HasAnyLateUpdatesSystems = false; }

        public abstract void PreInitComponents(string starterName, GameContext gameContext, GameShare gameShare, EcsWorld world);
        public abstract void InjectPooler();
        public abstract void PreInitGroup();
        public abstract void InitializeGroup();
        public abstract void OnDestroy();
        public abstract void FixedUpdate();
        public abstract void Update();
        public abstract void LateUpdate();
        public abstract List<IEcsSystem> GetAllSystems();
        public abstract object GetPooler();
    }
    
    [Serializable]
    public abstract class EcsGroup<TPoolerGroup> : EcsGroup where TPoolerGroup : IGroupPooler, new()
    {
        protected GameShare GameShare;
        protected Signal Signal;
        protected EcsWorld World;

        protected TPoolerGroup Pooler { get; private set; }
        protected GameContext GameContext { get; private set; }

        public override void PreInitComponents(string starterName, GameContext gameContext, GameShare gameShare, EcsWorld world)
        {
            GameContext = gameContext;
            GameShare = gameShare;
            Pooler = GetPoolerGroup();
            World = world;
            gameShare.GetSharedObject(ref Signal);
            GameShare.AddSharedObject(typeof(TPoolerGroup), Pooler);
            GameShare.AddSharedObject(Pooler.GetType(), Pooler);
            SetSharingData(World, GameShare);
        }

        public override void InjectPooler()
        {
            SharedObjectInjector.InjectSharedObjects(Pooler, GameShare);
        }

        public override void PreInitGroup()
        {
            OnBeforePoolInitializing(World, Pooler);
            Pooler.BeforeInitialize(World, GameShare, GameContext);
            
            Pooler.Initialize(World);
            
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
        
        public override void InitializeGroup()
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
                if (system is EasySystem<TPoolerGroup> easySystem)
                {
                    GameShare.InjectSharedObjects(easySystem);
                    easySystem.PreInit(GameShare, GameContext, World);
                }
            }

            return hasAnySystem;
        }

        public override List<IEcsSystem> GetAllSystems()
        {
            var systems = new List<IEcsSystem>();
            if (HasAnyInitSystems) systems.AddRange(_initSystems.GetAllSystems());
            if (HasAnyFixedUpdatesSystems) systems.AddRange(_fixedUpdateSystems.GetAllSystems());
            if (HasAnyUpdatesSystems) systems.AddRange(_updateSystems.GetAllSystems());
            if (HasAnyLateUpdatesSystems) systems.AddRange(_lateUpdateSystems.GetAllSystems());
            return systems;
        }

        public override object GetPooler() => Pooler;
        
        protected virtual void SetSharingData(EcsWorld world, GameShare gameShare) { }
        
        public override void OnDestroy() 
        {
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _updateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
        }
        
        public override void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        public override void Update()
        {
            _updateSystems?.Run();
        }

        public override void LateUpdate()
        {
            _lateUpdateSystems?.Run();
        }

        protected virtual void OnBeforePoolInitializing(EcsWorld world, TPoolerGroup pooler) { }
        protected virtual TPoolerGroup GetPoolerGroup() { return new(); }
    }

    public interface IGroupPooler
    {
        public virtual void BeforeInitialize(EcsWorld world, GameShare gameShare, GameContext gameContext) {}
        public virtual void Initialize(EcsWorld world) {}
    } 
}