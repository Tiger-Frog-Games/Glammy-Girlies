using UnityEngine;


namespace TigerFrogGames
{
    public class MainMenu : MonoBehaviour
    {
        /* ------- Variables ------- */
        
        [Header("Dependencies")]
        
        [SerializeField] private CustomButton startGameButton;
        [SerializeField] private CustomButton settingsButton;
        [SerializeField] private CustomButton exitButton;

        [SerializeField] private UIWindow MainMenuButtonWindow;
        [SerializeField] private UIElementMover MainMenuMover;
        
        [SerializeField] private SettingsMenu settingsMenu;
        
        /* ------- Unity Methods ------- */

        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGameClicked);
            settingsButton.onClick.AddListener(StartSettingsClicked);
            exitButton.onClick.AddListener(StartGameExitClicked);

            settingsMenu.OnSettingsClosed += ShowMainButtonsFromSettings;
        }

        private void Start()
        {
            Invoke(nameof(StartShowMainButtons), .5f);
        }

        private void OnDestroy()
        {
            settingsMenu.OnSettingsClosed -= StartShowMainButtons;
        }

        /* ------- Methods ------- */
        
        private void StartGameClicked()
        {
            MainMenuMover.Move("FadeDown");
            MainMenuMover.OnMovementOver += LoadGameplayStart;
        }

        private void LoadGameplayStart(string obj)
        {
            if (obj == "FadeDown")
            {
                SceneLoader.Instance.LoadGameplay();
            }
        }

        private void StartSettingsClicked()
        {
            MainMenuMover.Move("OpenSettings");
            MainMenuButtonWindow.enabled = false;
            MainMenuMover.OnMovementOver += SettingsAnimationOver;
        }

        private void SettingsAnimationOver(string obj)
        {
            if (obj == "OpenSettings")
            {
                MainMenuMover.OnMovementOver -= SettingsAnimationOver;
                settingsMenu.OpenSettingsMenu();
            }
        }

        private void StartGameExitClicked()
        {
            MainMenuMover.OnMovementOver += QuitAnimationOver;
            MainMenuButtonWindow.enabled = false;
            MainMenuMover.Move("FadeDown");
        }

        private void QuitAnimationOver(string obj)
        {
            MainMenuMover.OnMovementOver -= QuitAnimationOver;
            Application.Quit();
        }

        private void StartShowMainButtons()
        {
            MainMenuMover.Move("EnterPlay");
            MainMenuMover.OnMovementOver += ShowMainButtons;
        }

        private void ShowMainButtonsFromSettings()
        {
            MainMenuMover.Move("CloseSettings");
            MainMenuMover.OnMovementOver += ShowMainButtons;
        }
        
        private void ShowMainButtons(string name)
        {
            MainMenuMover.OnMovementOver -= ShowMainButtons;
            MainMenuButtonWindow.enabled = true;
        }
        
    }
}