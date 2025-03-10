using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class MainMenuAnimation : MonoBehaviour
    {
        /* ------- Variables ------- */

       [SerializeField] private Transform logoTransform;
       [SerializeField] private Transform EmoCat;
       [SerializeField] private Transform PrincessCat;

       /* ------- Unity Methods ------- */

       private void Start()
       {
           logoTransform.DOLocalMoveY(250, 2.5f).SetEase(Ease.OutBounce).onComplete = startMenuAnimation;
       }

       /* ------- Methods ------- */

       private void startMenuAnimation()
       {
           StartCoroutine(bounceLogo());
       }
       
       private IEnumerator bounceLogo()
       {
           while (true)
           {
               
               Sequence sequence = DOTween.Sequence().Append(EmoCat.DOScaleX(.8f, .2f)).Append(
                   EmoCat.DOScaleX(1, .2f));
               Sequence sequence2 = DOTween.Sequence().Append(EmoCat.DOScaleY(1.2f, .2f)).Append(
                   EmoCat.DOScaleY(1, .2f));
               
               yield return new WaitForSeconds(2.5f);
               
               Sequence sequence3 = DOTween.Sequence().Append(PrincessCat.DOScaleX(.8f, .2f)).Append(
                   PrincessCat.DOScaleX(1, .2f));
               Sequence sequence4 = DOTween.Sequence().Append(PrincessCat.DOScaleY(1.2f, .2f)).Append(
                   PrincessCat.DOScaleY(1, .2f));
               

               yield return new WaitForSeconds(4.5f);
           }
       }

    }
}