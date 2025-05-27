using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public static  class EcsFilterExtensions
    {
        public static void ForeachEntities(this EcsFilter filter, Action<int> action)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity);
            }
        }
        
        public static void ForeachEntities(this EcsFilter filter, Action<int, bool> action, bool boolValue)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity, boolValue);
            }
        }
        
        public static void ForeachEntities(this EcsFilter filter, Action<int, string, bool> action, string stringValue, bool boolValue)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity, stringValue, boolValue);
            }
        }
        
        public static int GetFirstEntity(this EcsFilter filter)
        {
            foreach (var entity in filter)
            {
                return entity;
            }

            throw new Exception("Пустой фильтр.");
        }
        
        public static int GetRandomEntity(this EcsFilter filter)
        {
            var randomValue = UnityEngine.Random.Range(0, filter.GetEntitiesCount());
            var count = 0;
            foreach (var entity in filter)
            {
                if (count == randomValue) return entity;
                count++;
            }

            throw new Exception("Пустой фильтр.");
        }

        public static bool HasAny(this EcsFilter filter)
        {
            foreach (var _ in filter)
            {
                return true;
            }

            return false;
        }

        public static bool TryGetFirstEntity(this EcsFilter filter, out int entity)
        {
            foreach (var e in filter)
            {
                entity = e;
                return true;
            }

            entity = default;
            return false;
        }
        
        public static void Foreach(this EcsFilter filter, Action<int> action)
        {
            foreach (var entity in filter)
            {
                action.Invoke(entity);
            }
        }

        public static void Foreach(this EcsFilter filter, Action action)
        {
            foreach (var _ in filter)
            {
                action.Invoke();
            }
        }
    }
}
