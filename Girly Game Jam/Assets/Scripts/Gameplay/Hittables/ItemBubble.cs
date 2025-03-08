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
            
            transform.parent = null;
            
            base.OnHit(collisionInfo);

            ParticleManager.Instance.PlayBubbleParticle(transform.position);
            
            spriteRendererIcing.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(collisionInfo.TriggeringPlayerOrb.PlayerTeam) , .2f);

            
            
            float fallTime = Mathf.Max(.2f, (transform.position.y - LevelManager.Instance.GetLevelBottomY())/4);
            
            var ySequence = DOTween.Sequence().Append( bodyTransform.DOLocalMoveY(.2f, .2f).SetEase(Ease.OutQuad)).Append(
                    bodyTransform.DOMoveY(LevelManager.Instance.GetLevelBottomY(),fallTime).SetEase(Ease.InQuad)
                );
           
            ySequence.Play();

            transform.DOShakeRotation(.2f, new Vector3(0, 0, 30f));

            foreach (var bodySprite in bodySprites)
            {
                bodySprite.DOFade(0f, .2f).SetDelay(fallTime).OnComplete(()=> Debug.Log("Award the shop a food item") );
            }
            
            
            
            spriteRendererBubble.DOFade(0,.2f).OnComplete(()=> { spriteRendererBubble.enabled = false; });
            
            bool isGoingLeft = this.transform.position.x <
                               collisionInfo.HitPosition.x;
            
            ScorePopUpData scorePopUpData = new ScorePopUpData( collisionInfo.HitPosition, isGoingLeft, collisionInfo.TriggeringPlayerOrb.PlayerTeam );
            ScoreManager.Instance.AddScore(scoreToGive, scorePopUpData);
                
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .Play(SoundSFXLibrary.Instance.GetSoundByEnum(soundToPlay));
        }

        [ContextMenu("Test Set Team")]
        public void TestSetTeam()
        {
            spriteRendererIcing.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(PlayerTeam.AesticOne) , .2f);
        }
        
        [ContextMenu("Test Set Team2")]
        public void TestSetTeamtwo()
        {
            spriteRendererIcing.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(PlayerTeam.AesticTwo) , .2f);
        }
        
        [ContextMenu("Test Set Both")]
        public void TestSetTeamBoth()
        {
            spriteRendererIcing.DOColor(PlayerInfoLibrary.Instance.GetColorByTeam(PlayerTeam.Both) , .2f);
        }
        
    }
}