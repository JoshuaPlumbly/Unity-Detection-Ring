internal interface IStateMachine<T>
{
    void Exit();
    void Enter(T state);
}