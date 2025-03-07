using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace TigerFrogGames
{
    //Based on git-amend video on his improved timers
    //https://www.youtube.com/watch?v=ilvmOQtl57c
    internal static class Bootstrapper
    {
        private static PlayerLoopSystem timerSystem;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimerManager<Update>(ref currentPlayerLoop, 0))
            {
                Debug.LogWarning("Improved Timers not initialized, unable to register TimerManager into the update loop");
                return;
            }
            //tells unity to use our player loop
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            
           // PlayerLoopUtils.PrintPlayerLoop(currentPlayerLoop);
            
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;

            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveTimerManager<Update>(ref currentPlayerLoop);
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);
                    
                    TimerManager.Clear();
                }
            }
#endif
        }

        static bool InsertTimerManager<T>( ref PlayerLoopSystem loop, int index)
        {
            timerSystem = new PlayerLoopSystem()
            {
                type = typeof(TimerManager),
                updateDelegate = TimerManager.UpdateTimers,
                subSystemList = null
            };
            
            return PlayerLoopUtils.InsertSystem<T>(ref loop, in timerSystem, index);
        }

        static void RemoveTimerManager<T>(ref PlayerLoopSystem loop)
        {
            PlayerLoopUtils.RemoveSystem<T>(ref loop, in timerSystem);
        }
        
    }
}