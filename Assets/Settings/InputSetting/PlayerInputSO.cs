using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BGD.Player
{
    [CreateAssetMenu(menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        
        public Vector2 InputDirection { get; private set; } 

        public void OnMove(InputAction.CallbackContext context)
        {
            if(context.performed)
                InputDirection = context.ReadValue<Vector2>();
        }
    }
}
