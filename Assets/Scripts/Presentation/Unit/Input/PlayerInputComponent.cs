using System;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class PlayerInputComponent : MonoBehaviour, IInputComponent
    {
        public event Action<Vector2> OnMoveInput;
        public event Action OnMoveRelease;
        public event Action OnAttackInput;

        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string Fire1Button = "Fire1";

        private bool _isMovePressed;
        private Vector2 _direction;
        
        private void Update()
        {
            ProcessMovementInput();

            if (Input.GetButtonDown(Fire1Button))
            {
                OnAttackInput?.Invoke();
            }
        }

        private void ProcessMovementInput()
        {
            Vector2 input = new Vector2(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis));
            if (input.magnitude > 0)
            {
                OnMoveInput?.Invoke(input);
                _direction = input;
                _isMovePressed = true;
            }else if (_isMovePressed)
            {
                OnMoveRelease?.Invoke();
                _isMovePressed = false;
            }
        }
        
        public Vector2 GetDirection()
        {
            return _direction;
        }
    }
}