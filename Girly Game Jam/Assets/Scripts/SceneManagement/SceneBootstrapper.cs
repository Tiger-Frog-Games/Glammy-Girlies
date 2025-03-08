using UnityEngine;
using UnityEngine.SceneManagement;

namespace TigerFrogGames
{
    public class SceneBootstrapper : MonoBehaviour
    {
        
        //This force unloads all other scenes and just loads the "core" scene which lives for the lifetime of the entire game
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static async void Initialize()
        {
            Debug.Log("SceneBootstrapper initialized");
            await SceneManager.LoadSceneAsync("Core", LoadSceneMode.Single);

            Debug.Log("Game has started!");
            
        }
    }
}