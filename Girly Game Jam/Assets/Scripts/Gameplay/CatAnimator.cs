using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class CatAnimator : MonoBehaviour
    {
        /* ------- Variables ------- */
        
        [SerializeField] private PlayerTeam playerTeam;
        
        private Sequence catSequenceX;
        private Sequence catSequenceY;

        /* ------- Unity Methods ------- */

        private void Awake()
        {
            PlayerCannonHolder.OnCannonFire += AnimateCat;
            ItemBubble.OnCollected += ItemBubbleOnOnCollected;
        }

        
        private void OnDestroy()
        {
            PlayerCannonHolder.OnCannonFire -= AnimateCat;
            ItemBubble.OnCollected -= ItemBubbleOnOnCollected;
        }

        /* ------- Methods ------- */

        private void ItemBubbleOnOnCollected(GameObject arg1, PlayerTeam arg2)
        {
            if(playerTeam != arg2) return;
            
            AnimateCat();
        }
        
        private bool firedOnce = false;
        private void AnimateCat()
        {
            
            if ( firedOnce && (catSequenceX.IsPlaying() || catSequenceY.IsPlaying())) return;
           
            firedOnce = true;
            
            catSequenceX = DOTween.Sequence().Append( transform.DOScaleX( .8f, .2f)).Append(
                transform.DOScaleX( 1, .2f));
            catSequenceY = DOTween.Sequence().Append( transform.DOScaleY( 1.2f, .2f)).Append(
                transform.DOScaleY( 1, .2f));
            
            catSequenceX.Play();
            catSequenceY.Play();
        }
        
        
    }
}