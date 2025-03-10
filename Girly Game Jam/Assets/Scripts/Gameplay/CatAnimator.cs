using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class CatAnimator : MonoBehaviour
    {
        /* ------- Variables ------- */
        
       private Sequence catSequenceX;
       private Sequence catSequenceY;

        /* ------- Unity Methods ------- */

        private void Awake()
        {
            catSequenceX = DOTween.Sequence().Append( transform.DOScaleX( .8f, .2f)).Append(
                transform.DOScaleX( 1, .2f));
            catSequenceY = DOTween.Sequence().Append( transform.DOScaleY( 1.2f, .2f)).Append(
                transform.DOScaleY( 1, .2f));
            
            PlayerCannonHolder.OnCannonFire += AnimateCat;
        }

        private void OnDestroy()
        {
            PlayerCannonHolder.OnCannonFire -= AnimateCat;
        }

        /* ------- Methods ------- */

        private void AnimateCat()
        {
            Debug.Log("Cat is animating");
            if (catSequenceX.IsPlaying()) return;
            Debug.Log("Cat is animating");
            catSequenceX.Play();
            catSequenceY.Play();
        }
        
        
    }
}