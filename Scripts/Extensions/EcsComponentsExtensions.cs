using System.Collections.Generic;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Extensions
{
    public static class EcsComponentsExtensions
    {

        public static IEcsSystems AddSystems(this IEcsSystems systems, List<IEcsSystem> addingSystems)
        {
            foreach (var system in addingSystems)
            {
                systems.Add(system);
            }

            return systems;
        }

        public static IEcsSystems AddSystems(this IEcsSystems systems, IEcsSystem[] addingSystems)
        {
            foreach (var system in addingSystems)
            {
                systems.Add(system);
            }

            return systems;
        }
    }
}