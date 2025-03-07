using UnityEngine;

namespace TigerFrogGames
{
    public class AudioPlayer : MonoBehaviour
    {
        /* ------- Variables ------- */

        [SerializeField] private SoundDataName soundToPlay;

        /* ------- Unity Methods ------- */
        
       

        /* ------- Methods ------- */
        public void PlayAudioOnHover() 
        {
            SoundManager.Instance.CreateSoundBuilder()
                .WithRandomPitch()
                .Play(SoundSFXLibrary.Instance.GetSoundByEnum(soundToPlay));
        }
        
    }
}