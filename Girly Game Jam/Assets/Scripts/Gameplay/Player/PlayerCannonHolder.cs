using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerCannonHolder : MonoBehaviour
    {
        public event Action OnCannonFire;
        
        /* ------- Variables ------- */
        
        [Header("Dependencies")]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerCannon leftCannon, rightCannon;
        [SerializeField] private Transform cannonTransformLeft, rightCannonTransformRight;
        [SerializeField] private InputReaderGirlyGame ir;

        
        
        private bool canAim = false;
        private bool canFire = false;
        
        private bool isUsingMouse = true;
        
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
            if(!canAim) return;
            
            if(!LevelManager.Instance.IsMouseInsideBounds()) return;
            
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition).With(z: 0);
            leftCannon.transform.right = worldPosition - leftCannon.transform.position;
            
            rightCannon.transform.eulerAngles = new Vector3(0,0, -180 + leftCannon.transform.eulerAngles.z);
        }

        /* ------- Methods ------- */

        public void EnableAim(bool state)
        {
            canAim = state;
        }
        
        public void EnableShot()
        {
            canFire = true;
        }
        
        private void FireAction(bool arg0)
        {
            if(!canFire) return;
            
            if (arg0)
            {
                PlayerManager.Instance.SpawnPlayerBall( cannonTransformLeft, rightCannonTransformRight );
                OnCannonFire?.Invoke();
                canFire = false;
            }
        }

       
    }
}