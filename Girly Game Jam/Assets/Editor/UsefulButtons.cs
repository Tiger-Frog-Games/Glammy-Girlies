using TigerFrogGames;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UnityEditor
{
    public class UsefulButtons : EditorWindow
    {
        private VisualElement ButtonToggleGameplayButtons;
        private VisualElement ButtonToggleUI;
        private VisualElement ButtonToggleCheatUI;
        private VisualElement ButtonSelectPlayer;

        [MenuItem("Tiger Frog Games/Open Useful Buttons Window")]
        public static void ShowExample()
        {
            UsefulButtons wnd = GetWindow<UsefulButtons>();
            wnd.titleContent = new GUIContent("Tiger Frog Games Useful Buttons");
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            root.Add(new Label("Scene Toggling"));
            
            ButtonToggleGameplayButtons = new Button(ToggleGameplay);
            ButtonToggleGameplayButtons.Add(new Label("Toggle Gameplay"));
            
            root.Add(ButtonToggleGameplayButtons);
            
            ButtonToggleUI = new Button(ToggleUI);
            ButtonToggleUI.Add(new Label("Toggle UI"));

            root.Add(ButtonToggleUI);

            ButtonToggleCheatUI = new Button(ToggleCheatUI);
            ButtonToggleCheatUI.Add(new Label("Toggle Cheat UI"));

            root.Add(ButtonToggleCheatUI);

            
            root.Add(new Label("Gameplay Helpers"));
            
            ButtonSelectPlayer = new Button(SelectPlayer);
            ButtonSelectPlayer.Add(new Label("Select Player"));

            ButtonSelectPlayer.SetEnabled(EditorApplication.isPlaying);

            root.Add(ButtonSelectPlayer);
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            EditorSceneManager.sceneClosed += CheckForGameSceneLoadManager;
            EditorSceneManager.sceneOpened += CheckForGameSceneLoadManager;
            UpdateSceneLoadButtons();
        }
        
        private void OnDestroy()
        {
            EditorSceneManager.sceneClosed -= CheckForGameSceneLoadManager;
            EditorSceneManager.sceneOpened -= CheckForGameSceneLoadManager;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange newState)
        {
            if (newState == PlayModeStateChange.EnteredPlayMode)
            {
                ButtonSelectPlayer.SetEnabled(true);
                
            }
            else if (newState == PlayModeStateChange.EnteredEditMode)
            {
                ButtonSelectPlayer.SetEnabled(false);
            }
        }

        private void UpdateSceneLoadButtons()
        {
            /*GameSceneLoadManager gameSceneLoadManager = FindFirstObjectByType<GameSceneLoadManager>();
            
            if (gameSceneLoadManager == null)
            {
                ButtonToggleGameplayButtons.SetEnabled(false);
                ButtonToggleUI.SetEnabled(false);
                ButtonToggleUI.SetEnabled(false);
            }
            else
            {
                ButtonToggleGameplayButtons.SetEnabled(true);
                ButtonToggleUI.SetEnabled(true);
                if(gameSceneLoadManager.HasCheatScene())
                {
                    ButtonToggleUI.SetEnabled(true);
                }
            }*/
        }
        
        private void CheckForGameSceneLoadManager(Scene scene, OpenSceneMode mode)
        {
            UpdateSceneLoadButtons();
        }
        private void  CheckForGameSceneLoadManager(Scene arg0)
        {
            UpdateSceneLoadButtons();
        }
        
        private void ToggleGameplay()
        {
           // GameSceneLoadManager.Instance.ToggleStartingGameplayScenes();
        }
        
        private void ToggleUI()
        {
           // GameSceneLoadManager.Instance.ToggleUI();
        }

        private void ToggleCheatUI()
        {
            //GameSceneLoadManager.Instance.ToggleCheatUI();
        }

        private void SelectPlayer()
        {
            //Object[] temp2 = { PlayerManager.Instance.GetPlayerUnit().gameObject };
            //Selection.objects = temp2;
            //EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}