
using System;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EasySystem<TPoolerGroup> : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
        where TPoolerGroup : IGroupPooler
    {
        private bool _isInitialized = false;
        private GameShare _gameShare;
        protected EcsWorld World;
        protected Componenter Componenter;
        protected TPoolerGroup Pooler;
        public string LogPrefix { get; set; }
        public LogLevel CurrentLogLevel { get; set; }
        private Signal _signal;
        protected float TickTime { get; private set; }
        private InitializeType _initializeType;
        private Func<float> _deltaTimeFunc = () => 0;
        protected float DeltaTime => _deltaTimeFunc();
        public Signal Signal => _signal;
        public GameShare GameShare => _gameShare;

        public virtual void PreInit(GameShare gameShare, float tickTime, Func<float> fixedUpdateDeltaFunc, Func<float> updateDeltaFunc, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _gameShare.GetSharedObject(ref Componenter);
            _gameShare.GetSharedObject(ref _signal);
            _gameShare.GetSharedObject(ref Pooler);
            TickTime = tickTime;
            _initializeType = initializeType;
            _deltaTimeFunc = GetCurrentTime(_initializeType, TickTime, fixedUpdateDeltaFunc, updateDeltaFunc);
            _isInitialized = true;
        }

        protected virtual void Log(Custom.LogType logType, string message)
        {
#if UNITY_EDITOR
            if ((LogLevel)logType > CurrentLogLevel) return;
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
        
        private static Func<float> GetCurrentTime(InitializeType initializeType, float tickTime, Func<float> fixedUpdateDeltaFunc, Func<float> updateDeltaFunc)
        {
            switch (initializeType)
            {
                case InitializeType.FixedUpdate:
                    return fixedUpdateDeltaFunc;
                
                case InitializeType.Tick:
                    return () => tickTime;
                
                case InitializeType.Update:
                    return updateDeltaFunc;
                
                default:
                    return () => 0;
            }
        }
        
        public void RegistrySignal<T>(T data) where T : struct
        {
            _signal.RegistryRaise(data);
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
        LateUpdate
    }
}