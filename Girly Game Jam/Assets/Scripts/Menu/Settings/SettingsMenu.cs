using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class SettingsMenu : MonoBehaviour
    {
        /* ------- Variables ------- */

        public event Action OnSettingsClosed;
        
        [Header("Dependencies")]
       
        [SerializeField] private CustomButton backNavigationButton;
        
        [SerializeField] private GameObject settingsRoot;
        [SerializeField] private UIElementMover settingsRootMover;
        
        [SerializeField] private GameObject audioSettingsRoot;
        

        
        
        [Header("Development")]
        [SerializeField] private bool enableDevelopment;
        
        /* ------- Unity Methods ------- */

        private void Awake()
        {

            
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
            
            audioSettingsRoot.SetActive(true);
           
        }
        
        
    }
}