using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class PlayerOrb : MonoBehaviour
    {
        /* ------- Variables ------- */

        public SerializableGuid ID { private set; get; }

        [Header("Dependencies")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        [FormerlySerializedAs("playerBallMovement")] [SerializeField] private PlayerOrbMovement playerOrbMovement;
       
        //remove the abaility to set from editor once Ash fires the cannons. 
        [field: SerializeField] public PlayerTeam PlayerTeam { private set; get; }

        private bool hitByBall = false;
        
        /* ------- Unity Methods ------- */

        private void Awake()
        {
            ID = SerializableGuid.NewGuid();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(hitByBall) return;
            
            if(other.gameObject.TryGetComponent(out PlayerOrb otherPlayerBall))
            {
                Debug.Log(otherPlayerBall);
                hitByBall = true;
                otherPlayerBall.HitByBall();
            }
        }

        public void HitByBall()
        {
            hitByBall = true;
        }
        
        /* ------- Methods ------- */

        public void SetUp(PlayerTeam team, Vector2 initialAngle, PlayerOrb pairedOrb)
        {
            PlayerTeam = team;
            SetUpVisual();
            
            
            playerOrbMovement.SetUp(ID, initialAngle, pairedOrb);
        }

        private void SetUpVisual()
        {
            if (PlayerTeam == PlayerTeam.AesticOne)
            {
                bodyRenderer.color = PlayerInfoLibrary.Instance.AesticOneColor;
            }
            else
            {
                bodyRenderer.color = PlayerInfoLibrary.Instance.AesticTwoColor;
            }
        }
        
    }
}