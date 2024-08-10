
namespace Exerussus._1EasyEcs.Scripts.Core
{
    /// <summary>
    /// Команда на уничтожение Entity 
    /// </summary>
    public struct CommandKillEntitySignal
    {
        public int Entity;
        public bool Immediately;
    }
    
    /// <summary>
    /// MonoBehaviourView проинициализировался.
    /// </summary>
    public struct OnEcsMonoBehaviorInitializedSignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorStartDestroySignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorDestroyedSignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }

}