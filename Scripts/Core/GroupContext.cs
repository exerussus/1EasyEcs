using System;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Serializable]
    public class GroupContext
    {
        public string Name;
        public bool IsEnabled = true;
        public float TickDelta;
    }
}