using System;
using TMPro;
using UnityEngine;
using OWS.ObjectPooling;

namespace TigerFrogGames
{
    public class ScorePopupManager : Singleton<ScorePopupManager>
    {
        /* ------- Variables ------- */
        [Header("Dependencies")]
        [SerializeField] private ScoreTextPopup scorePopupPrefab;
        
        [Header("Variables")]
        [SerializeField] private Vector2 fontSize;
        [SerializeField] private float biggestScore;
        
        
        private ObjectPool<ScoreTextPopup> scoreTextPool;
        
        private TMP_ColorGradient mixedGradient;
        private TMP_ColorGradient aesticOneGradient;
        private TMP_ColorGradient aesticTwoGradient;
        
        
        /* ------- Unity Methods ------- */

        protected override void InitializeSingleton()
        {
            base.InitializeSingleton();
            
            // Gradient Team 1
            aesticOneGradient = new()
            {
                topLeft = PlayerInfoLibrary.Instance.AesticOneColor,
                bottomLeft = PlayerInfoLibrary.Instance.AesticOneColor,
                topRight = PlayerInfoLibrary.Instance.AesticOneColor,
                bottomRight = PlayerInfoLibrary.Instance.AesticOneColor
            };

            // Gradient Team 2
            aesticTwoGradient = new()
            {
                topLeft = PlayerInfoLibrary.Instance.AesticTwoColor,
                bottomLeft = PlayerInfoLibrary.Instance.AesticTwoColor,
                topRight = PlayerInfoLibrary.Instance.AesticTwoColor,
                bottomRight = PlayerInfoLibrary.Instance.AesticTwoColor
            };

            // Gradient team mixed
            mixedGradient = new()
            {
                topLeft = PlayerInfoLibrary.Instance.AesticOneColor,
                bottomLeft = PlayerInfoLibrary.Instance.AesticOneColor,
                topRight = PlayerInfoLibrary.Instance.AesticTwoColor,
                bottomRight = PlayerInfoLibrary.Instance.AesticTwoColor
            };

            scoreTextPool = new ObjectPool<ScoreTextPopup>(scorePopupPrefab.gameObject, 10, this.gameObject);
        }
        

        /* ------- Methods ------- */

        public void SpawnScorePopUp( Vector3 position, bool isGoingLeft, PlayerTeam team, int score )
        {
            TMP_ColorGradient colorGradient;
            switch (team)
            {
                case PlayerTeam.Both:
                    colorGradient = mixedGradient;
                    break;
                case PlayerTeam.AesticOne:
                    colorGradient = aesticOneGradient;
                    break;
                case PlayerTeam.AesticTwo:
                    colorGradient = aesticTwoGradient;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(team), team, null);
            }
            
            float size = Mathf.Lerp(fontSize.x, fontSize.y,  1/ ( score / biggestScore));
            
            var pulledObject = scoreTextPool.Pull();
            pulledObject.Display(position, isGoingLeft, size, score, colorGradient);
        }
        
    }
}