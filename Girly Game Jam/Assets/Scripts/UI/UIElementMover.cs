using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace TigerFrogGames
{
    public class UIElementMover : MonoBehaviour
    {
        public event Action<String> OnMovementOver = delegate { };
        
        /* ------- Variables ------- */
        
        [Header("Set up")]
        [SerializeField] private GameObject UIElementToMove;
        
        [SerializeField] List<MovementEntry> allMovements;
        
        private CanvasGroup canvasGroup;
        private Vector2 startPosition;
        
        /* ------- Unity Methods ------- */

        private void OnEnable()
        {
            startPosition = UIElementToMove.transform.position;
            canvasGroup = UIElementToMove.GetComponent<CanvasGroup>();
        }

        /* ------- Methods ------- */

        public bool Move(string movementKey)
        {
            var foundMoveData = allMovements.FirstOrDefault(s => s.Name.Equals(movementKey));
            
            if (foundMoveData != null)
            {
                if (_animateElementCoroutine != null)
                {
                    StopCoroutine(_animateElementCoroutine);
                }

                _animateElementCoroutine = StartCoroutine(AnimateElement(movementKey, foundMoveData.Movement));
                return true;
            }
            Debug.LogWarning($"Could not find movement key: {movementKey}", this);
            return false;
        }
        
        Coroutine _animateElementCoroutine;
        private IEnumerator AnimateElement(string name, MovementData movementData)
        {
            Vector2 initialPosition;
            Vector2 targetPosition;
            float elapsedTime = 0f;
            
            if (movementData.goToStart)
            {
                initialPosition  = startPosition + movementData.distanceToMove;
                targetPosition = startPosition;
            }
            else
            {
                initialPosition = startPosition;
                targetPosition = startPosition + movementData.distanceToMove;
            }

            while (elapsedTime < movementData.duration)
            {
                float evaluatePositionAtTime = movementData.easingCurve.Evaluate(elapsedTime / movementData.duration);
                UIElementToMove.transform.position = Vector2.Lerp(initialPosition, targetPosition, evaluatePositionAtTime);
                
                canvasGroup.alpha = movementData.fadoutCurve.Evaluate(elapsedTime / movementData.duration);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            UIElementToMove.transform.position = targetPosition;
            canvasGroup.alpha = movementData.fadoutCurve.Evaluate(1);
            
            OnMovementOver.Invoke(name);
        }
        
        
        
        private void OnValidate()
        {
            SetGizmoMovementData(); // this makes is to the grayed out gizmo is the same as the info in the dictionary
        }
        
        #region Gizmos
        
        [Header("Display Gizmos")]
        [SerializeField] private bool drawGizmos;
        
        private String gizmoDrawEntry;
        
        private MovementData gizmoDrawData;
        
        private void SetGizmoMovementData()
        {
            var foundMoveData = allMovements.FirstOrDefault(s => s.Name.Equals(gizmoDrawEntry));
            
            if (foundMoveData == null)
            {
                gizmoDrawEntry = "";
                gizmoDrawData = default;
                return;
            }
            
            gizmoDrawData = foundMoveData.Movement;
        }
        
        #endregion
        
    }

    [Serializable]
    public class MovementEntry
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public MovementData Movement { get; private set; }
    }
    
    [Serializable]
    public struct MovementData
    {
        public float duration;
        public bool goToStart;
        public Vector2 distanceToMove;
        public AnimationCurve easingCurve;
        public AnimationCurve fadoutCurve;
    }
    
}