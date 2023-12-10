using RavenSoul.Utilities.StateMachine;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class AttackState : EnemyBaseState
    {
        private ICharacterView _target;
        private readonly float _attackRadius;
        
        public AttackState(EnemyAIInput aiInput, IStateMachine stateMachine, float attackRadius) : base(aiInput, stateMachine)
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
            if (_target == null || !_target.Model.Attributes.IsAlive)
            {
                AIInput.Target = null;
                StateMachine.SwitchTo<WanderState>();
                return;
            }
            
            if (Vector2.Distance(AIInput.transform.position, _target.GameObject.transform.position) > _attackRadius)
            {
                StateMachine.SwitchTo<ChaseState>();
                return;
            }

            AIInput.SetMoveRelease();
            AIInput.SetAttackInput();
        }
    }
}