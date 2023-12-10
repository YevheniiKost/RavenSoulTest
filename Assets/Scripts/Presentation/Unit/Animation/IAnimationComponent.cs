using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public interface IAnimationComponent
    {
        void PlayMoveAnimation(Vector2 input);
        void PlayAttackAnimation();
        void PlayDeathAnimation();
        void SetAnimationSpeed(AnimationState animationState, float animationSpeed = 1);
        void PlayIdleAnimation();
    }
}