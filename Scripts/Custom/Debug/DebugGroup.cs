using Leopotam.EcsLite;

namespace Exerussus._1EasyEcs.Scripts.Custom
{
    public class DebugGroup : EcsGroup
    {
        public override IGroupPooler[] Poolers { get; } = null;

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
#if UNITY_EDITOR
            fixedUpdateSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
        }
    }
}