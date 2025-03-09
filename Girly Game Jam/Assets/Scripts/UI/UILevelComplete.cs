using System;
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class UILevelComplete : MonoBehaviour
    {
        /* ------- Variables ------- */

        [SerializeField] private CustomButton nextLevelButton;
        [SerializeField] private UIElementMover uiMover;
        [SerializeField] private UIWindow window;
        [SerializeField] private TMP_Text TotalScoreText;

        [SerializeField] private TMP_Text youWinText;
        [SerializeField] private CustomButton toMainMenuButton;
        /* ------- Unity Methods ------- */

        private void Start()
        {
            nextLevelButton.onClick.AddListener(StartCallNextLevel);
            PlayerManager.OnRoundOver += startShowNextLevelWindow;

            toMainMenuButton.onClick.AddListener(startMainMenu);
        }

        private void OnDestroy()
        {
            PlayerManager.OnRoundOver -= startShowNextLevelWindow;
        }

        /* ------- Methods ------- */

        private void startShowNextLevelWindow()
        {
            if(!LevelManager.Instance.DidLevelComplete) return;

            if (LevelManager.Instance.IsOutOfLevels())
            {
                youWinText.enabled = true;
                toMainMenuButton.gameObject.SetActive(true);
                
                nextLevelButton.gameObject.SetActive(false);
            }
            
            TotalScoreText.text = ScoreManager.Instance.TotalScore.Value.ToString();
            
            uiMover.OnMovementOver += showNextLevelWindow;
            uiMover.Move("LevelOverWin");
        }

        private void showNextLevelWindow(string movementName)
        {
            uiMover.OnMovementOver -= showNextLevelWindow;
            window.enabled = true;
        }

        private void StartCallNextLevel()
        {
            window.enabled = false;
            uiMover.OnMovementOver += CallNextLevel;
            uiMover.Move("FadeOut");
        }
        
        private void CallNextLevel(string movementName)
        {
            uiMover.OnMovementOver -= CallNextLevel;
            LevelManager.Instance.LoadNextLevel();
        }

        private void startMainMenu()
        {
            window.enabled = false;
            uiMover.OnMovementOver += CallOpenMainMenu;
            uiMover.Move("FadeOut");
        }
        
        private void CallOpenMainMenu(string movementName)
        {
            uiMover.OnMovementOver -= CallNextLevel;
            SceneLoader.Instance.LoadMainMenu();
        }
        
        
    }
}