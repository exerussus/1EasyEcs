﻿
using System;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EasySystem<TPoolerGroup> : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
        where TPoolerGroup : IGroupPooler
    {
        private bool _isInitialized = false;
        protected GroupContext GroupContext { get; private set; }
        protected GameContext GameContext { get; private set; }
        private GameShare _gameShare;
        protected EcsWorld World { get; private set; }
        protected Componenter Componenter;
        protected TPoolerGroup Pooler;
        public string LogPrefix { get; set; }
        private Signal _signal;
        private InitializeType _initializeType;
        private Func<float> _deltaTimeFunc = () => 0;
        protected float DeltaTime => GetCurrentTime(_initializeType, GroupContext, GameContext);
        public Signal Signal => _signal;
        public GameShare GameShare => _gameShare;

        public virtual void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext,
            EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            if (_isInitialized) return;
            World = world;
            GameContext = gameContext;
            GroupContext = groupContext;
            _gameShare = gameShare;
            _gameShare.GetSharedObject(ref Componenter);
            _gameShare.GetSharedObject(ref _signal);
            Pooler = _gameShare.GetSharedObject<TPoolerGroup>();

            _initializeType = initializeType;
            _isInitialized = true;
        }

        protected virtual void Log(Custom.LogType logType, string message)
        {
#if UNITY_EDITOR
            if ((LogLevel)logType > GameContext.LogLevel) return;
            switch (logType)
            {
                case Custom.LogType.Error:
                    Debug.LogError($"{LogPrefix} Error | {message}");
                    break;
                case Custom.LogType.Warning:
                    Debug.LogWarning($"{LogPrefix} Warning | {message}");
                    break;
                case Custom.LogType.Info:
                    Debug.Log($"{LogPrefix} Info | {message}");
                    break;
                case Custom.LogType.Trace:
                    Debug.Log($"{LogPrefix} Trace | {message}");
                    break;
            }
#endif
        }
        
        private static float GetCurrentTime(InitializeType initializeType, GroupContext groupContext, GameContext gameContext)
        {
            switch (initializeType)
            {
                case InitializeType.FixedUpdate:
                    return gameContext.FixedUpdateDelta;
                
                case InitializeType.Tick:
                    return groupContext.TickDelta;
                
                case InitializeType.Update:
                    return gameContext.UpdateDelta;
                
                default:
                    return 0;
            }
        }
        
        public void RegistrySignal<T>(T data) where T : struct
        {
            _signal.RegistryRaise(data);
        }
        
        public void RegistrySignal<T>(ref T data) where T : struct
        {
            _signal.RegistryRaise(ref data);
        }

        public void SubscribeSignal<T>(Action<T> action) where T : struct
        {
            _signal.Subscribe(action);
        }

        public void UnsubscribeSignal<T>(Action<T> action) where T : struct
        {
            _signal?.Unsubscribe(action);
        }

        public void Init(IEcsSystems systems)
        {
            World = systems.GetWorld();
            Initialize();
        }

        public void Run(IEcsSystems systems)
        {
            Update();
        }

        protected virtual void Initialize() {}
        protected virtual void Update() {}

        public virtual void Destroy(IEcsSystems systems)
        {
            
        }
    }
    
    public enum InitializeType
    {
        None,
        Start,
        FixedUpdate,
        Tick,
        Update
    }

    public enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate,
        TickUpdate
    }
}