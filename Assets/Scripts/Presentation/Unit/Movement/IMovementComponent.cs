using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public interface IMovementComponent
    {
        void Move(Vector2 input);
        void StopMoving();
        void SetMovementSpeed(float movementSpeed);
    }
}