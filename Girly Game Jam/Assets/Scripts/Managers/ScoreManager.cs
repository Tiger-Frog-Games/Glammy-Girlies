using UnityEngine;

namespace TigerFrogGames
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        /* ------- Variables ------- */
        
        public int TotalScore { private set; get; } = 0;
        public int RoundScore { private set; get; } = 0;

        /* ------- Unity Methods ------- */
        
        

        /* ------- Methods ------- */

        public void AddScore( int scoreToAdd, Vector2 scorePosition = default)
        {
            Debug.Log("Adding score to ScoreManager: " + scoreToAdd);
        }
        
        
        
    }
}