using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class TestPlayerSpawner : MonoBehaviour
    {
        /* ------- Variables ------- */
        [SerializeField] private InputReaderGirlyGame ir;

        /* ------- Unity Methods ------- */

        private void OnEnable()
        {
            ir.EnablePlayerActions();
            
            ir.Fire += FireAction;
            ir.Aim += AimAction;
        }
        
        private void OnDisable()
        {
            ir.Fire -= FireAction;
            ir.Aim -= AimAction;
        }
        
        private void FireAction(bool arg0)
        {
            if (arg0 == true)
            {
                Debug.Log("Fire action called On Button Down");
                PlayerManager.Instance.TestSpawnPlayer();
            }

            else
            {
                Debug.Log("Fire action called Button Up");
            }
        }
        
        private void AimAction(Vector2 arg0)
        {
            Debug.Log(arg0);
        }
        
        /* ------- Methods ------- */


    }
}