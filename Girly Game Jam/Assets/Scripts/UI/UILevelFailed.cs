using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class UILevelFailed : MonoBehaviour
    {
        /* ------- Variables ------- */

        [SerializeField] private CustomButton restartLevelButton;
        [SerializeField] private UIElementMover uiMover;
        [SerializeField] private UIWindow window;

        /* ------- Unity Methods ------- */
        
        private void Start()
        {
            restartLevelButton.onClick.AddListener(resetLevel);
            PlayerManager.OnRoundOver += startShowNextLevelWindow;
            
        }

        private void OnDestroy()
        {
            PlayerManager.OnRoundOver -= startShowNextLevelWindow;
        }

        /* ------- Methods ------- */
                
        private void startShowNextLevelWindow()
        {
            window.enabled = false;
            
            if(LevelManager.Instance.DidLevelComplete ) return;
            
            if(PlayerManager.Instance.PlayerShotsRemaining.Value != 0) return;
            
            uiMover.OnMovementOver += showNextLevelWindow;
            uiMover.Move("LevelOverWin");
        }

        private void showNextLevelWindow(string movementName)
        {
            restartLevelButton.interactable = true;
            uiMover.OnMovementOver -= showNextLevelWindow;
            window.enabled = true;
        }
        
        private void resetLevel()
        {
            restartLevelButton.interactable = false;
            window.enabled = false;
            uiMover.Move("FadeOut");
            LevelManager.Instance.ReloadLevel();
            
        }
    }
}