using System;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Serializable]
    public class GroupContext
    {
        public string Name;
        public bool IsEnabled = true;
        public bool IsPaused = false;
        public float FixedUpdateDelta;
        public float UpdateDelta;
        public float TickDelta;
    }
}