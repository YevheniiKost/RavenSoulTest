using RavenSoul.Utilities.StateMachine;

namespace RavenSoul.Presentation.Unit
{
    public abstract class EnemyBaseState : IState
    {
        private readonly EnemyAIInput _aiInput;
        private readonly IEnemyView _enemyView;
        private readonly IStateMachine _stateMachine;
        
        protected EnemyAIInput AIInput => _aiInput;
        protected IStateMachine StateMachine => _stateMachine;

        protected EnemyBaseState(EnemyAIInput aiInput, IStateMachine stateMachine)
        {
            _aiInput = aiInput;
            _stateMachine = stateMachine;
        }

        public abstract void Prepare();
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}