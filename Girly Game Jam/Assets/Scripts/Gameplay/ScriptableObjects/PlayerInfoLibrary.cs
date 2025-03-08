using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(fileName = "new PlayerInfo", menuName = "ScriptableObject/Libray/PlayerInfo")]
    public class PlayerInfoLibrary : ScriptableObjectSingleton<PlayerInfoLibrary>
    {
        
        public Color AesticOneColor;
        
        public Color AesticTwoColor;

        public Color GetColorByTeam(PlayerTeam playerTeam)
        {
            switch (playerTeam)
            {
                case PlayerTeam.AesticOne:
                    return AesticOneColor;
                case PlayerTeam.AesticTwo:
                    return AesticTwoColor;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerTeam), playerTeam, null);
            }
        }
    }
}