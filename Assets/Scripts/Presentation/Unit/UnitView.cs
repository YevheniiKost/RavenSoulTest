using UnityEngine;

using RavenSoul.Data;
using RavenSoul.Domain.Unit;
using RavenSoul.Presentation.Interactors;

using RavenSoul.Utilities.Logger;
using RavenSoul.Utilities.Timer;

namespace RavenSoul.Presentation.Unit
{
    public class UnitView<T> : MonoBehaviour where T : UnitBlueprint
    {
        private IMovementComponent _movementComponent;
        private IInputComponent _inputComponent;
        private IAnimationComponent _animationComponent;
        private IAbilitiesComponent _abilitiesComponent;
        private IHitComponent _hitComponent;
        private IImpactComponent _impactComponent;
        private IAttributesComponent _attributesComponent;
        
        private IUnitModel<T> _model;
        private ITimer _attackTimer;
        
        protected IAbilitiesComponent AbilitiesComponent => _abilitiesComponent;
        protected IInputComponent InputComponent => _inputComponent;
        
        protected virtual void Awake()
        {
            _movementComponent = GetComponentInChildren<IMovementComponent>();
            _inputComponent = GetComponentInChildren<IInputComponent>();
            _animationComponent = GetComponentInChildren<IAnimationComponent>();
            _abilitiesComponent = GetComponentInChildren<IAbilitiesComponent>();
            _hitComponent = GetComponentInChildren<IHitComponent>();
            _impactComponent = GetComponentInChildren<IImpactComponent>();
            
            AssertComponents();
        }

        private void OnDestroy()
        {
            _inputComponent.OnMoveInput -= Move;
            _inputComponent.OnAttackInput -= Attack;
            _inputComponent.OnMoveRelease -= StopMoving;
            _hitComponent.OnHit -= OnHit;
            
            if(_model != null)
                _model.Attributes.OnDeath -= OnDeathHandler;
        }

        protected void Setup(IUnitModel<T> model)
        {
            _model = model;
            gameObject.name = model.Name;
            
            _movementComponent.SetMovementSpeed(model.Abilities.MovementSpeed);
            _abilitiesComponent.SetAttackRadius(model.Abilities.AttackSpeed);
            
            _inputComponent.OnMoveInput += Move;
            _inputComponent.OnAttackInput += Attack;
            _inputComponent.OnMoveRelease += StopMoving;
            _hitComponent.OnHit += OnHit;
            
            _model.Attributes.OnDeath += OnDeathHandler;
        }

        protected virtual void OnDeathHandler()
        {
            _animationComponent.PlayDeathAnimation();
        }

        private void OnHit(HitParams obj)
        {
            if (!_model.Attributes.IsAlive)
                return;
            
            _impactComponent.ShowHitEffect();
            _model.Attributes.TakeDamage(obj.Damage);
        }

        private void StopMoving()
        {
            _movementComponent.StopMoving();
            _animationComponent.PlayIdleAnimation();
        }

        private void Attack()
        {
            AssertModel();

            if (_model.Abilities.CanAttack && _model.Attributes.IsAlive)
            {
                _movementComponent.StopMoving();
                _abilitiesComponent.Attack(_inputComponent.GetDirection(), _model.Abilities.AttackSpeed, new HitParams(_model.Abilities.Damage), OnAttackEnd);
                _animationComponent.PlayAttackAnimation();
                _model.Abilities.Attack();
            }
        }

        private void OnAttackEnd()
        {
            _model.Abilities.OnAttackEnd();
        }

        private void Move(Vector2 input)
        {
            AssertModel();

            if (_model.Abilities.CanMove && _model.Attributes.IsAlive)
            {
                _movementComponent.Move(input);
                _animationComponent.PlayMoveAnimation(input);
            }
        }
        
        private void AssertComponents()
        {
            if(_movementComponent == null)
                MyLogger.LogError("No IMovementComponent found on " + gameObject.name);
            if(_inputComponent == null)
                MyLogger.LogError("No IInputComponent found on " + gameObject.name);
            if(_animationComponent == null)
                MyLogger.LogError("No IAnimationComponent found on " + gameObject.name);
            if(_abilitiesComponent == null)
                MyLogger.LogError("No IAbilitiesComponent found on " + gameObject.name);
            if(_hitComponent == null)
                MyLogger.LogError("No IHitComponent found on " + gameObject.name);
            if(_impactComponent == null)
                MyLogger.LogError("No IImpactComponent found on " + gameObject.name);
        }
        
        private void AssertModel()
        {
            if(_model == null)
                MyLogger.LogError("No UnitModel found on " + gameObject.name);
        }
    }
}