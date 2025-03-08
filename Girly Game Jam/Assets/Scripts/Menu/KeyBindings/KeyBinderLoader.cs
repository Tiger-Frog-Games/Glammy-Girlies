using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class KeyBindingSettingsLoader : MonoBehaviour
    {
        /* ------- Variables ------- */
        [FormerlySerializedAs("inputReaderExample")]
        [FormerlySerializedAs("inputReader")]
        [SerializeField] private InputReaderGirlyGame inputReaderGirlyGame;


        /* ------- Unity Methods ------- */

        private void Start()
        {
            string rebinds = PlayerPrefs.GetString("Rebinds");
            if (!string.IsNullOrEmpty(rebinds))
            {
                inputReaderGirlyGame.SetKeyBindings(rebinds);
            }
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetString("Rebinds",inputReaderGirlyGame.GetSavedKeyBindings());
        }

        /* ------- Methods ------- */


    }
}