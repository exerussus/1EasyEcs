
using Exerussus._1Extensions.SignalSystem;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [RequireComponent(typeof(IEcsMonoBehavior))]
    public abstract class EcsComponent<TData> : MonoSignalListener where TData : IEcsComponent
    {
        private int _entity;
        private Componenter<TData> _componenter;

        public int Entity => _entity;
        public Componenter<TData> Componenter => _componenter;
        
        public void PreInitialize(int entity, Componenter<TData> componenter)
        {
            _entity = entity;
            _componenter = componenter;
        }
        
        public virtual void Initialize()
        {
            
        }

        public virtual void Destroy()
        {
            
        }

        protected override void OnValidate()
        {
            base.OnValidate();
        }
    }
}