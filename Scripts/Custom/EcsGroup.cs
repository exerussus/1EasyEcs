using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    [Serializable]
    public class EcsGroupStarter
    {
        [SerializeField] public GroupContext GroupContext;
    }
    
    [Serializable]
    public abstract class EcsGroup : EcsGroupStarter
    {
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _lateUpdateSystems;
        protected IEcsSystems _tickUpdateSystems;

        public IEcsSystems InitSystems => _initSystems;
        public IEcsSystems FixedUpdateSystems => _fixedUpdateSystems;
        public IEcsSystems UpdateSystems => _updateSystems;
        public IEcsSystems LateUpdateSystems => _lateUpdateSystems;
        public IEcsSystems TickUpdateSystems => _tickUpdateSystems;
        public bool HasFixedUpdates { get; private set; } = true;
        public bool HasUpdates { get; private set; } = true;
        public bool HasLateUpdates { get; private set; } = true;
        public bool HasTickUpdates { get; private set; } = true;
        
        protected virtual void SetInitSystems(IEcsSystems initSystems) {}

        protected virtual void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems) { HasFixedUpdates = false; }
        protected virtual void SetUpdateSystems(IEcsSystems updateSystems) { HasUpdates = false; }
        protected virtual void SetLateUpdateSystems(IEcsSystems lateUpdateSystems) { HasLateUpdates = false; }
        protected virtual void SetTickUpdateSystems(IEcsSystems tickUpdateSystems) { HasTickUpdates = false; }

        public abstract void PreInitComponents(string starterName, GroupContext groupContext, GameContext gameContext, GameShare gameShare, EcsWorld world);
        public abstract void InjectPooler();
        public abstract void PreInitGroup();
        public abstract void InitializeGroup();
        public abstract void OnDestroy();
        public abstract void FixedUpdate();
        public abstract void Update();
        public abstract void LateUpdate();
        public abstract void TickUpdate();
    }
    
    [Serializable]
    public abstract class EcsGroup<TPoolerGroup> : EcsGroup where TPoolerGroup : IGroupPooler, new()
    {
        protected virtual float TickSystemDelay { get; } = 1f;
        protected float TickTimer;
        protected GameShare GameShare;
        protected Signal Signal;
        protected EcsWorld World;

        protected string GroupName { get; private set; }
        protected LogLevel LogLevel { get; private set; }
        protected TPoolerGroup Pooler { get; private set; }
        protected GameContext GameContext { get; private set; }

        public override void PreInitComponents(string starterName, GroupContext groupContext, GameContext gameContext, GameShare gameShare, EcsWorld world)
        {
            GameContext = gameContext;
            GroupContext = groupContext;
            GameShare = gameShare;
            Pooler = GetPoolerGroup();
            GroupContext.TickDelta = TickSystemDelay;
            World = world;
            gameShare.GetSharedObject(ref Signal);
            GroupName = GetType().Name;
            GroupContext.Name = GroupName;
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
            Pooler.BeforeInitialize(World, GameShare, GameContext, GroupName);
            
            Pooler.Initialize(World);
            
            _initSystems = new EcsSystems(World, GameShare);
            SetInitSystems(_initSystems);
            
            _fixedUpdateSystems = new EcsSystems(World, GameShare);
            SetFixedUpdateSystems(_fixedUpdateSystems);
            
            _updateSystems = new EcsSystems(World, GameShare);
            SetUpdateSystems(_updateSystems);
            
            _lateUpdateSystems = new EcsSystems(World, GameShare);
            SetLateUpdateSystems(_lateUpdateSystems);
            
            _tickUpdateSystems = new EcsSystems(World, GameShare);
            SetTickUpdateSystems(_tickUpdateSystems);
            
            InjectSystems(GroupName, _initSystems);
            InjectSystems(GroupName, _fixedUpdateSystems);
            InjectSystems(GroupName, _updateSystems);
            InjectSystems(GroupName, _lateUpdateSystems);
            InjectSystems(GroupName, _tickUpdateSystems);
        }
        
        public override void InitializeGroup()
        {
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
            _lateUpdateSystems.Init();
            _tickUpdateSystems.Init();
        }

        private void InjectPoolers()
        {
            
        }
        
        private void InjectSystems(string starterName, IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem<TPoolerGroup> easySystem)
                {
                    easySystem.LogPrefix = $"{starterName} | {GroupName} | {easySystem.GetType().Name} |";
                    GameShare.InjectSharedObjects(easySystem);
                    easySystem.PreInit(GameShare, GameContext, GroupContext, World);
                }
            }
        }

        protected virtual void SetSharingData(EcsWorld world, GameShare gameShare) { }
        
        public override void OnDestroy() 
        {
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _updateSystems?.Destroy();
            _lateUpdateSystems?.Destroy();
            _tickUpdateSystems?.Destroy();
        }
        
        public override void FixedUpdate()
        {
            if (!GroupContext.IsEnabled) return;
            _fixedUpdateSystems?.Run();
        }

        public override void Update()
        {
            if (!GroupContext.IsEnabled) return;
            _updateSystems?.Run();
        }

        public override void LateUpdate()
        {
            if (!GroupContext.IsEnabled) return;
            _lateUpdateSystems?.Run();
        }

        public override void TickUpdate()
        {
            TickTimer += GameContext.FixedUpdateDelta;
            if (!(TickTimer >= TickSystemDelay)) return;
            TickTimer -= TickSystemDelay;
            _tickUpdateSystems?.Run();
        }

        protected virtual void OnBeforePoolInitializing(EcsWorld world, TPoolerGroup pooler) { }
        protected virtual TPoolerGroup GetPoolerGroup() { return new(); }
    }

    public interface IGroupPooler
    {
        public virtual void BeforeInitialize(EcsWorld world, GameShare gameShare, GameContext gameContext, string groupName) {}
        public virtual void Initialize(EcsWorld world) {}
    } 
}