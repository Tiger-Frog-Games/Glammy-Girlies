using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TigerFrogGames
{
    public class UILevelComplete : MonoBehaviour
    {
        /* ------- Variables ------- */

        [SerializeField] private Button nextLevelButton;
        [SerializeField] private UIElementMover uiMover;
        [SerializeField] private UIWindow window;
        [SerializeField] private TMP_Text TotalScoreText;

        [SerializeField] private TMP_Text youWinText;
        [SerializeField] private Button toMainMenuButton;

        [SerializeField] private GameObject root;
        
        /* ------- Unity Methods ------- */

        private void Start()
        {
            nextLevelButton.onClick.AddListener(StartCallNextLevel);
            PlayerManager.OnRoundOver += startShowNextLevelWindow;

            toMainMenuButton.onClick.AddListener(startMainMenu);
            
            nextLevelButton.interactable = false;
            toMainMenuButton.interactable = false;
            
            root.SetActive(false);
        }

        private void OnDestroy()
        {
            PlayerManager.OnRoundOver -= startShowNextLevelWindow;
        }

        /* ------- Methods ------- */

        private void startShowNextLevelWindow()
        {
            if(!LevelManager.Instance.DidLevelComplete) return;

            root.SetActive(true);
            
            if (LevelManager.Instance.IsOutOfLevels())
            {
                youWinText.gameObject.SetActive(true);
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
           // window.enabled = true;
            
            if (LevelManager.Instance.IsOutOfLevels())
            {
                toMainMenuButton.interactable = true;
                nextLevelButton.interactable = false;
            }
            else
            {
                nextLevelButton.interactable = true;
                toMainMenuButton.interactable = false;
            }

            
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
            root.SetActive(false);
        }

        private void startMainMenu()
        {
            window.enabled = false;
            uiMover.OnMovementOver += CallOpenMainMenu;
            uiMover.Move("FadeOut");
        }
        
        private void CallOpenMainMenu(string movementName)
        {
            uiMover.OnMovementOver -= CallOpenMainMenu;
            root.SetActive(false);
            SceneLoader.Instance.LoadMainMenu();
        }
        
        
    }
}