using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class LevelManager : Singleton<LevelManager>
    {
        /* ------- Variables ------- */
        
        [SerializeField] private Transform levelBottomMarker;
        [SerializeField] private BoxCollider2D boundsCollider;
        [SerializeField] private Camera mainCamera;
        
        /* ------- Unity Methods ------- */

        private void Update()
        {
            //Debug.Log( IsMouseInsideBounds() );
        }

        /* ------- Methods ------- */

        public bool IsMouseInsideBounds()
        {
            Vector3 checkPos = mainCamera.ScreenToWorldPoint(Input.mousePosition).With(z: 0);
            return boundsCollider.bounds.Contains(checkPos);
        }
        
        public float GetLevelBottomY()
        {
            return levelBottomMarker.transform.position.y;
        }
        
    }
}