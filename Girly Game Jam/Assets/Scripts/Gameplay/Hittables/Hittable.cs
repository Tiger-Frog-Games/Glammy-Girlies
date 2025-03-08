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
            
            if (other.gameObject.TryGetComponent(out PlayerOrb ball))
            {
                
                var contactPoint = other.contacts[0].point;
                CollisionInfo info = new CollisionInfo(ball,this, contactPoint);
                OnHit(info);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!canBeHit) return;
            
            if (other.gameObject.TryGetComponent(out PlayerOrb ball))
            {
                
                var contactPoint = other.transform.position;
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
        public PlayerOrb TriggeringPlayerOrb { private set; get; }
        public Hittable Hittable { private set; get; }
        public Vector2 HitPosition { private set; get; }

        public CollisionInfo(PlayerOrb triggeringPlayerOrb, Hittable hittable, Vector2 hitPosition)
        {
            this.TriggeringPlayerOrb = triggeringPlayerOrb;
            this.Hittable = hittable;
            this.HitPosition = hitPosition;
        }
    }
}