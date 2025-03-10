
using UnityEngine;
using UnityEngine.UI;


namespace TigerFrogGames
{
    public class UILevelFailed : MonoBehaviour
    {
        /* ------- Variables ------- */

        [SerializeField] private Button restartLevelButton;
        [SerializeField] private UIElementMover uiMover;
        [SerializeField] private UIWindow window;
        [SerializeField] private GameObject root;
        
        /* ------- Unity Methods ------- */
        
        private void Start()
        {
            restartLevelButton.onClick.AddListener(resetLevel);
            PlayerManager.OnRoundOver += startShowNextLevelWindow;
            root.SetActive(false);
            //restartLevelButton = false;
            
        }

        private void OnDestroy()
        {
            PlayerManager.OnRoundOver -= startShowNextLevelWindow;
        }

        /* ------- Methods ------- */
                
        private void startShowNextLevelWindow()
        {
            if(LevelManager.Instance.DidLevelComplete ) return;
            
            if(PlayerManager.Instance.PlayerShotsRemaining.Value != 0) return;
            
            root.SetActive(true);
            
            uiMover.OnMovementOver += showNextLevelWindow;
            uiMover.Move("LevelOverWin");
        }

        private void showNextLevelWindow(string movementName)
        {
            restartLevelButton.interactable = true;
            uiMover.OnMovementOver -= showNextLevelWindow;
        }
        
        private void resetLevel()
        {
            Debug.Log("Reset Level");
            root.SetActive(false);
            restartLevelButton.interactable = false;
            uiMover.Move("FadeOut");
            LevelManager.Instance.ReloadLevel();
            
        }
    }
}