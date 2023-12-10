using System;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public interface IInputComponent
    {
        event Action<Vector2> OnMoveInput;
        event Action OnMoveRelease;
        event Action OnAttackInput;
        Vector2 GetDirection();
    }
}