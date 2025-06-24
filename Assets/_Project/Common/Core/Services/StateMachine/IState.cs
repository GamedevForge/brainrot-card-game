using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Core.Services.StateMachine
{
    public interface IState
    {
        
    }

    public interface IEnterState : IState
    {
        void Enter();
    }

    public interface IExitState : IState
    {
        void Exit();
    }

    public interface IAsyncEnterState : IState
    {
        UniTask AsyncEnter();
    }

    public interface IAsyncExitState : IState
    {
        UniTask AsyncExit();
    }
}