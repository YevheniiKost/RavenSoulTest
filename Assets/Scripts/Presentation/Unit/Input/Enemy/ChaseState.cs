using RavenSoul.Utilities.StateMachine;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class ChaseState : EnemyBaseState
    {
        private ICharacterView _target;
        private float _attackRadius;
        
        public ChaseState(EnemyAIInput aiInput, IStateMachine stateMachine, float attackRadius) : base(aiInput, stateMachine)
        {
            _attackRadius = attackRadius;
        }

        public override void Prepare()
        {
        }

        public override void Enter()
        {
            _target = AIInput.Target;
            if(_target == null)
            {
                StateMachine.SwitchTo<WanderState>();
            }
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (_target == null)
            {
                StateMachine.SwitchTo<WanderState>();
                return;
            }
            
            Vector2 direction = (_target.GameObject.transform.position - AIInput.transform.position).normalized;
            AIInput.SetMoveInput(direction);
            
            if (Vector2.Distance(AIInput.transform.position, _target.GameObject.transform.position) <= _attackRadius)
            {
                StateMachine.SwitchTo<AttackState>();
            }
        }
    }
}