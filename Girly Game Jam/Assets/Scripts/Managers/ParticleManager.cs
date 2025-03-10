using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class ParticleManager : Singleton<ParticleManager>
    {
        /* ------- Variables ------- */
        [Header("Dependencies")]
        [SerializeField] private ParticleSystem particleSystemBubble;
        [SerializeField] private ParticleSystem particleSystemBlockBreack;

        [SerializeField] private ParticleSystem particleSystemRoundWinFanFair;
        [SerializeField] private ParticleSystem particleSystemGameWinFanFair;

        /* ------- Unity Methods ------- */

        private void Start()
        {
            LevelManager.Instance.OnLevelVictory += StartFanFair;
            LevelManager.Instance.OnLevelStartLoading += EndFanFair;
        }

        private void OnDestroy()
        {
            LevelManager.Instance.OnLevelVictory -= StartFanFair;
            LevelManager.Instance.OnLevelStartLoading -= EndFanFair;
        }

        /* ------- Methods ------- */

        public void PlayBubbleParticle(Vector3 position)
        {
            particleSystemBubble.transform.position = position;
            particleSystemBubble.Stop();
            particleSystemBubble.Play();
            
            
            /*ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                position = position
            };
            
            particleSystemBubble.Emit(emitParams, 12);*/
        }

        public void PlayBlockCleanUp(Vector3 transformPosition)
        {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                position = transformPosition
            };

            particleSystemBlockBreack.Emit(emitParams, 12);
        }

        private void StartFanFair()
        {
            if (LevelManager.Instance.IsOutOfLevels())
            {
                particleSystemGameWinFanFair.Play();
            }
            particleSystemRoundWinFanFair.Play();
        }

        private void EndFanFair()
        {
            particleSystemRoundWinFanFair.Stop();
        }
        
    }
}