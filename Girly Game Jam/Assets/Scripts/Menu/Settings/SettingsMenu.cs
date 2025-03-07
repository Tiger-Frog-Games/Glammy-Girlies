using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class SettingsMenu : MonoBehaviour
    {
        /* ------- Variables ------- */

        public event Action OnSettingsClosed;
        
        [Header("Dependencies")]
        [SerializeField] private CustomButton audioNavigationButton;
        [SerializeField] private CustomButton keyBindNavigationButton;
        
        [SerializeField] private CustomButton backNavigationButton;
        
        [SerializeField] private GameObject settingsRoot;
        [SerializeField] private UIElementMover settingsRootMover;
        
        [SerializeField] private GameObject audioSettingsRoot;
        [SerializeField] private GameObject keyBindSettingsRoot;

        
        
        [Header("Development")]
        [SerializeField] private bool enableDevelopment;
        
        /* ------- Unity Methods ------- */

        private void Awake()
        {
            audioNavigationButton.onClick.AddListener(ShowAudioOptions);
            keyBindNavigationButton.onClick.AddListener(ShowKeyBindOptions);
            
            backNavigationButton.onClick.AddListener(StartCloseMenu);
        }
        
        /* ------- Methods ------- */

        public void OpenSettingsMenu()
        {
            ShowAudioOptions();
            settingsRoot.SetActive(true);
            settingsRootMover.Move("OpenSettings");
        }

        private void StartCloseMenu()
        {
            settingsRootMover.Move("CloseSettings");
            settingsRootMover.OnMovementOver += CloseMenu;
        }

        private void CloseMenu(string movementName )
        {
            settingsRootMover.OnMovementOver -= CloseMenu;
            OnSettingsClosed?.Invoke();
            settingsRoot.SetActive(false);
        }

        private void ShowAudioOptions()
        {
            audioNavigationButton.interactable=(false);
            keyBindNavigationButton.interactable=(true);
            
            audioSettingsRoot.SetActive(true);
            keyBindSettingsRoot.SetActive(false);
        }

        private void ShowKeyBindOptions()
        {
            audioNavigationButton.interactable=(true);
            keyBindNavigationButton.interactable=(false);
            
            audioSettingsRoot.SetActive(false);
            keyBindSettingsRoot.SetActive(true);
        }
        
        
    }
}