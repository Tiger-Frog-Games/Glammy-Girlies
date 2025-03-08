using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class ItemBubble : Hittable
    {
        /* ------- Variables ------- */
        [Header("Dependencies")]
        [SerializeField] private SpriteRenderer spriteRendererIcing;
        [SerializeField] private SpriteRenderer[] bodySprites;
        [SerializeField] private SpriteRenderer spriteRendererBubble;
        [SerializeField] private Transform bodyTransform;
        
        [Header("Variables")]
        [SerializeField] private int scoreToGive;

        [SerializeField] private SoundDataName soundToPlay;
        
        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */
        
        protected override void OnHit(CollisionInfo collisionInfo)
        {
            Debug.Log("Hit orb");
            canBeHit = false;
            base.OnHit(collisionInfo);

            ParticleManager.Instance.PlayBubbleParticle(transform.position);
            
            spriteRendererIcing.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(collisionInfo.TriggeringPlayerOrb.PlayerTeam) , .2f);

            
            
            float fallTime = Mathf.Max(.2f, (transform.position.y - LevelManager.Instance.GetLevelBottomY())/4);
            
            var ySequence = DOTween.Sequence().Append( bodyTransform.DOLocalMoveY(.2f, .2f).SetEase(Ease.OutQuad)).Append(
                    bodyTransform.DOMoveY(LevelManager.Instance.GetLevelBottomY(),fallTime).SetEase(Ease.InQuad)
                );
            //1/transform.position.y * LevelManager.Instance.GetLevelBottomY()
            ySequence.Play();

            transform.DOShakeRotation(.2f, new Vector3(0, 0, 30f));

            foreach (var bodySprite in bodySprites)
            {
                bodySprite.DOFade(0f, .2f).SetDelay(fallTime).OnComplete(()=> Debug.Log("Award the shop a food item") );
            }
            
            
            
            spriteRendererBubble.DOFade(0,.2f).OnComplete(()=> { spriteRendererBubble.enabled = false; });
            
            bool isGoingLeft = this.transform.position.x <
                               collisionInfo.HitPosition.x;
            
            ScorePopupManager.Instance.SpawnScorePopUp(this.transform.position, isGoingLeft, collisionInfo.TriggeringPlayerOrb.PlayerTeam, scoreToGive);
            
            ScoreManager.Instance.AddScore(scoreToGive, collisionInfo.HitPosition);
                
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .Play(SoundSFXLibrary.Instance.GetSoundByEnum(soundToPlay));
        }
        
    }
}