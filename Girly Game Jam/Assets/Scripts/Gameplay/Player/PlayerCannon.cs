using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerCannon : MonoBehaviour
    {
        /* ------- Variables ------- */
        [SerializeField] private InputReaderGirlyGame ir;

        [SerializeField] private Vector3 targetPosition;

        [SerializeField] private Camera mainCamera;

        // [SerializeField] private Vector2 destination;

        /* ------- Unity Methods ------- */

        private void OnEnable()
        {
            ir.EnablePlayerActions();
            
            ir.Fire += FireAction;
            ir.Aim += AimAction;

            // transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime);
            // System.Console.WriteLine(destination);
        }
        
        private void OnDisable()
        {
            ir.Fire -= FireAction;
            ir.Aim -= AimAction;
        }

        private void Update()
        {
            // i have a mouse position and i want this in screen space and i want this only on the z axis
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition).With(z: 0);

            transform.right = worldPosition - transform.position;

        }

        private void FireAction(bool arg0)
        {
            if (arg0 == true)
            {
                Debug.Log("Fire action called On Button Down");

                // send the player mouse position to the log upon clicking left mouse button
                Vector3 mousePosition = Input.mousePosition;

                
               // Debug.Log("Mouse position x position is: " + mousePosition.x);
               // Debug.Log("Mouse position y position is: " + mousePosition.y);

                // creating a raycast point
                // Ray ray = new Ray(transform.position, transform.forward);

                // alternative ray creation that starts at the center of the camera's viewport using a helper function
                // Ray ray2 = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));                    

                // PlayerManager.Instance.TestSpawnPlayer();

                // creates a ray from the mouse position
                // Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);

                // convert the mouse position to world space in 2D (using Screen To World Point). spits out the x, y, and z position.
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               // Debug.Log(Input.mousePosition);

                // spits out totally different position from the one above
                //Debug.Log(worldPosition);

                Ray ray = new Ray(transform.position, transform.forward);

                // Debug.DrawRay(Vector3 worldPosition, Vector3 direction);



                  

            }

            else
            {
                Debug.Log("Fire action called Button Up");

                // this would probably be where the aiming mouse position goes since this is before the player clicks the button down.
            }
        }
        
        private void AimAction(Vector2 arg0)
        {
            Debug.Log(arg0);
        }
        
        /* ------- Methods ------- */


    }
}