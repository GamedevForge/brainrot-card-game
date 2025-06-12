using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class PlayerTurnState : IAsyncEnterState
    {
        private readonly AttackController _attackController;
        private readonly CardHandlerRepository _cardHandlerRepository;
        private readonly InputController _inputController;
        private readonly BaseStateController _baseStateController;
        private readonly GameplayModel _gameplayModel;

        public PlayerTurnState(
            AttackController attackController,
            InputController inputController,
            BaseStateController baseStateController,
            GameplayModel gameplayModel,
            CardHandlerRepository cardHandlerRepository)
        {
            _attackController = attackController;
            _inputController = inputController;
            _baseStateController = baseStateController;
            _gameplayModel = gameplayModel;
            _cardHandlerRepository = cardHandlerRepository;
        }

        public async UniTask AsyncEnter()
        {           
            _inputController.EnableInput();
            await _cardHandlerRepository.AsyncWaitToAttack();
            _inputController.DisableInput();

            await _attackController.AttackEnemy();

            if (_gameplayModel.AllEnemyCardDead())
                _baseStateController.Translate(typeof(NextWaveState)).Forget();
            else if (_gameplayModel.CurrentWave !=
                _gameplayModel.LevelModel[_gameplayModel.LevelModel.Count - 1])
            {
                _baseStateController.Translate(typeof(EnemyTurnState)).Forget();
            }
            else
                _baseStateController.Translate(typeof(EndLevelState)).Forget();
        }
    }
}