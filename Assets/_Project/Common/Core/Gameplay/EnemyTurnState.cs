using Cysharp.Threading.Tasks;
using Project.Ai;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class EnemyTurnState : IAsyncEnterState
    {
        private readonly AiActor _aiActor;
        private readonly BaseStateController _stateController;
        private readonly GameplayModel _gameplayModel;
        private readonly AttackController _attackController;

        public EnemyTurnState(
            AiActor aiActor, 
            BaseStateController stateController, 
            GameplayModel gameplayModel, 
            AttackController attackController)
        {
            _aiActor = aiActor;
            _stateController = stateController;
            _gameplayModel = gameplayModel;
            _attackController = attackController;
        }

        public async UniTask AsyncEnter()
        {
            CardCreatedData enemyCard = _aiActor
                .GetCardForAttackPlayer(_gameplayModel
                .CurrentWave
                .CardCreatedDatas
                .ToArray());

            await _attackController.AttackPlayer(enemyCard);

            if (_gameplayModel.PlayerCard.Health.IsAlive)
                _stateController.Translate(typeof(PlayerTurnState)).Forget();
            else
                _stateController.Translate(typeof(EndLevelState)).Forget();
        }
    }
}