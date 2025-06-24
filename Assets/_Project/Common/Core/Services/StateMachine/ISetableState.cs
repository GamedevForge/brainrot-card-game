namespace Project.Core.Services.StateMachine
{
    public interface ISetableState<T>
    {
        void Set(T data);
    }
}