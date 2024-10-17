using System;
using System.Collections.Generic;
using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    public abstract class EcsStarter : MonoBehaviour
    {
        [SerializeField] private bool autoInitialize = true;
        [SerializeField] private GroupContext[] groups;
        
        protected abstract Func<float> FixedUpdateDelta { get; }
        protected abstract Func<float> UpdateDelta { get; }
        protected abstract Signal Signal { get; }
        protected EcsWorld _world;
        protected Componenter _componenter;
        
        private GameContext _gameContext;
        private Dictionary<Type, GroupContext> _groupContextsDict;
        private EcsGroup[] _allGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _fixedUpdatesGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _updatesGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _lateUpdatesGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _tickUpdatesGroups = Array.Empty<EcsGroup>();
        private bool _isPreInitialized;
        private bool _isInitialized;
        
        public GameShare GameShare { get; } = new();
        public virtual string Name { get; private set; }
        public GroupContext[] Groups => groups;

        public bool TryGetGroupContext(Type groupType, out GroupContext groupContext)
        {
            if (_groupContextsDict.TryGetValue(groupType, out groupContext)) return true;
            return false;
        }
        
        public GroupContext GetGroupContext(Type groupType)
        {
            return _groupContextsDict[groupType];
        }

        protected abstract GameContext GetGameContext(GameShare gameShare); 
        
        public void PreInitialize()
        {
            if (_isPreInitialized) return;
            
            _gameContext = GetGameContext(GameShare);
            Name = GetType().Name;
            _isPreInitialized = true;
            _world = new EcsWorld();
            _componenter = new Componenter(_world);
            GameShare.AddSharedObject(_world);
            GameShare.AddSharedObject(_componenter);
            GameShare.AddSharedObject(Signal);
            GameShare.AddSharedObject(_gameContext);
            GameShare.AddSharedObject(_gameContext.GetType(), _gameContext);
            
            SetSharingData(_world, GameShare);
            
            _allGroups = GetGroups();
            _groupContextsDict = new();
            
            for (int i = 0; i < _allGroups.Length; i++)
            {
                var groupContext = new GroupContext();
                _allGroups[i].PreInitComponents(GetType().Name, groupContext, _gameContext, GameShare, _world);
                _groupContextsDict.Add(_allGroups[i].GetType(), _allGroups[i].GroupContext);
            }
            
            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].PreInitGroup();

            groups = _allGroups.Select(group => group.GroupContext).ToArray();
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
            
            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].InitializeGroup();
            
            _fixedUpdatesGroups = _allGroups.Where(ecsGroup => ecsGroup.FixedUpdateSystems.GetAllSystems().Count > 0).ToArray();
            _updatesGroups = _allGroups.Where(ecsGroup => ecsGroup.UpdateSystems.GetAllSystems().Count > 0).ToArray();
            _lateUpdatesGroups = _allGroups.Where(ecsGroup => ecsGroup.LateUpdateSystems.GetAllSystems().Count > 0).ToArray();
            _tickUpdatesGroups = _allGroups.Where(ecsGroup => ecsGroup.TickUpdateSystems.GetAllSystems().Count > 0).ToArray();
        }
        
        protected abstract EcsGroup[] GetGroups();
        protected abstract void SetSharingData(EcsWorld world, GameShare gameShare);
        
        protected virtual void OnDestroy() 
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].OnDestroy();
        }

        public void FixedUpdate()
        {
            if (!_isInitialized) return;
            _gameContext.FixedUpdateDelta = Time.fixedDeltaTime;
            for (int i = 0; i < _fixedUpdatesGroups.Length; i++) _fixedUpdatesGroups[i].FixedUpdate();
            for (int i = 0; i < _tickUpdatesGroups.Length; i++) _tickUpdatesGroups[i].TickUpdate();
        }

        public void Update()
        {
            if (!_isInitialized) return;
            _gameContext.UpdateDelta = Time.deltaTime;
            for (int i = 0; i < _updatesGroups.Length; i++) _updatesGroups[i].Update();
        }

        public void LateUpdate()
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _lateUpdatesGroups.Length; i++) _lateUpdatesGroups[i].LateUpdate();
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