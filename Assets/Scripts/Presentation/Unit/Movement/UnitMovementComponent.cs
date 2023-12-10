using System;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitMovementComponent : MonoBehaviour, IMovementComponent
    {
        public event Action<Vector2> OnMove;

        [SerializeField] private float _initialMovementSpeed = 5f;

        private float _movementSpeed;
        private Rigidbody2D _rigidbody2D;
        private bool _isMovePressed;
        private Vector2 _input;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.freezeRotation = true;
            _rigidbody2D.gravityScale = 0;
            _movementSpeed = _initialMovementSpeed;
        }

        private void FixedUpdate()
        {
            if (!_isMovePressed) return;

            Vector2 input = Vector2.ClampMagnitude(_input, 1);
            Vector2 movement = input * (_movementSpeed * Time.deltaTime);
            Vector2 newPosition = _rigidbody2D.position + movement;
            
            _rigidbody2D.MovePosition(newPosition);
            OnMove?.Invoke(input);
        }

        public void Move(Vector2 input)
        {
            _input = input;

            _isMovePressed = true;
        }

        public void StopMoving()
        {
            _isMovePressed = false;
           _rigidbody2D.velocity = Vector2.zero;
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            _movementSpeed = Mathf.Clamp(movementSpeed, 0, float.MaxValue);
        }
    }
}