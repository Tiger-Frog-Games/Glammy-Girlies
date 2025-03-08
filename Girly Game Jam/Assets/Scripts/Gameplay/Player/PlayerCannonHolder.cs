using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerCannonHolder : MonoBehaviour
    { 
        /* ------- Variables ------- */
        
        [Header("Dependencies")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerCannon leftCannon, rightCannon;
        [SerializeField] private Transform cannonTransformLeft, rightCannonTransformRight;
        [SerializeField] private InputReaderGirlyGame ir;

        
        
        private bool isInFiringMode = true;
        private bool isUsingMouse = false;
        
        /* ------- Unity Methods ------- */

        private void OnEnable()
        {
            ir.EnablePlayerActions();
            
            ir.Fire += FireAction;
            //ir.Aim += AimAction;

            // transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime);
            // System.Console.WriteLine(destination);
        }
        
        private void OnDisable()
        {
            ir.Fire -= FireAction;
            //ir.Aim -= AimAction;
        }
        
        private void Update()
        {
            if(!isInFiringMode) return;
            
            if(!LevelManager.Instance.IsMouseInsideBounds()) return;
            
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition).With(z: 0);
            leftCannon.transform.right = worldPosition - leftCannon.transform.position;
            
            rightCannon.transform.eulerAngles = new Vector3(0,0, -180 + leftCannon.transform.eulerAngles.z);
        }

        /* ------- Methods ------- */

        private void FireAction(bool arg0)
        {
            if(!isInFiringMode) return;
            
            if (arg0 == true)
            {
                Debug.Log("Fire action called On Button Down");
            }
        }
    }
}