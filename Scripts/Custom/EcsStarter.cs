using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    public abstract class EcsStarter : MonoBehaviour
    {
        [SerializeField] private bool autoInitialize = true;
        [SerializeField] private EcsGroupStarter[] groups;
        private EcsGroup[] _groups;
        protected abstract Func<float> FixedUpdateDelta { get; }
        protected abstract Func<float> UpdateDelta { get; }
        protected abstract Signal Signal { get; }
        
        protected EcsWorld _world;
        protected Componenter _componenter;
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _lateUpdateSystems;
        protected IEcsSystems _tickUpdateSystems;
        protected float _tickTimer;
        public GameShare GameShare { get; } = new();
        private bool _isPreInitialized;
        private bool _isInitialized;
        public virtual string Name { get; private set; }
        public virtual LogLevel LogLevel => LogLevel.Trace;

        public EcsGroupStarter[] Groups => groups;

        public void PreInitialize()
        {
            if (_isPreInitialized) return;
            
            Name = GetType().Name;
            _isPreInitialized = true;
            _world = new EcsWorld();
            _componenter = new Componenter(_world);
            GameShare.AddSharedObject(_componenter);
            GameShare.AddSharedObject(Signal);
            
            SetSharingData(_world, GameShare);
            
            _groups = GetGroups();
            if (_groups is EcsGroupStarter[] ecsGroup) groups = ecsGroup;
            for (int i = 0; i < _groups.Length; i++) _groups[i].PreInitGroup(GetType().Name, GameShare, FixedUpdateDelta, UpdateDelta, _world, LogLevel);
        }

        private void Start()
        {
            if (autoInitialize) Initialize();
        }

        public void Initialize()
        {
            if (_isInitialized) return;
            if (!_isPreInitialized) PreInitialize();
            
            _isInitialized = true;
            
            for (int i = 0; i < _groups.Length; i++) _groups[i].InitializeGroup();
        }
        
        protected abstract EcsGroup[] GetGroups();
        protected abstract void SetSharingData(EcsWorld world, GameShare gameShare);
        
        protected virtual void OnDestroy() 
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _groups.Length; i++) _groups[i].OnDestroy();
        }

        public void FixedUpdate()
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _groups.Length; i++) _groups[i].FixedUpdate();
        }

        public void Update()
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _groups.Length; i++) _groups[i].Update();
        }

        public void LateUpdate()
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _groups.Length; i++) _groups[i].LateUpdate();
        }
    }

    public enum LogLevel
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Trace = 4
    }

    public enum LogType
    {
        Error = 1,
        Warning = 2,
        Info = 3,
        Trace = 4
    }
}