using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class PlayerBallMovement : MonoBehaviour
    {
        /* ------- Variables ------- */

        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D rb;
        
        [Header("Variables")]
        [SerializeField] private float startingVelocity = 100f;

        [SerializeField] private float attractionThreshHold = 1f;
        [SerializeField] private float attractionVelocity = 10f;

        private SerializableGuid owningId;
        /* ------- Unity Methods ------- */

        private void FixedUpdate()
        {
            if (rb.linearVelocity.magnitude < attractionThreshHold)
            {
                rb.AddForce( PlayerManager.Instance.GetDirectionToNearestOppositeBall(owningId  ) );
            }
        }

        /* ------- Methods ------- */


        public void SetUp(SerializableGuid ownerIdIn, Vector2 initialAngle)
        {
            owningId = ownerIdIn;
            rb.AddForce(initialAngle * startingVelocity);
            
            this.enabled = true;
        }
        
    }
}