using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class TimerManager
    {
        private static readonly List<Timer> timers = new();

        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void DeRegisterTimer(Timer timer) => timers.Remove(timer);

        public static int NumberOfListeners()
        {
            return timers.Count;
        }
        
        public static void UpdateTimers()
        {
            foreach (var timer in new List<Timer>(timers))
            {
                timer.Tick();
            }
        }

        public static void Clear() => timers.Clear();

        public static object Contains(FrequencyTimer onHitTimer)
        {
            return timers.Contains(onHitTimer);
        }
    }
}