
using Exerussus._1Extensions.SignalSystem;
using UnityEngine;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [RequireComponent(typeof(IEcsMonoBehavior))]
    public abstract class EcsComponent : MonoSignalListener
    {
        private int _entity;
        private Componenter _componenter;

        public int Entity => _entity;
        public Componenter Componenter => _componenter;
        
        public void PreInitialize(int entity, Componenter componenter)
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