using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class LevelManager : Singleton<LevelManager>
    {
        public event Action OnOutOfLevels;

        public event Action OnLevelDoneLoading;
        /* ------- Variables ------- */
        
        [SerializeField] private Transform levelBottomMarker;
        [SerializeField] private BoxCollider2D boundsCollider;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private GameObject levelRoot;
        
        [SerializeField] private GameObject[] levelPrefabs;
        [SerializeField] private int currentLevel = 0;

        private GameObject loadedLevel;
        /* ------- Unity Methods ------- */
        

        /* ------- Methods ------- */

        [ContextMenu("Load Level")]
        public void LoadNextLevel()
        {
            if (currentLevel > levelPrefabs.Length - 1)
            {
                OnOutOfLevels?.Invoke();
                return;
            }


            StartCoroutine(LoadLevelFadeIn());

        }

        private IEnumerator LoadLevelFadeIn()
        {
            levelRoot.SetActive(false);
            loadedLevel = Instantiate(levelPrefabs[currentLevel], Vector3.zero, levelBottomMarker.rotation, levelRoot.transform);

            var allSprites = loadedLevel.GetComponentsInChildren<SpriteRenderer>();
            foreach (var VARIABLE in allSprites)
            {
                VARIABLE.DOFade(0,0);
            }
            
            yield return new WaitForEndOfFrame();
            
            levelRoot.SetActive(true);
            
            foreach (var VARIABLE in allSprites)
            {
                VARIABLE.DOFade(1,.8f);
            }
            yield return new WaitForSeconds(.8f);
            OnLevelDoneLoading?.Invoke();
        }
        
        [ContextMenu("unload level")]
        public void UnloadLevel()
        {
            foreach (var VARIABLE in loadedLevel.GetComponentsInChildren<SpriteRenderer>())
            {
                VARIABLE.DOFade(0, .8f);
            }
        }
        
        public bool IsMouseInsideBounds()
        {
            Vector3 checkPos = mainCamera.ScreenToWorldPoint(Input.mousePosition).With(z: 0);
            return boundsCollider.bounds.Contains(checkPos);
        }
        
        public float GetLevelBottomY()
        {
            return levelBottomMarker.transform.position.y;
        }
        
    }
}