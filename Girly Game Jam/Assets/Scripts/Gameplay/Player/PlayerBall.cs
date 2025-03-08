using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerBall : MonoBehaviour
    {
        /* ------- Variables ------- */

        public SerializableGuid ID { private set; get; }

        [Header("Dependencies")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        [SerializeField] private PlayerBallMovement playerBallMovement;
       
        //remove the abaility to set from editor once Ash fires the cannons. 
        [field: SerializeField] public PlayerTeam PlayerTeam { private set; get; }

        private bool hitByBall = false;
        
        /* ------- Unity Methods ------- */

        private void Start()
        {
            ID = SerializableGuid.NewGuid();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(hitByBall) return;
            
            if(other.gameObject.TryGetComponent(out PlayerBall otherPlayerBall))
            {
                Debug.Log(otherPlayerBall);
                otherPlayerBall.HitByBall();
            }
        }

        public void HitByBall()
        {
            hitByBall = true;
        }
        
        /* ------- Methods ------- */

        public void SetUp(PlayerTeam team, Vector2 initialAngle)
        {
            PlayerTeam = team;
            SetUpVisual();
            
            
            playerBallMovement.SetUp(ID, initialAngle);
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