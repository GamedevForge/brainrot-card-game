using Cysharp.Threading.Tasks;
using Project.Ai;
using Project.Core.Services;
using Project.Core.Services.StateMachine;

namespace Project.Core.Gameplay
{
    public class EnemyTurnState : IAsyncEnterState, ISetableState<BaseStateController>
    {
        private readonly AiActor _aiActor;
        private readonly GameplayModel _gameplayModel;
        private readonly AttackController _attackController;
        
        private BaseStateController _gameplayStateController;

        public EnemyTurnState(
            AiActor aiActor, 
            GameplayModel gameplayModel, 
            AttackController attackController)
        {
            _aiActor = aiActor;
            _gameplayModel = gameplayModel;
            _attackController = attackController;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {
            CardCreatedData enemyCard = _aiActor
                .GetCardForAttackPlayer(_gameplayModel
                .CurrentWave
                .CardCreatedDatas
                .ToArray());

            await _attackController.AttackPlayer(enemyCard);

            if (_gameplayModel.PlayerCard.Health.IsAlive)
                _gameplayStateController.Translate(typeof(PlayerTurnState)).Forget();
            else
                _gameplayStateController.Translate(typeof(EndLevelState)).Forget();
        }
    }
}