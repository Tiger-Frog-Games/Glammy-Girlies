using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class UIWindow : MonoBehaviour
    {
        /* ------- Variables ------- */

        [SerializeField] private CustomButton[] buttons;
       

        /* ------- Unity Methods ------- */

        private void Awake()
        {
            UpdateButtons(isActiveAndEnabled);
        }

        private void OnEnable()
        {
            UpdateButtons(true);
        }

        private void OnDisable()
        {
            UpdateButtons(false);
        }
        
        /* ------- Methods ------- */

        private void UpdateButtons(bool newState)
        {
            foreach (CustomButton button in buttons)
            {
                button.enabled = newState;
            }
        }
        
        [ContextMenu("Scan for Buttons")]
        private void ScanForButtons()
        {
            var foundButtons = gameObject.GetComponentsInChildren<CustomButton>();
            buttons = new CustomButton[foundButtons.Length];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = foundButtons[i];
            }
        }
        
    }
}