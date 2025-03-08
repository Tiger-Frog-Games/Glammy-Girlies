using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class LevelManager : Singleton<LevelManager>
    {
        /* ------- Variables ------- */
        [SerializeField] private Transform levelBottomMarker;
       

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */

        public float GetLevelBottomY()
        {
            return levelBottomMarker.transform.position.y;
        }
        
    }
}