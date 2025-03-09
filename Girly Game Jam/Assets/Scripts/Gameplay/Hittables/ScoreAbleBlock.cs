using System;
using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    [SelectionBase]
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
            
            LevelManager.Instance.AddScoreAbleBlock(this);
            
            bodyRenderer.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(collisionInfo.TriggeringPlayerOrb.PlayerTeam) , .2f);
            
            bool isGoingLeft = this.transform.position.x <
                               collisionInfo.HitPosition.x;
            
            ScorePopUpData scorePopUpData = new ScorePopUpData( collisionInfo.HitPosition, isGoingLeft, collisionInfo.TriggeringPlayerOrb.PlayerTeam );
            
            ScoreManager.Instance.AddScore(scoreToGive, scorePopUpData);
                
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .Play(SoundSFXLibrary.Instance.GetSoundByEnum(soundToPlay));
        }
        
    }
}