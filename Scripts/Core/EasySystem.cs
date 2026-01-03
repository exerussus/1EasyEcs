
using Exerussus._1Extensions.SignalSystem;
using Exerussus.GameSharing.Runtime;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EasySystem : IEcsInitSystem, IEcsRunSystem
    {
        private bool _isInitialized = false;
        protected EcsWorld World { get; private set; }
        protected GameContext GameContext { get; private set; }
        private GameShare _gameShare;
        private Signal _signal;
        public float DeltaTime { get; private set; }
        public float Time { get; private set; }
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
            _gameShare.GetSharedObject(ref _signal);

            _isInitialized = true;
        }

        public void Init(IEcsSystems systems)
        {
            Initialize();
        }

        public void Run(IEcsSystems systems)
        {
            Time = UnityEngine.Time.time;
            
            if (NextUpdateTime > Time) return;
            NextUpdateTime = Time + UpdateDelay;
            DeltaTime = Time - LastUpdateTime;
            Update();
            LastUpdateTime = Time;
        }

        protected virtual void Initialize() {}
        protected virtual void Update() {}
    }
}