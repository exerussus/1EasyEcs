
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
        protected GameContext GameContext { get; private set; }
        private GameShare _gameShare;
        protected EcsWorld World { get; private set; }
        protected Componenter Componenter;
        protected TPoolerGroup Pooler;
        private Signal _signal;
        public float DeltaTime { get; private set; }
        public Signal Signal => _signal;
        public GameShare GameShare => _gameShare;
        public virtual float UpdateDelay { get; set; }
        protected float NextUpdateTime;
        protected float LastUpdateTime;

        public virtual void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            if (_isInitialized) return;
            World = world;
            GameContext = gameContext;
            _gameShare = gameShare;
            _gameShare.GetSharedObject(ref Componenter);
            _gameShare.GetSharedObject(ref _signal);
            Pooler = _gameShare.GetSharedObject<TPoolerGroup>();

            _isInitialized = true;
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
            if (NextUpdateTime > Time.time) return;
            NextUpdateTime = UpdateDelay + Time.time;
            DeltaTime = Time.time - LastUpdateTime;
            Update();
            LastUpdateTime = Time.time;
        }

        protected virtual void Initialize() {}
        protected virtual void Update() {}

        public virtual void Destroy(IEcsSystems systems)
        {
            
        }
    }

    public enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate,
        TickUpdate
    }
}