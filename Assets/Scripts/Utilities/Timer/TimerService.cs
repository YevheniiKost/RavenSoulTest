using System;

namespace RavenSoul.Utilities.Timer
{
    public static class TimerService
    {
        public static ITimer CreateTimer(float duration, TimerDirection direction, Action<float> onUpdate, Action onTimerEnd, float speed = 1)
        {
            return new Utilities.Timer.Timer(duration, direction, onUpdate, onTimerEnd, speed);
        }

        public static ITimer CreateTimer(float duration, TimerDirection direction, float speed = 1)
        {
            return new Utilities.Timer.Timer(duration, direction, null, null, speed);
        }
    }
}