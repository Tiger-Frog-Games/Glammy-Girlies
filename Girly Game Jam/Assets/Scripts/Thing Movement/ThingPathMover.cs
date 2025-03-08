using System;
using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class ThingPathMover : MonoBehaviour
    {
        /* ------- Variables ------- */

        [Header("Variables")]
       [SerializeField] private Transform[] pathPoints;

        [SerializeField] private float speed;

        private Sequence sequence;
        
        /* ------- Unity Methods ------- */

        private void OnDestroy()
        {
            sequence.Kill();
        }

        private void Start()
        {
            Vector3[] path = new Vector3[pathPoints.Length];
            Vector3[] reversePath = new Vector3[pathPoints.Length];

            for (int i = 0; i < pathPoints.Length; i++)
            {
                path[i] = pathPoints[i].position;
                reversePath[pathPoints.Length - 1 - i] = pathPoints[i].position;
            }
            
            
            sequence = DOTween.Sequence().Append(transform.DOPath(path,speed,PathType.CatmullRom, PathMode.Sidescroller2D )).Append(
                transform.DOPath(reversePath,speed,PathType.CatmullRom, PathMode.Sidescroller2D )
                ).SetLoops(-1,LoopType.Restart);
            sequence.Play();
        }

        /* ------- Methods ------- */
                
        
    }
}