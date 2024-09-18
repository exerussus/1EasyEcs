using System;
using Exerussus._1EasyEcs.Scripts.Core;
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
        protected virtual void SetInitSystems(IEcsSystems initSystems) {}
        protected virtual void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems) {}
        protected virtual void SetUpdateSystems(IEcsSystems updateSystems) {}
        protected virtual void SetLateUpdateSystems(IEcsSystems lateUpdateSystems) {}
        protected virtual void SetTickUpdateSystems(IEcsSystems tickUpdateSystems) {}

        public abstract void PreInitGroup(string starterName, GroupContext groupContext, GameContext gameContext,
            GameShare gameShare, EcsWorld world, LogLevel logLevel);
        public abstract void InitializeGroup();
        public abstract void OnDestroy();
        public abstract void FixedUpdate();
        public abstract void Update();
        public abstract void LateUpdate();
    }
    
    [Serializable]
    public abstract class EcsGroup<TPoolerGroup> : EcsGroup where TPoolerGroup : IGroupPooler, new()
    {
        protected virtual float TickSystemDelay { get; } = 1f;
        protected float TickTimer;
        protected GameShare GameShare;
        protected EcsWorld World;
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _lateUpdateSystems;
        protected IEcsSystems _tickUpdateSystems;
        protected string GroupName { get; private set; }
        protected LogLevel LogLevel { get; private set; }
        protected TPoolerGroup Pooler { get; private set; }
        protected GameContext GameContext { get; private set; }

        public override void PreInitGroup(string starterName, GroupContext groupContext, GameContext gameContext,
            GameShare gameShare, EcsWorld world, LogLevel logLevel)
        {
            GameContext = gameContext;
            GroupContext = groupContext;
            GroupContext.TickDelta = TickSystemDelay;
            GameShare = gameShare;
            World = world;
            GroupName = GetType().Name;
            GroupContext.Name = GroupName;
            LogLevel = logLevel;
            Pooler = new();
            Pooler.Initialize(World);
            GameShare.AddSharedObject(Pooler);

            SetSharingData(World, GameShare);
            
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
            
            InjectSystems(starterName, _initSystems);
            InjectSystems(starterName, _fixedUpdateSystems, InitializeType.FixedUpdate);
            InjectSystems(starterName, _updateSystems, InitializeType.Update);
            InjectSystems(starterName, _lateUpdateSystems, InitializeType.Update);
            InjectSystems(starterName, _tickUpdateSystems, InitializeType.Tick);
            
        }
        
        public override void InitializeGroup()
        {
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
            _lateUpdateSystems.Init();
            _tickUpdateSystems.Init();
        }
        
        private void InjectSystems(string starterName, IEcsSystems systems, InitializeType initializeType = InitializeType.None)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem<TPoolerGroup> easySystem)
                {
                    easySystem.LogPrefix = $"{starterName} | {GroupName} | {easySystem.GetType().Name} |";
                    easySystem.CurrentLogLevel = LogLevel;
                    easySystem.PreInit(GameShare, GameContext, GroupContext, World, initializeType);
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

        private void TryInvokeTick(float deltaTime)
        {
            TickTimer += deltaTime;
            if (!(TickTimer >= TickSystemDelay)) return;
            TickTimer -= TickSystemDelay;
            _tickUpdateSystems?.Run();
        }
        
        public override void FixedUpdate()
        {
            if (!GroupContext.IsEnabled) return;
            _fixedUpdateSystems?.Run();
            TryInvokeTick(GroupContext.FixedUpdateDelta);
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
    }

    public interface IGroupPooler
    {
        public abstract void Initialize(EcsWorld world);
    } 
}