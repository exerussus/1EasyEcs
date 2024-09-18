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
        [SerializeField] private GameContext gameContext;
        [SerializeField] private GroupContext[] groups;
        
        protected abstract Func<float> FixedUpdateDelta { get; }
        protected abstract Func<float> UpdateDelta { get; }
        protected abstract Signal Signal { get; }
        protected EcsWorld _world;
        protected Componenter _componenter;
        
        private Dictionary<Type, GroupContext> _groupContextsDict;
        private EcsGroup[] _groups;
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
        
        public void PreInitialize(GameContext context)
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
            _groupContextsDict = new();
            
            for (int i = 0; i < _groups.Length; i++)
            {
                var groupContext = new GroupContext();
                _groups[i].PreInitGroup(GetType().Name, groupContext, context, GameShare, _world);
                _groupContextsDict.Add(_groups[i].GetType(), _groups[i].GroupContext);
            }

            groups = _groups.Select(group => group.GroupContext).ToArray();
        }

        private void Start()
        {
            if (autoInitialize) Initialize(gameContext);
        }
        
        public void Initialize(GameContext context)
        {
            if (_isInitialized) return;
            gameContext = context;
            if (!_isPreInitialized) PreInitialize(gameContext);
            
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
            gameContext.FixedUpdateDelta = Time.fixedDeltaTime;
            for (int i = 0; i < _groups.Length; i++) _groups[i].FixedUpdate();
        }

        public void Update()
        {
            if (!_isInitialized) return;
            gameContext.UpdateDelta = Time.deltaTime;
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