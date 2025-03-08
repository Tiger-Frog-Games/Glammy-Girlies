using UnityEngine;

namespace TigerFrogGames
{
    public class ThingRotator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float rotationSpeed = 10;
        [SerializeField, Range(-1,1)] private int rotationDirection = 1;
        
        #endregion

        #region Unity Methods
        
        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationDirection * rotationSpeed * Time.deltaTime);
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        #endregion

        /// <summary>
        /// Should be
        /// -1 for left rotation
        /// 1 for right rotation
        /// 0 for no rotation
        /// </summary>
        /// <param name="i"></param>
        public void SetDirection(int i)
        {
            rotationDirection = i;
        }
    }
}