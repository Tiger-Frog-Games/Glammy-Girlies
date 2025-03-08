using System;
using System.Collections;
using OWS.ObjectPooling;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TigerFrogGames
{
    public class ScoreTextPopup : MonoBehaviour, IPoolable<ScoreTextPopup>
    {
        [Header("Damage Label")]
        [SerializeField] private TMP_Text displayText;
        
        [SerializeField, Range(0,1)] private float startColorFadeAtPercent = .8f;
        
        [Header("Movement Curve")]
        
        [SerializeField] private AnimationCurve easeCurve;
        [Range(0.8f, 1.5f), SerializeField] public float displayDuration = 1f;

        [SerializeField] private Vector2 highPointOffset = new (-350, 500);
        [SerializeField] private Vector2 lowPointOffSet = new (-100, 500);
        [SerializeField] private float heightVariationMax = 150;
        [SerializeField] private float heightVariationMin = 50;

        private Vector3 highPointOffsetBasedOnDirection = Vector3.zero;
        private Vector3 dropPointOffsetBaseOnDirection = Vector3.zero;
        private bool isGoingLeft;

        private float startingTextSize;
        
        [Header("Editor Gizmos")]
        [SerializeField] private bool displayGizmos;
        [SerializeField, Range(1, 30)] private int gizmoResolution = 20;
        [SerializeField] private Vector3 startPositionForVisualation = Vector3.zero;

        private bool isPaused;
        
        private Action<ScoreTextPopup> returnToPool;
        public void Initialize(Action<ScoreTextPopup> returnAction)
        {
            returnToPool = returnAction;

            isPaused = GameStateManager.Instance.CurrentGameState != GameState.Paused;
            
            OrientCurveBasedOnDirection();
            startingTextSize = displayText.fontSize;
        }

        public void ReturnToPool()
        {
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            
            returnToPool?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            if(!displayGizmos) return;

            OrientCurveBasedOnDirection();

            Vector3 start = transform.position;

            if (Application.isPlaying) start = startPositionForVisualation;

            var heightVariation = heightVariationMax - heightVariationMin;

            Vector3 highPoint = start + highPointOffsetBasedOnDirection + new Vector3(0, heightVariation, 0);
            Vector3 dropPoint = highPoint + dropPointOffsetBaseOnDirection;

            int colorChangeIndex = (int)startColorFadeAtPercent * gizmoResolution;

            Gizmos.color = Color.red;

            Vector3 prevPoint = start;

            for (int i = 1; i < gizmoResolution; i++)
            {
                float time = i / (float)gizmoResolution;
                Vector3 nextPoint = CalculateBezierPoint(time, start, highPoint, dropPoint);
                
                if(i >= colorChangeIndex) Gizmos.color = Color.yellow;
                
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }
        }
       
        public void Display(Vector3 position, bool isGoingLeftIn, float startingSize,  int score, TMP_ColorGradient gradient)
        {
            transform.position = position;
            startPositionForVisualation = position;
            this.isGoingLeft = isGoingLeftIn;
            
            displayText.SetText(score.ToString());
            
            displayText.fontSize = startingSize;
    
            displayText.enableVertexGradient = true;
            displayText.colorGradientPreset = gradient;
            displayText.alpha = startColorFadeAtPercent;
            
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);

            moveCoroutine = StartCoroutine(Move());

        }

        private Coroutine moveCoroutine;
        private IEnumerator Move()
        {
            float time = 0;
            float fadeStartTime = startColorFadeAtPercent * displayDuration;
            
            OrientCurveBasedOnDirection();
            
            Vector3 start = transform.position;
            
            var heightVariation = Random.Range(heightVariationMin, heightVariationMax);
            Vector3 variation = new Vector3(0, heightVariation, 0);
            
            Vector3 highPoint = (start + highPointOffsetBasedOnDirection + variation);
            Vector3 dropPoint = highPoint + dropPointOffsetBaseOnDirection;
        
            while (time < displayDuration)
            {
                time += Time.deltaTime;
            
                float progess = time / displayDuration;
                float easedTime = easeCurve.Evaluate(progess);
            
                if (time > fadeStartTime)
                {
                    Color color = displayText.color;
                    float newAlpha = Mathf.Lerp(1, 0, (time - fadeStartTime) / (displayDuration - fadeStartTime));                    
                    color.a = newAlpha;
                    displayText.color = color;
                }
            
                transform.position = CalculateBezierPoint(easedTime, start, highPoint, dropPoint);
            
                yield return null;
            }

            ReturnToPool();
        }

        private Vector3 CalculateBezierPoint(float progress, Vector3 start, Vector3 control, Vector3 end)
        {
            float remainingPath = 1 - progress;
            
            Vector3 currentLocation = remainingPath * remainingPath * start;
            currentLocation += 2 * remainingPath * progress * control;
            currentLocation += progress * progress * end;

            return currentLocation;
        }

        private void OrientCurveBasedOnDirection()
        {
            highPointOffsetBasedOnDirection = highPointOffset;
            dropPointOffsetBaseOnDirection = lowPointOffSet;
            
            if(isGoingLeft) return;

            highPointOffsetBasedOnDirection.x = -highPointOffsetBasedOnDirection.x;
            dropPointOffsetBaseOnDirection.x = -dropPointOffsetBaseOnDirection.x;
        }
    }
}