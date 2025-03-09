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



        /* ------- Methods ------- */

        public void StartIntroGame()
        {
            if (SkipIntro)
            {
                LoadNextLevel();
            }
            else
            {
                //play animation for the intro
                //diologe?
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