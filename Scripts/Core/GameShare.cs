using System;
using System.Collections.Generic;
using UnityEngine;


namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Serializable]
    public class GameShare
    {
        public GameShare(Dictionary<Type, DataPack> sharedObjects)
        {
            _sharedObjects = sharedObjects;
        }

        [SerializeField] private List<string> sharedEcsSystems = new ();
        private Dictionary<Type, DataPack> _sharedObjects;
        
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