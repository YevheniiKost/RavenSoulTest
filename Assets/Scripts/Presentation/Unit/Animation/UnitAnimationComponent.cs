using UnityEngine;

using RavenSoul.Utilities.Logger;

namespace RavenSoul.Presentation.Unit
{
    public class UnitAnimationComponent : MonoBehaviour, IAnimationComponent
    {
        const string AngleName = "Angle";
        const string AttackName = "Attack";
        const string IsMovingName = "IsMoving";
        const string DeathName = "Death";
        
        const string MovementAnimationSpeedName = "MovementAnimationSpeed";
        const string AttackAnimationSpeedName = "AttackAnimationSpeed";
        const string IdleAnimationSpeedName = "IdleAnimationSpeed";
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorOverrideController _animatorOverrideController;

        private readonly int _angleHash = Animator.StringToHash(AngleName);
        private readonly int _attackHash = Animator.StringToHash(AttackName);
        private readonly int _isMovingHash = Animator.StringToHash(IsMovingName);
        private readonly int _deathHash = Animator.StringToHash(DeathName);
        
        private readonly int _movementAnimationSpeedHash = Animator.StringToHash(MovementAnimationSpeedName);
        private readonly int _attackAnimationSpeedHash = Animator.StringToHash(AttackAnimationSpeedName);
        private readonly int _idleAnimationSpeedHash = Animator.StringToHash(IdleAnimationSpeedName);

        private void Awake()
        {
            if (_animator == null)
            {
                MyLogger.LogError($"Animator is not set on {gameObject.name}");
            }

            if (_animatorOverrideController != null && _animator != null)
            {
                _animator.runtimeAnimatorController = _animatorOverrideController;
            }
        }
        
        public void PlayMoveAnimation(Vector2 input)
        {
            float angle = Mathf.Atan2(-input.y, input.x) * Mathf.Rad2Deg + 90f;
            
            if(angle < 0)
                angle += 360;
            
            if(angle >= 360)
                angle -= 360;
            
            _animator.SetFloat(_angleHash, angle);
            _animator.SetBool(_isMovingHash, input.magnitude > 0);
        }
        
        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(_attackHash);
            _animator.SetBool(_isMovingHash, false);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger(_deathHash);
        }
        
        public void SetAnimationSpeed(AnimationState animationState, float animationSpeed = 1)
        {
            animationSpeed = Mathf.Clamp(animationSpeed, 0, float.MaxValue);
            switch (animationState)
            {
                case AnimationState.Move:
                    _animator.SetFloat(_movementAnimationSpeedHash, animationSpeed);
                    break;
                case AnimationState.Attack:
                    _animator.SetFloat(_attackAnimationSpeedHash, animationSpeed);
                    break;
                case AnimationState.Idle:
                    _animator.SetFloat(_idleAnimationSpeedHash, animationSpeed);
                    break;
                default:
                    MyLogger.LogError($"AnimationState {animationState} is not implemented");
                    break;
            }
        }

        public void PlayIdleAnimation()
        {
            _animator.SetBool(_isMovingHash, false);
        }
    }
}