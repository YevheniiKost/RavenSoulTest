using System;
using RavenSoul.Data;
using RavenSoul.Utilities.StateMachine;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class EnemyAIInput : MonoBehaviour, IInputComponent
    {
        public event Action<ICharacterView> OnTargetDetected;
        
        public event Action<Vector2> OnMoveInput;
        public event Action OnMoveRelease;
        public event Action OnAttackInput;
        
        private IStateMachine _behaviorStateMachine;
        private ICharacterView _target;
        private EnemyBlueprint _enemyBlueprint;
        private Vector2 _currentDirection;
        
        public ICharacterView Target
        {
            get => _target;
            set => _target = value;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<ICharacterView>(out var characterView))
            {
                _target = characterView;
                OnTargetDetected?.Invoke(_target);
            }
        }

        private void Update()
        {
            _behaviorStateMachine?.Update();
        }
        
        public void SetBlueprint(EnemyBlueprint blueprint)
        {
            _enemyBlueprint = blueprint;
            
            _behaviorStateMachine = new BaseStateMachine();
            WanderState wanderState = new WanderState(this, _behaviorStateMachine);
            ChaseState chaseState = new ChaseState(this, _behaviorStateMachine, blueprint.AttackRadius * 0.8f);
            AttackState attackState = new AttackState(this, _behaviorStateMachine, blueprint.AttackRadius * 0.8f);
            _behaviorStateMachine.AddState(wanderState);
            _behaviorStateMachine.AddState(chaseState);
            _behaviorStateMachine.AddState(attackState);
            _behaviorStateMachine.SwitchTo<WanderState>();
        }

        public Vector2 GetDirection()
        {
           return _currentDirection;
        }
        
        public void SetMoveInput(Vector2 direction)
        {
            _currentDirection = direction;
            OnMoveInput?.Invoke(direction);
        }
        
        public void SetMoveRelease()
        {
            OnMoveRelease?.Invoke();
        }
        
        public void SetAttackInput()
        {
            OnAttackInput?.Invoke();
        }
    }
}