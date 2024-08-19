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
    
    public class PoolerModule<T> : IPoolerModule<T> where T : struct, IEcsComponent
    {
        public PoolerModule(EcsWorld world)
        {
            _world = world;
            _pool = world.GetPool<T>();
        }

        private EcsWorld _world;
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