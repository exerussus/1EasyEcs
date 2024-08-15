using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;


namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Serializable]
    public class GameShare
    {
        [SerializeField] private List<string> sharedEcsSystems = new ();
        private Dictionary<Type, DataPack> _sharedObjects = new();
        
        public T GetSharedObject<T>()
        {
            var classPack = _sharedObjects[typeof(T)];
            var sharedObject = classPack.Object;
            return (T)sharedObject;
        }

        public void AddSharedObject<T>(Type type, T sharedObject)
        {
            sharedEcsSystems.Add(type.Name);
            _sharedObjects[type] = new DataPack(type, sharedObject);
        }

        public void AddSharedObject<T>(T sharedObject)
        {
            var type = sharedObject.GetType();
            sharedEcsSystems.Add(type.Name);
            _sharedObjects[type] = new DataPack(type, sharedObject);
        }
    }
    
    [Serializable]
    public class DataPack
    {
        public DataPack(Type type, object sharedObject)
        {
            _object = sharedObject;
            _type = type;
            name = _type.Name;
        }

        [SerializeField] private string name;
        private Type _type;
        private object _object;
        
        public string Name => name; 
        public object Object => _object;
    }
}