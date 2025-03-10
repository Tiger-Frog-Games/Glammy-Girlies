using UnityEngine;
using UnityEngine.UI;

namespace TigerFrogGames
{
    public class UIIntro : Singleton<UIIntro>
    {
        /* ------- Variables ------- */

        [SerializeField] private Button restartLevelButton;
        [SerializeField] private UIElementMover uiMover;
        [SerializeField] private UIWindow window;
        [SerializeField] private GameObject root;

        /* ------- Unity Methods ------- */

        protected override void InitializeSingleton()
        {
            base.InitializeSingleton();
        }

        private void Start()
        {
            restartLevelButton.onClick.AddListener(startGame);
            
           
            
        }
        
        /* ------- Methods ------- */
                
        

        
        private void startGame()
        {
            root.SetActive(false);
            restartLevelButton.interactable = false;
            GameFlowManager.Instance.LoadNextLevel();
        }
    }
}