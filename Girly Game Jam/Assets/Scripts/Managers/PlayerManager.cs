using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        /* ------- Variables ------- */
        [Header("Dependencies")]
        [SerializeField] private PlayerBall prefabPlayerBall;

        private Dictionary<SerializableGuid, PlayerBall> AllPlayerBalls = new();

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */

        [SerializeField] private Transform testSpawnPointOne;
        [SerializeField] private Transform testSpawnPointTwo; 
        
        public void TestSpawnPlayer()
        {
            PlayerBall playerBall = Instantiate(prefabPlayerBall, testSpawnPointOne);
            playerBall.SetUp(PlayerTeam.AesticOne, Vector2.right);
            
           // AllPlayerBalls.Add( playerBall.ID, playerBall );
            
            PlayerBall playerBall2 = Instantiate(prefabPlayerBall, testSpawnPointTwo);
            playerBall2.SetUp(PlayerTeam.AesticTwo, Vector2.left);
            
           // AllPlayerBalls.Add( playerBall2.ID, playerBall2 );
        }

        public Vector2 GetDirectionToNearestOppositeBall(SerializableGuid owningId)
        {
            return Vector2.zero;
            //throw new System.NotImplementedException();
        }
    }
}