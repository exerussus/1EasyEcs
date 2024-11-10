using Leopotam.EcsLite;

namespace Plugins.Exerussus._1EasyEcs.Scripts.Extensions
{
    public static class EcsWorldExtensions
    {
        public static EcsPackedEntity NewPackedEntity(this EcsWorld world)
        {
            return world.PackEntity(world.NewEntity());
        }
    }
}