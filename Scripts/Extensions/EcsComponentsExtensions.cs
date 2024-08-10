using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Extensions
{
    public static class EcsComponentsExtensions
    {
        public static Vector2 GetVector2Position<TData>(this Componenter<TData> componenter, int entity) where TData : IEcsComponent
        {
            ref var transformData = ref componenter.Get<TransformData>(entity);
            return transformData.Value.position;
        }

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