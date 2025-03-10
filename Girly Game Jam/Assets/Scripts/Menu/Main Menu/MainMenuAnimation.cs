using System;
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
           logoTransform.DOLocalMoveY(250, 2.5f).SetEase(Ease.OutBounce).onComplete = bounceLogo;
       }

       /* ------- Methods ------- */

       private void bounceLogo()
       {
           
       }

    }
}