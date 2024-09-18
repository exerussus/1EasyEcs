using System;
using Exerussus._1EasyEcs.Scripts.Custom;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Serializable]
    public class GameContext
    {
        public bool IsPaused = false;
        public float GameTimeScale = 1f;
        public LogLevel LogLevel = LogLevel.Trace;
        public float FixedUpdateDelta;
        public float UpdateDelta;
    }
}