using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TigerFrogGames
{ 
    public class SceneLoader : PersistentSingleton<SceneLoader>
    {
        /* ------- Variables ------- */


        [SerializeField] private Camera loadingCamera;

        [Header("Loading Bar")]
        
        [SerializeField] private Canvas loadingCanvas;
        [SerializeField] private Image loadingBar;
        [SerializeField] private float fillSpeed = .05f;
        
        [SerializeField] private SceneGroup mainMenuSceneGroup;
        [SerializeField] private SceneGroup initialGameplaySceneGroup;
        
        private float targetProcess;
        private bool isLoading;

        public readonly SceneLoadManager sceneLoadManager = new SceneLoadManager(); 
        
#if UNITY_EDITOR
        [Header("Development")]
        [SerializeField] private bool startInGameplay = false;
#endif
        
        /* ------- Unity Methods ------- */

        private async void Start()
        {
#if UNITY_EDITOR
            if (startInGameplay)
            {
                await LoadGameplay();
            }
            else
            {
#endif
                await LoadMainMenu();
#if UNITY_EDITOR
            }
#endif
        }
        
#if UNITY_EDITOR
        private void Awake()
        {
            if (displayLevelLoading)
            {
                sceneLoadManager.OnSceneLoaded += sceneName => Debug.Log($"Loaded: {sceneName}");
                sceneLoadManager.OnSceneUnloaded += sceneName => Debug.Log($"Unloaded: {sceneName}");
                sceneLoadManager.OnSceneGroupLoaded += groupName => Debug.Log($"Scene Group Loaded: {groupName}");
            }
        }
#endif
        
        private void Update()
        {
            if(!isLoading) return;
            
            float currentFillAmount = loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - targetProcess);
            
            float dynamicFillSpeed = progressDifference * fillSpeed;
            
            loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProcess,  Time.deltaTime * dynamicFillSpeed);
        }

        /* ------- Methods ------- */

        public async Task LoadGameplay()
        {
            await LoadSingleSceneGroup(initialGameplaySceneGroup);
            GameFlowManager.Instance.StartIntroGame();
        }
        
        public async Task LoadMainMenu()
        {
            await LoadSingleSceneGroup(mainMenuSceneGroup);
        }

        private async Task LoadSingleSceneGroup(SceneGroup sceneGroupToLoad)
        {
            loadingBar.fillAmount = 0;
            targetProcess = 0f;
            
            LoadProgress progress = new LoadProgress();
            progress.Progressed += target => targetProcess = Mathf.Max(target, targetProcess);

            EnableLoadingCanvas();

            await sceneLoadManager.LoadSceneGroupAsync(sceneGroupToLoad, progress, true, true);
            
            EnableLoadingCanvas(false);
        }

        private void EnableLoadingCanvas(bool enable = true)
        {
            isLoading = enable;
            loadingCamera.gameObject.SetActive(enable);
            loadingCanvas.gameObject.SetActive(enable);
        }
        #region Testing
        [Header("Debugging")]
        
        [SerializeField] private bool showDeBugOptions = false;
        
        [SerializeField] private bool displayLevelLoading = false;
        
        [ContextMenu("Unload All Scenes")]
        public void testUnloadAllScenes()
        {
            _ = sceneLoadManager.UnloadScenes();
        }
        
        [ContextMenu("Unload All Scenes including gameplay")]
        public void testUnloadAllScenesIncludingGameplay()
        {
            _ = sceneLoadManager.UnloadScenes(true);
        }
    
        #endregion
    }

    public class LoadProgress : IProgress<float>
    {
        public event Action<float> Progressed;

        private const float ratio = 1f;

        public void Report(float value)
        {
            Progressed?.Invoke(value/ratio);
        }
    }
}