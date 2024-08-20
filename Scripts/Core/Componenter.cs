using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public class Componenter
    {
        private readonly EcsWorld _world;
        private readonly Dictionary<Type, IEcsPool> _pools;

        public EcsWorld World => _world;

        public Componenter(EcsWorld world)
        {
            _world = world;
            _pools = new Dictionary<Type, IEcsPool>();
        }

        public int GetNewEntity()
        {
            return _world.NewEntity();
        }

        public void DelEntity(int entity)
        {
            _world.DelEntity(entity);
        }

        public EcsWorld.Mask Filter<T>() where T : struct, IEcsComponent
        {
            return _world.Filter<T>();
        }
    }
}