namespace RavenSoul.Utilities.StateMachine
{
    public interface IStateMachine
    {
        void SwitchTo<TState>() where TState : IState;
        void AddState<TState>(TState state) where TState : IState;
        void ExitCurrent();
        void Update();
    }
}