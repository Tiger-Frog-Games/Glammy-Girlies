using System;
using UnityEngine;

namespace TigerFrogGames
{
    public abstract class Hittable : MonoBehaviour
    {
        static event Action<CollisionInfo> OnHitEvent;

        protected bool canBeHit = true;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!canBeHit) return;
            
            Debug.Log("Hit");
            
            if (other.gameObject.TryGetComponent(out PlayerBall ball))
            {
                
                var contactPoint = other.contacts[0].point;
                CollisionInfo info = new CollisionInfo(ball,this, contactPoint);
                OnHit(info);
            }
        }
        
        protected virtual void OnHit(CollisionInfo collisionInfo)
        {
            OnHitEvent?.Invoke(collisionInfo);
        }
    }

    public struct CollisionInfo
    {
        public PlayerBall TriggeringPlayerBall { private set; get; }
        public Hittable Hittable { private set; get; }
        public Vector2 HitPosition { private set; get; }

        public CollisionInfo(PlayerBall triggeringPlayerBall, Hittable hittable, Vector2 hitPosition)
        {
            this.TriggeringPlayerBall = triggeringPlayerBall;
            this.Hittable = hittable;
            this.HitPosition = hitPosition;
        }
    }
}