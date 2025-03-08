using UnityEngine;

namespace TigerFrogGames
{
    public class ParticleManager : Singleton<ParticleManager>
    {
        /* ------- Variables ------- */
        [Header("Dependencies")]
        [SerializeField] private ParticleSystem particleSystemBubble;
       

        /* ------- Unity Methods ------- */
        
       

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
        
    }
}