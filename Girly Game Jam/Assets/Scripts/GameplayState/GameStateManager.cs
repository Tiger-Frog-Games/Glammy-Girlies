using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TigerFrogGames
{

    public enum GameState
    {
        Gameplay,
        Paused,
        Cinematic
    }
    
    public class GameStateManager 
    {
        private static GameStateManager _instance;
        public static GameStateManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        
        private GameStateManager()
        {
            pausedSources = new Dictionary<string, int>();
        }
        
        public GameState CurrentGameState { get; private set; }
        public delegate void GameStateChangeHandler(GameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;

        private Dictionary<string, int> pausedSources;

        public void ForceState(GameState newGameState)
        {
            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }
        
        public void SetState( string source,  GameState newGameState )
        {
            //if (CurrentGameState == newGameState){ return; }

            if (newGameState == GameState.Paused)
            {
                pausedSources.TryAdd(source, 0);
                
                pausedSources[source]++;

                if (CurrentGameState == GameState.Gameplay)
                {
                    CurrentGameState = GameState.Paused;
                    OnGameStateChanged?.Invoke(newGameState);
                }
                return;
            }

            if (newGameState == GameState.Gameplay)
            {
                if (pausedSources != null)
                {
                    pausedSources[source]--;

                    if (pausedSources[source] < 0)
                    {
                        pausedSources[source] = 0;
                    }

                    foreach (var dictionaryKey in GameStateManager.Instance.pausedSources)
                    {
                        if (dictionaryKey.Value != 0)
                        {
                            return;
                        }
                    }
                }

                
                if (CurrentGameState != GameState.Paused) return;
                
                //all sources of pause are removed
                CurrentGameState = GameState.Gameplay;
                OnGameStateChanged?.Invoke(GameState.Gameplay);

            }
            
            
            //CurrentGameState = newGameState;
            //OnGameStateChanged?.Invoke(newGameState);
        }

        public void ClearPausedSources()
        {
            pausedSources.Clear();
        }

        #if UNITY_EDITOR
        
        [MenuItem("Tiger Frog Games/DebugGameStateManager")]
        private static void DebugGameStateManager()
        {
            if (GameStateManager.Instance.pausedSources.Count == 0)
            {
                Debug.Log("Game State Manager is empty");  
            }
            
            foreach (var dictionaryKey in GameStateManager.Instance.pausedSources)
            {
                Debug.Log(dictionaryKey);
            }
        }

        [InitializeOnEnterPlayMode]
        static void InitializeOnEnterPlayMode()
        {
            Instance.ClearPausedSources();
        }
        
        #endif
        
        /*
         * 
         * Use the following methods in scripts that want to subscribe to game state. 
         * 
         * Awake/OnDestroy  Use to subscribe and unsubscribe to the event handler
         * 
         * GameStateManager_OnGameStateChanged(GameState newGameState) How do you want the code to work when in a specic game state. 
         * 
        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay);
        }
        */
        
    }
}