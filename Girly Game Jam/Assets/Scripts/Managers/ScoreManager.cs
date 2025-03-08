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

        public void AddScore(int scoreToAdd, ScorePopUpData scorePopUpData = default)
        {
            if (scorePopUpData.Equals(default(ScorePopUpData)))
            {
                ScorePopupManager.Instance.SpawnScorePopUp(scorePopUpData , scoreToAdd);
            }

        
            Debug.Log("Adding score to ScoreManager: " + scoreToAdd);
        }
        
        
        
    }
}