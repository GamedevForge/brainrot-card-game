using Cysharp.Threading.Tasks;
using Project.Ai;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class EnemyTurnState : IAsyncEnterState
    {
        private readonly AiActor _aiActor;
        private readonly BaseStateController _stateController;
        private readonly GameplayModel _gameplayModel;

        public UniTask AsyncEnter()
        {
            throw new System.NotImplementedException();
        }
    }
}