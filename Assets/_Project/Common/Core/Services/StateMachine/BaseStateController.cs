using System.Linq;
using System;
using Cysharp.Threading.Tasks;

namespace Project.Core.Services.StateMachine
{
    public class BaseStateController
    {
        private readonly IState[] _states;
        private readonly ITransition[] _transitions;
        private readonly Type _initialState;

        private IState _current;

        public BaseStateController(
            IState[] states,
            ITransition[] transitions,
            Type initialState)
        {
            _states = states;
            _transitions = transitions;
            _initialState = initialState;
        }

        public void Initialize() =>
            Translate(_initialState).Forget();

        public void Dispose()
        {
            foreach (var state in _states)
            {
                if (state is IDisposable disposable)
                    disposable.Dispose();
            }
        }

        public async UniTask Translate(Type type)
        {
            if (_current is IAsyncExitState exitStateAsync)
                await exitStateAsync.AsyncExit();
            else if (_current is IExitState exitState)
                exitState.Exit();

            ITransition transition = _transitions.First(x => x.To == type);
            _current = _states.First(x => x.GetType() == transition.To);

            if (_current is IAsyncEnterState enterStateAsync)
                await enterStateAsync.AsyncEnter();
            else if (_current is IEnterState enterState)
                enterState.Enter();
        }
    }
}