
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EcsSignalListener<TPooler, T1> : EasySystem<TPooler>
        where T1 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
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
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
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
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
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
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
            SubscribeSignal<T7>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
            UnsubscribeSignal<T7>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
            SubscribeSignal<T7>(OnSignal);
            SubscribeSignal<T8>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
            UnsubscribeSignal<T7>(OnSignal);
            UnsubscribeSignal<T8>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
        protected abstract void OnSignal(T8 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8, T9> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
        where T9 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
            SubscribeSignal<T7>(OnSignal);
            SubscribeSignal<T8>(OnSignal);
            SubscribeSignal<T9>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
            UnsubscribeSignal<T7>(OnSignal);
            UnsubscribeSignal<T8>(OnSignal);
            UnsubscribeSignal<T9>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
        protected abstract void OnSignal(T8 data);
        protected abstract void OnSignal(T9 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
        where T9 : struct
        where T10 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
            SubscribeSignal<T7>(OnSignal);
            SubscribeSignal<T8>(OnSignal);
            SubscribeSignal<T9>(OnSignal);
            SubscribeSignal<T10>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
            UnsubscribeSignal<T7>(OnSignal);
            UnsubscribeSignal<T8>(OnSignal);
            UnsubscribeSignal<T9>(OnSignal);
            UnsubscribeSignal<T10>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
        protected abstract void OnSignal(T8 data);
        protected abstract void OnSignal(T9 data);
        protected abstract void OnSignal(T10 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
        where T9 : struct
        where T10 : struct
        where T11 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
            SubscribeSignal<T7>(OnSignal);
            SubscribeSignal<T8>(OnSignal);
            SubscribeSignal<T9>(OnSignal);
            SubscribeSignal<T10>(OnSignal);
            SubscribeSignal<T11>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
            UnsubscribeSignal<T7>(OnSignal);
            UnsubscribeSignal<T8>(OnSignal);
            UnsubscribeSignal<T9>(OnSignal);
            UnsubscribeSignal<T10>(OnSignal);
            UnsubscribeSignal<T11>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
        protected abstract void OnSignal(T8 data);
        protected abstract void OnSignal(T9 data);
        protected abstract void OnSignal(T10 data);
        protected abstract void OnSignal(T11 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : EasySystem<TPooler>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
        where T9 : struct
        where T10 : struct
        where T11 : struct
        where T12 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
            SubscribeSignal<T4>(OnSignal);
            SubscribeSignal<T5>(OnSignal);
            SubscribeSignal<T6>(OnSignal);
            SubscribeSignal<T7>(OnSignal);
            SubscribeSignal<T8>(OnSignal);
            SubscribeSignal<T9>(OnSignal);
            SubscribeSignal<T10>(OnSignal);
            SubscribeSignal<T11>(OnSignal);
            SubscribeSignal<T12>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
            UnsubscribeSignal<T4>(OnSignal);
            UnsubscribeSignal<T5>(OnSignal);
            UnsubscribeSignal<T6>(OnSignal);
            UnsubscribeSignal<T7>(OnSignal);
            UnsubscribeSignal<T8>(OnSignal);
            UnsubscribeSignal<T9>(OnSignal);
            UnsubscribeSignal<T10>(OnSignal);
            UnsubscribeSignal<T11>(OnSignal);
            UnsubscribeSignal<T12>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
        protected abstract void OnSignal(T8 data);
        protected abstract void OnSignal(T9 data);
        protected abstract void OnSignal(T10 data);
        protected abstract void OnSignal(T11 data);
        protected abstract void OnSignal(T12 data);
    }
    
}