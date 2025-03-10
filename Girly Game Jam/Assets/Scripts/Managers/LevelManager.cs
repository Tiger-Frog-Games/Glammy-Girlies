using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class LevelManager : Singleton<LevelManager>
    {
        public event Action OnOutOfLevels;
        public event Action OnLevelVictory;
        
        public event Action OnLevelStartLoading;
        public event Action OnLevelDoneLoading;
        /* ------- Variables ------- */
        
        [SerializeField] private Transform levelBottomMarker;
        [SerializeField] private BoxCollider2D boundsCollider;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private GameObject levelRoot;
        
        [SerializeField] private GameObject[] levelPrefabs;
        [SerializeField] private int currentLevel = 0;

        private List<ItemBubble>  foodBubbles = new ();
        private List<ScoreAbleBlock> scoreAbleBlocks = new();

        private GameObject prevloadedLevel;
        
        public bool DidLevelComplete { get; private set; }
        
        private GameObject loadedLevel;
        /* ------- Unity Methods ------- */
        

        /* ------- Methods ------- */

        public bool IsOutOfLevels()
        {
            return currentLevel > levelPrefabs.Length - 1;
        }

        public void ReloadLevel()
        {
            currentLevel--;
            LoadNextLevel();
        }
        
        [ContextMenu("Load Level")]
        public void LoadNextLevel()
        {
            
            UnloadLevel();
            
            OnLevelStartLoading?.Invoke();
            scoreAbleBlocks.Clear();
            
            
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
            DidLevelComplete = false;
            currentLevel++;
        }
        
       
        public void UnloadLevel()
        {
            prevloadedLevel = loadedLevel;
            StartCoroutine(LoadLevelFadeOut());
        }

        private IEnumerator LoadLevelFadeOut()
        {
            if (prevloadedLevel == null)
            {
                yield break;
            }
            
            foodBubbles.Clear();
            foreach (var VARIABLE in loadedLevel.GetComponentsInChildren<SpriteRenderer>())
            {
                VARIABLE.DOFade(0, .8f);
            }
            yield return new WaitForSeconds(.8f);
            Destroy(prevloadedLevel);
        }
        
        public void AddFoodBubble(ItemBubble foodBubble)
        {
            foodBubbles.Add(foodBubble);
        }

        public void AddScoreAbleBlock(ScoreAbleBlock scoreAbleBlock)
        {
            scoreAbleBlocks.Add(scoreAbleBlock);
        }
        
        public void RemoveFoodBubble(ItemBubble foodBubble)
        {
            foodBubbles.Remove(foodBubble);
            if (foodBubbles.Count == 0)
            {
                OnLevelVictory?.Invoke();
                DidLevelComplete = true;
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

        public async Task CleanUpLevel()
        {
            Debug.Log("Cleaning up level");

            while (scoreAbleBlocks.Count > 0)
            {
                var blockToRemove = scoreAbleBlocks[^1];

                ParticleManager.Instance.PlayBlockCleanUp(blockToRemove.transform.position);
                
                scoreAbleBlocks.RemoveAt(scoreAbleBlocks.Count-1);
                Destroy(blockToRemove.gameObject);
                await Task.Delay(100);
            }
            
            
            Debug.Log("Cleaning done cleaning up level");
        }
    }
}