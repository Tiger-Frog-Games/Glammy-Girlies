using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TigerFrogGames
{
    [RequireComponent(typeof(Image))]
    public class CustomButton : Selectable, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler
    {
        /* ------- Variables ------- */

      [SerializeField] float fadeDuration = 0.1f;
       private Image targetGraphic;
      
     
       //[SerializeField] private SoundData onHoverSound;
       
       public ButtonClickEvent onClick;
       public ButtonHoverEvent onHover;
       public ButtonHoverEvent onUnHover;
       
       private bool isHovered = false;
       
       /* ------- Unity Methods ------- */

       private void Awake()
       {
           targetGraphic = GetComponent<Graphic>() as Image;

          // if (!isInteractable) StartColorTween(, true);
       }

       protected override void Reset()
       {
           base.Reset();
           
           var image = GetComponent<Image>();
           if (image == null)
           {
               image = gameObject.AddComponent<Image>();
           }
           
           targetGraphic = image;
       }


       //taken from unity button.
       //I might need to implement ISubmit / other functions to get it working on gamepad
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }
        
        private void Press()
        {
            //if (!isActiveAndEnabled || !isInteractable)
               // return;

            UISystemProfilerApi.AddMarker("Button.onClick", this);
            onClick.Invoke();
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            
            if (!isActiveAndEnabled )
                return;
            
            isHovered = true;
            
            DoStateTransition(currentSelectionState, false);
            
            onHover?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            
            if (!isActiveAndEnabled || !isHovered)
                return;
            
            isHovered = false;
            
            DoStateTransition(currentSelectionState, false);
            onUnHover?.Invoke();
        }
        
        private void StartColorTween(Color targetColor, bool instant)
        {
            if (targetGraphic == null)
                return;

            targetGraphic.CrossFadeColor(targetColor, instant ? 0f : fadeDuration, true, true);
        }
        
    }
    
    [Serializable] public class ButtonClickEvent : UnityEvent{}
    
    [Serializable] public class ButtonHoverEvent : UnityEvent {}
    
    [Serializable] public class ButtonUnHoverEvent : UnityEvent {}
    
}