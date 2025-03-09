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
        public static event Action OnGameWin;
        
        /* ------- Variables ------- */
        
        
        [Header("Dependencies")]
        [SerializeField] private PlayerOrb prefabPlayerOrb;

        private Dictionary<SerializableGuid, PlayerOrb> AllPlayerOrbs = new();

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */
        
        public void SpawnPlayerBall(Transform transformPosition, Transform transformPositionTwo)
        {
            PlayerOrb playerOrb = Instantiate(prefabPlayerOrb, transformPosition.position, transformPosition.rotation);
            PlayerOrb playerOrb2 = Instantiate(prefabPlayerOrb, transformPositionTwo.position, transformPositionTwo.rotation);
            
            playerOrb.SetUp(PlayerTeam.AesticOne, playerOrb2);
            playerOrb2.SetUp(PlayerTeam.AesticTwo, playerOrb);
            
            AllPlayerOrbs.Add( playerOrb.ID, playerOrb );
            AllPlayerOrbs.Add( playerOrb2.ID, playerOrb2 );
        }
        
        public void RemovePlayerOrb(PlayerOrb playerOrb)
        {
            AllPlayerOrbs.Remove(playerOrb.ID);
            
            Destroy(playerOrb.gameObject);

            if (AllPlayerOrbs.Count == 0)
            {
                OnRoundOver?.Invoke();
                
                //check to see if there are 
                
                
                //check if lives are over 
                //if they are invoke the event OnGameOver
            }
        }
        

        //No longer used.
        //Instead orbs are paired up when fired
        /*public Vector2 GetDirectionToNearestOppositeOrb(SerializableGuid owningId)
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
        }*/
    }
}