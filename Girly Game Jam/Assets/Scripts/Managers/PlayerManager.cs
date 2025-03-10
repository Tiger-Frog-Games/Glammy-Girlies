using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public static event Action OnRoundOver;
        public static event Action OnGameOver;
        
        /* ------- Variables ------- */
        
        
        [Header("Dependencies")]
        [SerializeField] private PlayerOrb prefabPlayerOrb;

        [SerializeField] private PlayerCannonHolder playerCannon;
        [SerializeField] private InputReaderGirlyGame inputReaderGirlyGame;
        
        
        [Header("Variables")] 
        [SerializeField] private int startingShots = 3;
        
        public Observer<int> PlayerShotsRemaining { private set; get; }

        private Dictionary<SerializableGuid, PlayerOrb> AllPlayerOrbs = new();

        /* ------- Unity Methods ------- */

        protected override void InitializeSingleton()
        {
            base.InitializeSingleton();
            PlayerShotsRemaining = new Observer<int>(startingShots);
        }

        private void Start()
        {
            LevelManager.Instance.OnLevelDoneLoading += RefreshLivesAndEnableCannon;
            
            PlayerCannonHolder.OnCannonFire += OnPlayerCannonFire;
            
            inputReaderGirlyGame.Reload += InputReaderGirlyGameOnReload;
        }
        
        private void OnDisable()
        {
            LevelManager.Instance.OnLevelDoneLoading -= RefreshLivesAndEnableCannon;
            PlayerCannonHolder.OnCannonFire -= OnPlayerCannonFire;
            
            inputReaderGirlyGame.Reload += InputReaderGirlyGameOnReload;
        }
        
        /* ------- Methods ------- */
        
        public void SpawnPlayerBall(Transform transformPosition, Transform transformPositionTwo)
        {
            PlayerOrb playerOrb = Instantiate(prefabPlayerOrb, transformPosition.position, transformPosition.rotation);
            PlayerOrb playerOrb2 = Instantiate(prefabPlayerOrb, transformPositionTwo.position, transformPositionTwo.rotation);
            
            playerOrb.SetUp(PlayerTeam.AesticTwo, playerOrb2);
            playerOrb2.SetUp(PlayerTeam.AesticOne, playerOrb);
            
            AllPlayerOrbs.Add( playerOrb.ID, playerOrb );
            AllPlayerOrbs.Add( playerOrb2.ID, playerOrb2 );
        }
        
        private void InputReaderGirlyGameOnReload(bool arg0)
        {
            if(AllPlayerOrbs.Count == 0) return;
            
            if (arg0)
            {
                var list = AllPlayerOrbs.ToList();
                
                foreach (KeyValuePair<SerializableGuid, PlayerOrb> playerOrb in list)
                {
                    RemovePlayerOrb(playerOrb.Value);
                }
            }
            
            
        }
        
        public async Task RemovePlayerOrb(PlayerOrb playerOrb)
        {
            AllPlayerOrbs.Remove(playerOrb.ID);
            
            Destroy(playerOrb.gameObject);
            
            if (AllPlayerOrbs.Count == 0)
            {
                OnRoundOver?.Invoke();
                
                await LevelManager.Instance.CleanUpLevel();
                
                if (LevelManager.Instance.DidLevelComplete)
                {
                    return;
                }
                
                if (PlayerShotsRemaining.Value == 0)
                {
                    OnGameOver?.Invoke();
                    return;
                }
                
                playerCannon.EnableShot();
            }
        }

        private void RefreshLivesAndEnableCannon()
        {
            PlayerShotsRemaining.Value = startingShots;
            
            playerCannon.EnableAim(true);
            playerCannon.EnableShot();
        }

        
        private void OnPlayerCannonFire()
        {
            PlayerShotsRemaining.Value--;
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