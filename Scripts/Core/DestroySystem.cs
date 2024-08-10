using System;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Preserve]
    public class DestroySystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private bool _isInitialized = false;
        private GameShare _gameShare;
        protected EcsWorld World;
        private Signal _signal;
        private float _deltaTime;
        protected float DeltaTime => _deltaTime;
        protected float TickTime { get; private set; }
        private InitializeType _initializeType;
        public GameShare GameShare => _gameShare;
        private bool _toDestroy = true;
        private EcsFilter _destroyingFilter;
        private const float DestroyDelay = 1f;
        private EcsPool<EcsMonoBehaviorData> _monoBehPool;
        private EcsPool<OnDestroyData> _destroyPool;
        
        public void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            _signal = _gameShare.GetSharedObject<Signal>();
            TickTime = tickTime;
            _initializeType = initializeType;
            _deltaTime = GetCurrentTime();
            _isInitialized = true;
            _signal.Subscribe<CommandKillEntitySignal>(OnSignal);
        }

        public void Destroy(IEcsSystems systems)
        {
            _signal.Unsubscribe<CommandKillEntitySignal>(OnSignal);
        }
        
        private void OnSignal(CommandKillEntitySignal data)
        {
            var delay = data.Immediately ? 0.05f : DestroyDelay;
            _monoBehPool.Get(data.Entity).Value.DestroyEcsMonoBehavior(delay);
        }

        public void Init(IEcsSystems systems)
        {
            World = systems.GetWorld();
            _monoBehPool = World.GetPool<EcsMonoBehaviorData>();
            _destroyPool = World.GetPool<OnDestroyData>();
            _destroyingFilter = World.Filter<OnDestroyData>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _destroyingFilter)
            {
                ref var onDestroyData = ref _destroyPool.Get(entity);
                onDestroyData.TimeRemaining -= DeltaTime;
                
                if (onDestroyData.TimeRemaining <= 0)
                {
                    if (onDestroyData.ObjectToDelete != null)
                    {
                        if (!_toDestroy) onDestroyData.ObjectToDelete.gameObject.SetActive(false);
                        else Object.Destroy(onDestroyData.ObjectToDelete.gameObject);

                        if (_monoBehPool.Has(entity))
                        {
                            ref var ecsMonoBehaviorData = ref _monoBehPool.Get(entity);
                            _signal.RegistryRaise(new OnEcsMonoBehaviorDestroyedSignal {EcsMonoBehavior = ecsMonoBehaviorData.Value});
                        }
                    }
                    World.DelEntity(entity);
                }
            }
        }
        
        [Serializable]
        public class Settings
        {
            public float entityDestroyDelay = 1.15f;
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
    }
}