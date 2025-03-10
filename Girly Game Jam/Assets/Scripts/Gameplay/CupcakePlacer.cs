using System;
using DG.Tweening;
using UnityEngine;

namespace TigerFrogGames
{
    public class CupcakePlacer : Singleton<CupcakePlacer>
    {
        /* ------- Variables ------- */
        [SerializeField] private GameObject[] LeftSideList, RightSideList;
        private int LastLeftSide, LastRightSide = 0;
        

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */

        public void PlaceCupCake(GameObject cupcake, PlayerTeam team)
        {
            cupcake.transform.parent = null;

            switch (team)
            {
                case PlayerTeam.Both:
                    break;
                case PlayerTeam.AesticOne:
                    if(LastLeftSide > LeftSideList.Length) return;
                    
                    cupcake.transform.position = LeftSideList[LastLeftSide].transform.position;
                    LastLeftSide++;
                    
                    foreach (SpriteRenderer sprite in cupcake.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sprite.DOFade(1, 0);
                    }
                    
                    break;
                case PlayerTeam.AesticTwo:
                    if(LastRightSide > RightSideList.Length) return;
                    
                    cupcake.transform.position = RightSideList[LastRightSide].transform.position;
                    LastRightSide++;
                    
                    foreach (SpriteRenderer sprite in cupcake.GetComponentsInChildren<SpriteRenderer>())
                    {
                        sprite.DOFade(1, 0);
                    }
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(team), team, null);
            }
        }
    }
}