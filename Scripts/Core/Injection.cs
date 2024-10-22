using System;
using Exerussus._1EasyEcs.Scripts.Custom;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class InjectSharedObjectAttribute : Attribute
    {
    }
    
    public static class SharedObjectInjector
    {
        public static void InjectSharedObjects<T>(this EasySystem<T> easySystem, GameShare gameShare) where T : IGroupPooler
        {
            InjectSharedObjects(easySystem as object, gameShare);
        }
        
        public static void InjectSharedObjects(object target, GameShare gameShare)
        {
            var targetType = target.GetType();
            var fields = targetType.GetFields(
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Instance);
            var properties = targetType.GetProperties(
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Public | 
                System.Reflection.BindingFlags.Instance);
            
            var method = typeof(GameShare).GetMethod("GetSharedObject", new Type[] { });
        
            foreach (var field in fields)
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(InjectSharedObjectAttribute)) as InjectSharedObjectAttribute;
                if (attribute != null)
                {
                    var fieldType = field.FieldType;
                    var genericMethod = method.MakeGenericMethod(fieldType);
                    var sharedObject = genericMethod.Invoke(gameShare, null);
                    field.SetValue(target, sharedObject);
                }
            }

            foreach (var property in properties)
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(InjectSharedObjectAttribute)) as InjectSharedObjectAttribute;
                if (attribute != null && property.CanWrite)
                {
                    var propertyType = property.PropertyType;
                    var genericMethod = method.MakeGenericMethod(propertyType);
                    var sharedObject = genericMethod.Invoke(gameShare, null);
                    property.SetValue(target, sharedObject);
                }
            }
        }
    }
}
