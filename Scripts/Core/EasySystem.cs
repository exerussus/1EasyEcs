
using System;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EasySystem<TPooler> : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private bool _isInitialized = false;
        private GameShare _gameShare;
        protected EcsWorld World;
        protected Componenter Componenter;
        protected TPooler Pooler;
        private Signal _signal;
        private float _deltaTime;
        protected float TickTime { get; private set; }
        private InitializeType _initializeType;

        protected float DeltaTime => _deltaTime;
        public Signal Signal => _signal;
        public GameShare GameShare => _gameShare;

        public virtual void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _gameShare.GetSharedObject(ref Componenter);
            _gameShare.GetSharedObject(ref _signal);
            _gameShare.GetSharedObject(ref Pooler);
            TickTime = tickTime;
            _initializeType = initializeType;
            _deltaTime = GetCurrentTime();
            _isInitialized = true;
        }
        
        private float GetCurrentTime()
        {
            switch (_initializeType)
            {
                case InitializeType.None:
                    return 0;
                
                case InitializeType.FixedUpdate:
                    return Time.fixedDeltaTime;
                
                case InitializeType.Tick:
                    return TickTime;
                
                case InitializeType.Update:
                    return Time.deltaTime;
                
                default:
                    throw new ArgumentOutOfRangeException();
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
}