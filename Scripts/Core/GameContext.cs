using System;

namespace Exerussus._1EasyEcs.Scripts.Core
{
    [Serializable]
    public class GameContext
    {
        public bool IsPaused = false;
        public float GameTimeScale = 1f;
    }
}