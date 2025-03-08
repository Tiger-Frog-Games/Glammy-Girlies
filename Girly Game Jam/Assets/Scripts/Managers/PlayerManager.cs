using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public static event Action OnRoundOver;
        public static event Action OnGameOver;
        
        /* ------- Variables ------- */
        [FormerlySerializedAs("prefabPlayerBall")]
        [Header("Dependencies")]
        [SerializeField] private PlayerOrb prefabPlayerOrb;

        private Dictionary<SerializableGuid, PlayerOrb> AllPlayerOrbs = new();

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */

        [SerializeField] private Transform testSpawnPointOne;
        [SerializeField] private Transform testSpawnPointTwo;

        public void SpawnPlayerBall(Vector2 position, Vector2 direction)
        {
            Debug.Log($"{position} - {direction}");
        }
        
        [ContextMenu("Spawn Player Ball")]
        public void TestSpawnPlayer()
         {
            PlayerOrb playerOrb = Instantiate(prefabPlayerOrb, testSpawnPointOne.position, testSpawnPointOne.rotation);
            PlayerOrb playerOrb2 = Instantiate(prefabPlayerOrb, testSpawnPointTwo.position, testSpawnPointTwo.rotation);
            
            
             playerOrb.SetUp(PlayerTeam.AesticOne, testSpawnPointOne.eulerAngles, playerOrb2);
            playerOrb2.SetUp(PlayerTeam.AesticTwo,testSpawnPointTwo.eulerAngles, playerOrb);
            
             AllPlayerOrbs.Add( playerOrb.ID, playerOrb );
             AllPlayerOrbs.Add( playerOrb2.ID, playerOrb2 );
        }

        public Vector2 GetDirectionToNearestOppositeOrb(SerializableGuid owningId)
        {
            var owningUnit = AllPlayerOrbs[owningId];
            var teamToLookFor = owningUnit.PlayerTeam != PlayerTeam.AesticOne ? PlayerTeam.AesticOne : PlayerTeam.AesticTwo;

            float closestDistance = float.MaxValue;
            SerializableGuid closestOwner = default;
            
            foreach (var VARIABLE in AllPlayerOrbs)
            {
                if (VARIABLE.Value.PlayerTeam == teamToLookFor &&
                    Vector3.Distance(owningUnit.transform.position, VARIABLE.Value.transform.position) <
                    closestDistance)
                {
                    closestDistance = Vector3.Distance(owningUnit.transform.position, VARIABLE.Value.transform.position);
                    closestOwner = VARIABLE.Key;
                }
            }
            
            var closestUnit = AllPlayerOrbs[closestOwner];
            
            return  closestUnit.transform.position - owningUnit.transform.position;
        }

        public void RemovePlayerOrb(PlayerOrb playerOrb)
        {
            AllPlayerOrbs.Remove(playerOrb.ID);
            
            Destroy(playerOrb.gameObject);

            if (AllPlayerOrbs.Count == 0)
            {
                OnRoundOver?.Invoke();
                //check if lives are over 
                //if they are invoke the event OnGameOver
            }
        }
    }
}