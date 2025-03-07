using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class TestClick : MonoBehaviour
    {
        /* ------- Variables ------- */
        [SerializeField] private InputReaderExample ir;


        /* ------- Unity Methods ------- */

        private void OnEnable()
        {
            ir.EnablePlayerActions();
            
            ir.Action1 += IrOnAction1;
        }

        private void OnDisable()
        {
            ir.Action1 -= IrOnAction1;
        }

        private void IrOnAction1(bool arg0)
        {
            Debug.Log("IrOnAction1");
        }


        /* ------- Methods ------- */


    }
}