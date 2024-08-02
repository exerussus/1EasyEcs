﻿
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EcsSignalListener<T1> : EasySystem 
        where T1 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            SubscribeSignal<T1>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
    }
    
    public abstract class EcsSignalListener<T1, T2> : EasySystem 
        where T1 : struct
        where T2 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
    }
    
    public abstract class EcsSignalListener<T1, T2, T3> : EasySystem 
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
    }
    
    public abstract class EcsSignalListener<T1, T2, T3, T4> : EasySystem 
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
    }
    
}