namespace Project.Core.Sevices.StateMachine
{
    public interface ISetableState<T>
    {
        void Set(T data);
    }
}