using UnityEngine;

namespace TigerFrogGames
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        /* ------- Variables ------- */
        
        public Observer<int> TotalScore { private set; get; } = new(0);
        public int RoundScore { private set; get; } = 0;

        /* ------- Unity Methods ------- */
        
        

        /* ------- Methods ------- */

        public void AddScore(int scoreToAdd, ScorePopUpData scorePopUpData = default)
        {
            if (!scorePopUpData.Equals(default(ScorePopUpData)))
            {
                ScorePopupManager.Instance.SpawnScorePopUp(scorePopUpData , scoreToAdd);
            }
            
            TotalScore.Value += scoreToAdd;
        }
        
        
        
    }
}