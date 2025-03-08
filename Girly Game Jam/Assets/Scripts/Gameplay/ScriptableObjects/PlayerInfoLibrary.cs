using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(fileName = "new PlayerInfo", menuName = "ScriptableObject/Libray/PlayerInfo")]
    public class PlayerInfoLibrary : ScriptableObjectSingleton<PlayerInfoLibrary>
    {
        
        public Color AesticOneColor;
        
        public Color AesticTwoColor;
        public Color AesticBoth;

        public Color GetColorByTeam(PlayerTeam playerTeam)
        {
            switch (playerTeam)
            {
                case PlayerTeam.AesticOne:
                    return AesticOneColor;
                case PlayerTeam.AesticTwo:
                    return AesticTwoColor;
                case PlayerTeam.Both:
                    return AesticBoth;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerTeam), playerTeam, null);
            }
        }
    }
}