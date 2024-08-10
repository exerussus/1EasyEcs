using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Plugins.Exerussus._1EasyEcs.Scripts.Extensions
{
    public static class EcsPoolExtensions
    {
        public static ref TData AddOrGet<TData>(this EcsPool<TData> pool, int entity) where TData : struct, IEcsComponent
        {
            if (!pool.Has(entity)) return ref pool.Add(entity);
            return ref pool.Get(entity);
        }     
    }
}