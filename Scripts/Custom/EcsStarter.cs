using System;
using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Exerussus.GameSharing.Runtime;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    public abstract class EcsStarter : MonoBehaviour
    {
        [SerializeField] private bool autoInitialize = false;
        public abstract Signal Signal { get; }
        protected EcsWorld _world;
        protected Componenter _componenter;
        
        private GameContext _gameContext;
        private EcsGroup[] _allGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _fixedUpdatesGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _updatesGroups = Array.Empty<EcsGroup>();
        private EcsGroup[] _lateUpdatesGroups = Array.Empty<EcsGroup>();
        private bool _isPreInitialized;
        private bool _isInitialized;
        
        public virtual GameShare GameShare { get; } = new();
        public EcsGroup[] AllGroups => _allGroups;

        protected abstract GameContext GetGameContext(GameShare gameShare); 
        
        public void PreInitialize()
        {
            if (_isPreInitialized) return;
            _isPreInitialized = true;
            
            _gameContext = GetGameContext(GameShare);
            _world = new EcsWorld();
            _componenter = new Componenter(_world);
            GameShare.AddSharedObject(_world);
            GameShare.AddSharedObject(_componenter);
            GameShare.AddSharedObject(Signal);
            GameShare.AddSharedObject(_gameContext);
            GameShare.AddSharedObject(_gameContext.GetType(), _gameContext);
            
            SetSharingDataOnStart(_world, GameShare);
            
            _allGroups = CreateGroups();
            
            for (int i = 0; i < _allGroups.Length; i++)
            {
                _allGroups[i].PreInitComponents(GetType().Name, _gameContext, GameShare, _world);
            }

            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].InjectPooler();

            SetSharingDataBeforePreInitialized(_world, GameShare);
            
            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].PreInitGroup();

            OnPreInitialize();
        }

        protected virtual void Start()
        {
            if (autoInitialize) Initialize();
        }
        
        public void Initialize()
        {
            if (_isInitialized) return;
            if (!_isPreInitialized) PreInitialize();
            
            _isInitialized = true;
            
            SetSharingDataBeforeInitialized(_world, GameShare);
            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].InitializeGroup();
            
            _fixedUpdatesGroups = _allGroups.Where(ecsGroup => ecsGroup.FixedUpdateSystems.GetAllSystems().Count > 0).ToArray();
            _updatesGroups = _allGroups.Where(ecsGroup => ecsGroup.UpdateSystems.GetAllSystems().Count > 0).ToArray();
            _lateUpdatesGroups = _allGroups.Where(ecsGroup => ecsGroup.LateUpdateSystems.GetAllSystems().Count > 0).ToArray();
            
            OnInitialize();
            
            SetSharingDataAfterInitialized(_world, GameShare);
        }
        
        protected abstract EcsGroup[] CreateGroups();
        
        /// <summary> Срабатывает после создания групп, пуллеров и систем и их пре-инициализации, но до инициализации. </summary>
        public virtual void OnPreInitialize() {}
        /// <summary> Срабатывает после инициализации всех групп, пуллеров и систем. </summary>
        public virtual void OnInitialize() { }
        
        /// <summary> Срабатывает до начала создания групп, пуллеров и систем. Полезно для прокидывания данных для групп и пуллеров до их появления. </summary>
        protected abstract void SetSharingDataOnStart(EcsWorld world, GameShare gameShare);
        
        /// <summary> Срабатывает после создания групп, пуллеров и систем, но до их пре-инициализации. </summary>
        protected virtual void SetSharingDataBeforePreInitialized(EcsWorld world, GameShare gameShare) {}
        
        /// <summary> Срабатывает после создания групп, пуллеров и систем и их пре-инициализации, но до инициализации. </summary>
        protected virtual void SetSharingDataBeforeInitialized(EcsWorld world, GameShare gameShare) {}
        /// <summary> Срабатывает после инициализации всех групп, пуллеров и систем. Самая крайняя точка инициализации. </summary>
        protected virtual void SetSharingDataAfterInitialized(EcsWorld world, GameShare gameShare) {}
        
        protected virtual void OnDestroy() 
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _allGroups.Length; i++) _allGroups[i].OnDestroy();
        }

        public virtual void FixedUpdate()
        {
            if (!_isInitialized) return;
            _gameContext.FixedUpdateDelta = Time.fixedDeltaTime;
            for (int i = 0; i < _fixedUpdatesGroups.Length; i++) _fixedUpdatesGroups[i].FixedUpdate();
        }

        public virtual void Update()
        {
            if (!_isInitialized) return;
            _gameContext.Time = Time.time;
            _gameContext.UpdateDelta = Time.deltaTime;
            for (int i = 0; i < _updatesGroups.Length; i++) _updatesGroups[i].Update();
        }

        public virtual void LateUpdate()
        {
            if (!_isInitialized) return;
            for (int i = 0; i < _lateUpdatesGroups.Length; i++) _lateUpdatesGroups[i].LateUpdate();
        }
    }
}