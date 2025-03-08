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

        public void SpawnScorePopUp( ScorePopUpData popUpData, int score )
        {
            TMP_ColorGradient colorGradient;
            switch (popUpData.Team)
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
                    throw new ArgumentOutOfRangeException(nameof(popUpData.Team), popUpData.Team, null);
            }
            
            float size = Mathf.Lerp(fontSize.x, fontSize.y,  1/ ( score / biggestScore));
            
            var pulledObject = scoreTextPool.Pull();
            pulledObject.Display(popUpData.Position, popUpData.IsGoingLeft, size, score, colorGradient);
        }
        
    }

    public struct ScorePopUpData : IEquatable<ScorePopUpData>
    {
        public Vector3 Position { private set; get; }
        public bool IsGoingLeft { private set; get; }
        public PlayerTeam Team { private set; get; }

        public ScorePopUpData(Vector3 position, bool isGoingLeft, PlayerTeam team)
        {
            this.Position = position;
            this.IsGoingLeft = isGoingLeft;
            this.Team = team;
        }

        public bool Equals(ScorePopUpData other)
        {
            return Position.Equals(other.Position) && IsGoingLeft == other.IsGoingLeft && Team == other.Team;
        }

        public override bool Equals(object obj)
        {
            return obj is ScorePopUpData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, IsGoingLeft, (int)Team);
        }
    }
    
}