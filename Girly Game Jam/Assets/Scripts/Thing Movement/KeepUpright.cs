using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class KeepUpright : MonoBehaviour
    {
        /* ------- Variables ------- */

       

        /* ------- Unity Methods ------- */

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        /* ------- Methods ------- */
                
        
    }
}