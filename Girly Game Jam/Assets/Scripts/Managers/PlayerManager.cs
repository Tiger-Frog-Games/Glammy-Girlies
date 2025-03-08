using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        /* ------- Variables ------- */
        [FormerlySerializedAs("prefabPlayerBall")]
        [Header("Dependencies")]
        [SerializeField] private PlayerOrb prefabPlayerOrb;

        private Dictionary<SerializableGuid, PlayerOrb> AllPlayerBalls = new();

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */

        [SerializeField] private Transform testSpawnPointOne;
        [SerializeField] private Transform testSpawnPointTwo;

        public void SpawnPlayerBall(Vector2 position, Vector2 direction)
        {
            Debug.Log($"{position} - {direction}");
        }
        
        public void TestSpawnPlayer()
        {
            PlayerOrb playerOrb = Instantiate(prefabPlayerOrb, testSpawnPointOne);
            
            
            PlayerOrb playerOrb2 = Instantiate(prefabPlayerOrb, testSpawnPointTwo);
            
            playerOrb.SetUp(PlayerTeam.AesticOne, Vector2.right, playerOrb2);
            playerOrb2.SetUp(PlayerTeam.AesticTwo, Vector2.left, playerOrb);
            
            AllPlayerBalls.Add( playerOrb.ID, playerOrb );
            AllPlayerBalls.Add( playerOrb2.ID, playerOrb2 );
        }

        public Vector2 GetDirectionToNearestOppositeBall(SerializableGuid owningId)
        {
            var owningUnit = AllPlayerBalls[owningId];
            var teamToLookFor = owningUnit.PlayerTeam != PlayerTeam.AesticOne ? PlayerTeam.AesticOne : PlayerTeam.AesticTwo;

            float closestDistance = float.MaxValue;
            SerializableGuid closestOwner = default;
            
            foreach (var VARIABLE in AllPlayerBalls)
            {
                if (VARIABLE.Value.PlayerTeam == teamToLookFor &&
                    Vector3.Distance(owningUnit.transform.position, VARIABLE.Value.transform.position) <
                    closestDistance)
                {
                    closestDistance = Vector3.Distance(owningUnit.transform.position, VARIABLE.Value.transform.position);
                    closestOwner = VARIABLE.Key;
                }
            }
            
            var closestUnit = AllPlayerBalls[closestOwner];
            
            return  closestUnit.transform.position - owningUnit.transform.position;
        }
    }
}