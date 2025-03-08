using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static MyPlayerInputs;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "ScriptableObject/InputReaders/Girly Game InputReader")]
    public class InputReaderGirlyGame : InputReader<MyPlayerInputs>, IPlayerActions
    {
        public event UnityAction<Vector2> Aim = delegate { };
        public event UnityAction<bool> Fire = delegate { };
        public event UnityAction<bool> Pause = delegate { };
       
        
        protected override void SetUpInputActions()
        {
            if (inputActions == null)
            {
                inputActions = new MyPlayerInputs();
                inputActions.Player.SetCallbacks(this);
            }
        }
        
        protected override void Destroy()
        {
            if (inputActions != null)
            {
                inputActions.Player.Disable();
            }
        }
        
        public Vector2 AimDirection => inputActions.Player.Aim.ReadValue<Vector2>();
        
        public void OnAim(InputAction.CallbackContext context)
        {
            Aim.Invoke(context.ReadValue<Vector2>());
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            switch (context.phase) {
                case InputActionPhase.Started:
                    Fire.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Fire.Invoke(false);
                    break;
            }
        }
        
        
        public void OnPause(InputAction.CallbackContext context)
        {
            switch (context.phase) {
                case InputActionPhase.Started:
                    Pause.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Pause.Invoke(false);
                    break;
            }
        }
        
        public void OnUIMove(InputAction.CallbackContext context)
        {
            //throw new System.NotImplementedException();
        }

 
    }
}