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
        [SerializeField] private PlayerOrbMovement playerOrbMovement;

        
        [Header("Variables")] 
        [SerializeField] private float destructionTime = .8f;
        [SerializeField] private int ticksPerSecond = 4;
        [SerializeField] private float DistanceCheck = 1f;

        private PlayerOrb pairedOrb;

        public PlayerTeam PlayerTeam { private set; get; }
        private bool hitByBall = false;
        private FrequencyCountDownTimer collisionTimer;

        /* ------- Unity Methods ------- */

        private void Awake()
        {
            ID = SerializableGuid.NewGuid();

            collisionTimer = new(destructionTime, ticksPerSecond);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (hitByBall) return;

            if (other.gameObject.Equals( pairedOrb.gameObject))
            {
                hitByBall = true;
                pairedOrb.SetHitByBall(true);
                
                collisionTimer.OnTick += CheckIfInRangeOfDestruction;
                collisionTimer.OnTimerStop += RemovePlayerOrbs;
                collisionTimer.Start();
            }
        }


        /* ------- Methods ------- */

        public void SetUp(PlayerTeam team, PlayerOrb pairedOrbIn)
        {
            pairedOrb = pairedOrbIn;
            PlayerTeam = team;
            SetUpVisual();
            
            playerOrbMovement.SetUp(ID,  pairedOrb);
        }

        private void SetUpVisual()
        {
            bodyRenderer.sprite = PlayerTeam == PlayerTeam.AesticOne ? PlayerInfoLibrary.Instance.AesticOneSprite : PlayerInfoLibrary.Instance.AesticTwoSprite;
        }

        public void SetHitByBall( bool state)
        {
            hitByBall = state;
        }

        private void CheckIfInRangeOfDestruction()
        {
            if(pairedOrb == null) return;
            
            if (Vector3.Distance(transform.position, pairedOrb.transform.position) > DistanceCheck)
            {
                collisionTimer.OnTick -= CheckIfInRangeOfDestruction;
                collisionTimer.OnTimerStop -= RemovePlayerOrbs;
                collisionTimer.Stop();
                
                hitByBall = false;
                pairedOrb.SetHitByBall(false);
            }
            
        }

        private void RemovePlayerOrbs()
        {
            Debug.Log("Removing player orbs");
            
            PlayerManager.Instance.RemovePlayerOrb(pairedOrb);
            PlayerManager.Instance.RemovePlayerOrb(this);
        }


}
}