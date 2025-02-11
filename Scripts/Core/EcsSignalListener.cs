
using System;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    public abstract class EcsSignalListener<TPooler, T1> : EasySystem<TPooler> where TPooler : IGroupPooler
        where T1 : struct
    {
        private Action<T1> _signalSubscribeT1;
        
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
        }
        
        protected abstract void OnSignal(T1 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2> : EasySystem<TPooler> where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3> : EasySystem<TPooler> where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4> : EasySystem<TPooler> where TPooler : IGroupPooler
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        private Action<T1> _signalSubscribeT1;
        private Action<T2> _signalSubscribeT2;
        private Action<T3> _signalSubscribeT3;
        private Action<T4> _signalSubscribeT4;
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT4 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
            SubscribeSignal(_signalSubscribeT4);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
            UnsubscribeSignal(_signalSubscribeT4);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5> : EasySystem<TPooler> where TPooler : IGroupPooler
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
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT4 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT5 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
            SubscribeSignal(_signalSubscribeT4);
            SubscribeSignal(_signalSubscribeT5);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
            UnsubscribeSignal(_signalSubscribeT4);
            UnsubscribeSignal(_signalSubscribeT5);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6> : EasySystem<TPooler> where TPooler : IGroupPooler
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
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT4 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT5 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT6 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
            SubscribeSignal(_signalSubscribeT4);
            SubscribeSignal(_signalSubscribeT5);
            SubscribeSignal(_signalSubscribeT6);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
            UnsubscribeSignal(_signalSubscribeT4);
            UnsubscribeSignal(_signalSubscribeT5);
            UnsubscribeSignal(_signalSubscribeT6);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7> : EasySystem<TPooler> where TPooler : IGroupPooler
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
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT4 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT5 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT6 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT7 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
            SubscribeSignal(_signalSubscribeT4);
            SubscribeSignal(_signalSubscribeT5);
            SubscribeSignal(_signalSubscribeT6);
            SubscribeSignal(_signalSubscribeT7);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
            UnsubscribeSignal(_signalSubscribeT4);
            UnsubscribeSignal(_signalSubscribeT5);
            UnsubscribeSignal(_signalSubscribeT6);
            UnsubscribeSignal(_signalSubscribeT7);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
    }
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8> : EasySystem<TPooler> where TPooler : IGroupPooler
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
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT4 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT5 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT6 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT7 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT8 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
            SubscribeSignal(_signalSubscribeT4);
            SubscribeSignal(_signalSubscribeT5);
            SubscribeSignal(_signalSubscribeT6);
            SubscribeSignal(_signalSubscribeT7);
            SubscribeSignal(_signalSubscribeT8);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
            UnsubscribeSignal(_signalSubscribeT4);
            UnsubscribeSignal(_signalSubscribeT5);
            UnsubscribeSignal(_signalSubscribeT6);
            UnsubscribeSignal(_signalSubscribeT7);
            UnsubscribeSignal(_signalSubscribeT8);
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
    
    public abstract class EcsSignalListener<TPooler, T1, T2, T3, T4, T5, T6, T7, T8, T9> : EasySystem<TPooler> where TPooler : IGroupPooler
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
        
        public override void PreInit(GameShare gameShare, GameContext gameContext, GroupContext groupContext, EcsWorld world)
        {
            base.PreInit(gameShare, gameContext, groupContext, world);
            
            _signalSubscribeT1 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT2 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT3 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT4 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT5 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT6 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT7 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT8 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            _signalSubscribeT9 = data => { if (GroupContext.IsEnabled) OnSignal(data); };
            
            SubscribeSignal(_signalSubscribeT1);
            SubscribeSignal(_signalSubscribeT2);
            SubscribeSignal(_signalSubscribeT3);
            SubscribeSignal(_signalSubscribeT4);
            SubscribeSignal(_signalSubscribeT5);
            SubscribeSignal(_signalSubscribeT6);
            SubscribeSignal(_signalSubscribeT7);
            SubscribeSignal(_signalSubscribeT8);
            SubscribeSignal(_signalSubscribeT9);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal(_signalSubscribeT1);
            UnsubscribeSignal(_signalSubscribeT2);
            UnsubscribeSignal(_signalSubscribeT3);
            UnsubscribeSignal(_signalSubscribeT4);
            UnsubscribeSignal(_signalSubscribeT5);
            UnsubscribeSignal(_signalSubscribeT6);
            UnsubscribeSignal(_signalSubscribeT7);
            UnsubscribeSignal(_signalSubscribeT8);
            UnsubscribeSignal(_signalSubscribeT9);
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
}