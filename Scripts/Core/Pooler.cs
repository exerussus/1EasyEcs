using System;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public interface IPoolerModule<T> where T : struct, IEcsComponent
    {
        public ref T AddOrGet(int entity);
        public ref T Add(int entity);
        public ref T Get(int entity);
        public bool Has(int entity);
        public void Del(int entity);
    }
    
    public class PoolerModuleCustom<T> : IPoolerModule<T> where T : struct, IEcsComponent
    {
        public PoolerModuleCustom(EcsWorld world, Action<int, EcsPool<T>> onAdd)
        {
            _pool = world.GetPool<T>();
            _onAdd = onAdd;
        }

        private readonly EcsPool<T> _pool;
        private readonly Action<int, EcsPool<T>> _onAdd;

        public ref T AddOrGet(int entity)
        {
            if (_pool.Has(entity)) return ref _pool.Get(entity);
            return ref Add(entity);
        }

        public ref T Add(int entity)
        {
            ref var newData = ref _pool.Add(entity);
            _onAdd.Invoke(entity, _pool);
            return ref newData;
        }

        public ref T Get(int entity)
        {
            return ref _pool.Get(entity);
        }

        public bool Has(int entity)
        {
            return _pool.Has(entity);
        }

        public void Del(int entity)
        {
            _pool.Del(entity);
        }
    }
    
    public class PoolerModule<T> : IPoolerModule<T> where T : struct, IEcsComponent
    {
        public PoolerModule(EcsWorld world)
        {
            _pool = world.GetPool<T>();
        }

        private readonly EcsPool<T> _pool;

        public ref T AddOrGet(int entity)
        {
            if (_pool.Has(entity)) return ref _pool.Get(entity);
            return ref _pool.Add(entity);
        }

        public ref T Add(int entity)
        {
            return ref _pool.Add(entity);
        }

        public ref T Get(int entity)
        {
            return ref _pool.Get(entity);
        }

        public bool Has(int entity)
        {
            return _pool.Has(entity);
        }

        public void Del(int entity)
        {
            _pool.Del(entity);
        }
    }
}