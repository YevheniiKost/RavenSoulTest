using RavenSoul.Utilities.StateMachine;
using UnityEngine;

namespace RavenSoul.Presentation.Unit
{
    public class WanderState : EnemyBaseState
    {
        public WanderState(EnemyAIInput aiInput, IStateMachine stateMachine) : base(aiInput, stateMachine)
        {
        }
        
        private Vector2 _currentDirection;
        private float _currentMovementTimer;

        public override void Prepare()
        {
        }

        public override void Enter()
        {
            AIInput.OnTargetDetected += OnTargetDetected;
        }
        
        public override void Exit()
        {
            AIInput.OnTargetDetected -= OnTargetDetected;
        }

        public override void Update()
        {
            if(_currentMovementTimer <= 0)
            {
                _currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                _currentMovementTimer = GetRandomMovementTimer();
            }
            else
            {
                _currentMovementTimer -= Time.deltaTime;
                AIInput.SetMoveInput(_currentDirection.normalized);
            }
        }

        private static float GetRandomMovementTimer()
        {
            return Random.Range(1f, 3f);
        }

        private void OnTargetDetected(ICharacterView obj)
        {
            StateMachine.SwitchTo<ChaseState>();
        }
    }
}