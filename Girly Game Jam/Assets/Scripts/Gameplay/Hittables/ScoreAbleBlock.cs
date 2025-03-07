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

            bodyRenderer.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(collisionInfo.TriggeringPlayerOrb.PlayerTeam) , .2f);
            
            bool isGoingLeft = this.transform.position.x <
                               collisionInfo.HitPosition.x;
            //collisionInfo.TriggeringPlayerOrb.PlayerTeam
            ScorePopupManager.Instance.SpawnScorePopUp(this.transform.position, isGoingLeft, PlayerTeam.Both , scoreToGive);
            ScoreManager.Instance.AddScore(scoreToGive, collisionInfo.HitPosition);
                
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .Play(SoundSFXLibrary.Instance.GetSoundByEnum(soundToPlay));
        }
        
    }
}