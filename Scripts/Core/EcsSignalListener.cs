
using System;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus.GameSharing.Runtime;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EcsSignalListener<TPooler, T1> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
    {
        private Action<T1> _signalSubscribeT1;
        
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            OnDestroy();
        }

        public virtual void OnDestroy() { } 
        
        protected abstract void OnSignal(T1 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            _signalSubscribeT4 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
            Signal.Subscribe(_signalSubscribeT4);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            Signal?.Unsubscribe(_signalSubscribeT4);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        private Action<T5> _signalSubscribeT5;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            _signalSubscribeT4 = OnSignal;
            _signalSubscribeT5 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
            Signal.Subscribe(_signalSubscribeT4);
            Signal.Subscribe(_signalSubscribeT5);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            Signal?.Unsubscribe(_signalSubscribeT4);
            Signal?.Unsubscribe(_signalSubscribeT5);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        private Action<T5> _signalSubscribeT5;
        private Action<T6> _signalSubscribeT6;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            _signalSubscribeT4 = OnSignal;
            _signalSubscribeT5 = OnSignal;
            _signalSubscribeT6 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
            Signal.Subscribe(_signalSubscribeT4);
            Signal.Subscribe(_signalSubscribeT5);
            Signal.Subscribe(_signalSubscribeT6);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            Signal?.Unsubscribe(_signalSubscribeT4);
            Signal?.Unsubscribe(_signalSubscribeT5);
            Signal?.Unsubscribe(_signalSubscribeT6);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        private Action<T5> _signalSubscribeT5;
        private Action<T6> _signalSubscribeT6;
        private Action<T7> _signalSubscribeT7;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            _signalSubscribeT4 = OnSignal;
            _signalSubscribeT5 = OnSignal;
            _signalSubscribeT6 = OnSignal;
            _signalSubscribeT7 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
            Signal.Subscribe(_signalSubscribeT4);
            Signal.Subscribe(_signalSubscribeT5);
            Signal.Subscribe(_signalSubscribeT6);
            Signal.Subscribe(_signalSubscribeT7);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            Signal?.Unsubscribe(_signalSubscribeT4);
            Signal?.Unsubscribe(_signalSubscribeT5);
            Signal?.Unsubscribe(_signalSubscribeT6);
            Signal?.Unsubscribe(_signalSubscribeT7);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        private Action<T5> _signalSubscribeT5;
        private Action<T6> _signalSubscribeT6;
        private Action<T7> _signalSubscribeT7;
        private Action<T8> _signalSubscribeT8;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            _signalSubscribeT4 = OnSignal;
            _signalSubscribeT5 = OnSignal;
            _signalSubscribeT6 = OnSignal;
            _signalSubscribeT7 = OnSignal;
            _signalSubscribeT8 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
            Signal.Subscribe(_signalSubscribeT4);
            Signal.Subscribe(_signalSubscribeT5);
            Signal.Subscribe(_signalSubscribeT6);
            Signal.Subscribe(_signalSubscribeT7);
            Signal.Subscribe(_signalSubscribeT8);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            Signal?.Unsubscribe(_signalSubscribeT4);
            Signal?.Unsubscribe(_signalSubscribeT5);
            Signal?.Unsubscribe(_signalSubscribeT6);
            Signal?.Unsubscribe(_signalSubscribeT7);
            Signal?.Unsubscribe(_signalSubscribeT8);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
        protected abstract void OnSignal(T8 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8, T9> : EasySystem<TPooler>, IEcsDestroySystem where TPooler : IGroupPooler
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
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        private Action<T5> _signalSubscribeT5;
        private Action<T6> _signalSubscribeT6;
        private Action<T7> _signalSubscribeT7;
        private Action<T8> _signalSubscribeT8;
        private Action<T9> _signalSubscribeT9;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, world);
            
            _signalSubscribeT1 = OnSignal;
            _signalSubscribeT2 = OnSignal;
            _signalSubscribeT3 = OnSignal;
            _signalSubscribeT4 = OnSignal;
            _signalSubscribeT5 = OnSignal;
            _signalSubscribeT6 = OnSignal;
            _signalSubscribeT7 = OnSignal;
            _signalSubscribeT8 = OnSignal;
            _signalSubscribeT9 = OnSignal;
            
            Signal.Subscribe(_signalSubscribeT1);
            Signal.Subscribe(_signalSubscribeT2);
            Signal.Subscribe(_signalSubscribeT3);
            Signal.Subscribe(_signalSubscribeT4);
            Signal.Subscribe(_signalSubscribeT5);
            Signal.Subscribe(_signalSubscribeT6);
            Signal.Subscribe(_signalSubscribeT7);
            Signal.Subscribe(_signalSubscribeT8);
            Signal.Subscribe(_signalSubscribeT9);
        }

        public void Destroy(IEcsSystems systems)
        {
            Signal?.Unsubscribe(_signalSubscribeT1);
            Signal?.Unsubscribe(_signalSubscribeT2);
            Signal?.Unsubscribe(_signalSubscribeT3);
            Signal?.Unsubscribe(_signalSubscribeT4);
            Signal?.Unsubscribe(_signalSubscribeT5);
            Signal?.Unsubscribe(_signalSubscribeT6);
            Signal?.Unsubscribe(_signalSubscribeT7);
            Signal?.Unsubscribe(_signalSubscribeT8);
            Signal?.Unsubscribe(_signalSubscribeT9);
            OnDestroy();
        }
        
        public virtual void OnDestroy() { } 
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
}