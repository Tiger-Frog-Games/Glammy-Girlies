using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class PlayerOrbMovement : MonoBehaviour
    {
        /* ------- Variables ------- */

        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D rb;
        
        [Header("Variables")]
        [SerializeField] private float startingVelocity = 100f;

        [SerializeField] private float attractionThreshHold = 5f;
        [SerializeField] private float attractionVelocity = 1f;

        
        private SerializableGuid owningId;
        private PlayerOrb pairedOrb;
        
        /* ------- Unity Methods ------- */

        private void FixedUpdate()
        {
            //PlayerManager.Instance.GetDirectionToNearestOppositeBall(owningId)
            float distanceMultiplier = 1 / Vector3.Distance(transform.position, pairedOrb.transform.position);
            var direction = pairedOrb.transform.position - transform.position;
            rb.AddForce( direction * (distanceMultiplier * attractionVelocity));
            
        }

        /* ------- Methods ------- */


        public void SetUp(SerializableGuid ownerIdIn, Vector2 initialAngle, PlayerOrb pairedOrbIn)
        {
            owningId = ownerIdIn;
            pairedOrb = pairedOrbIn;
            rb.AddForce(initialAngle * startingVelocity);
            
            this.enabled = true;
        }
        
    }
}