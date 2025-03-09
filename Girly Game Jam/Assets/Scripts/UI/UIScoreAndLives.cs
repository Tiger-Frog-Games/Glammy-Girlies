using System;
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class UIScoreAndLives : MonoBehaviour
    {
        /* ------- Variables ------- */

       [Header("Dependencies")]
       [SerializeField] private TMP_Text scoreText;
       [SerializeField] private TMP_Text livesText;
        /* ------- Unity Methods ------- */

        private void Start()
        {
            PlayerManager.Instance.PlayerShotsRemaining.OnStatChange.AddListener(OnLivesChange);
            ScoreManager.Instance.RoundScore.OnStatChange.AddListener(OnScoreChange);

            OnLivesChange(PlayerManager.Instance.PlayerShotsRemaining.Value);
            OnScoreChange(ScoreManager.Instance.RoundScore.Value);
        }

        /* ------- Methods ------- */

        private void OnScoreChange(int newScore)
        {
            scoreText.text = newScore.ToString();
        }
        
        private void OnLivesChange(int newStat)
        {
            livesText.text = newStat.ToString();
        }
        
    }
}