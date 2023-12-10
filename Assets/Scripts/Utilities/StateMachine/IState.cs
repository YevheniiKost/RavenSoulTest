namespace RavenSoul.Utilities.StateMachine
{
    public interface IState
    {
        void Prepare();
        void Enter();
        void Exit();
        void Update();
    }
}
