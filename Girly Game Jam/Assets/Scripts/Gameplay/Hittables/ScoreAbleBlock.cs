using System;
using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class ScoreAbleBlock : Hittable
    {
        /* ------- Variables ------- */
        [Header("Dependencies")]
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        [Header("Variables")]
        [SerializeField] private PlayerTeam playerTeam;
        [SerializeField] private int scoreToGive;

        [SerializeField] private SoundDataName soundToPlay;
        
        /* ------- Unity Methods ------- */
        
 

        /* ------- Methods ------- */

        protected override void OnHit(CollisionInfo collisionInfo)
        {
            canBeHit = false;
            base.OnHit(collisionInfo);

            bodyRenderer.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(collisionInfo.TriggeringPlayerBall.PlayerTeam)  , 5.2f);
            
            ScoreManager.Instance.AddScore(scoreToGive, collisionInfo.HitPosition);
                
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .Play(SoundSFXLibrary.Instance.GetSoundByEnum(soundToPlay));
        }
        
        private void StartColorTween(Color targetColor, bool instant)
        {
            
        }
    }
}