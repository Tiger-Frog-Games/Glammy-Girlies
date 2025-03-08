using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(fileName = "new SoundSFXLibrary", menuName = "ScriptableObject/SoundSFXLibrary")]
    public class SoundSFXLibrary : ScriptableObjectSingleton<SoundSFXLibrary>
    {
        [Header("Main Menu")]
        [field: SerializeField] public SoundData HoverButtonMainMenu {get; private set;}
        [field: SerializeField] public SoundData BlockHit {get; private set;}


        public SoundData GetSoundByEnum(SoundDataName soundDataName)
        {
            switch (soundDataName)
            {
                case SoundDataName.HoverButtonMainMenu:
                    return HoverButtonMainMenu;
                case SoundDataName.BlockHit:
                    return BlockHit;
                default:
                    throw new ArgumentOutOfRangeException(nameof(soundDataName), soundDataName, null);
            }
        }


    }

    public enum SoundDataName
    {
        HoverButtonMainMenu,
        BlockHit
    }
}