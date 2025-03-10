using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class GameFlowManager : Singleton<GameFlowManager>
    {
        public static event Action OnGameStarted;
        public static event Action OnGameEnded;
        
        [SerializeField] private bool SkipIntro = false;
        /* ------- Variables ------- */



        /* ------- Unity Methods ------- */

        private void Start()
        {
            StartIntroGame();
        }

        /* ------- Methods ------- */

        public void StartIntroGame()
        {
            if (SkipIntro)
            {
                LoadNextLevel();
            }
            else
            {
                
            }
        }

        public void EndIntroGame()
        {
            
        }

        public void LoadNextLevel()
        {
            LevelManager.Instance.LoadNextLevel();
        }
        
    }
}