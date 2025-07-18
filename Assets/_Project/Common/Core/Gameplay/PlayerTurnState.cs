﻿using Cysharp.Threading.Tasks;
using Project.Core.Services;
using Project.Core.Services.StateMachine;

namespace Project.Core.Gameplay
{
    public class PlayerTurnState : IAsyncEnterState, ISetableState<BaseStateController>
    {
        private readonly AttackController _attackController;
        private readonly CardHandlerRepository _cardHandlerRepository;
        private readonly InputController _inputController;
        private readonly GameplayModel _gameplayModel;
        private readonly CardCreatedData _playerCard;
        
        private BaseStateController _gameplayStateController;

        public PlayerTurnState(
            AttackController attackController,
            InputController inputController,
            GameplayModel gameplayModel,
            CardHandlerRepository cardHandlerRepository,
            CardCreatedData playerCard)
        {
            _attackController = attackController;
            _inputController = inputController;
            _gameplayModel = gameplayModel;
            _cardHandlerRepository = cardHandlerRepository;
            _playerCard = playerCard;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {           
            _inputController.EnableInput();
            await _cardHandlerRepository.AsyncWaitToAttack();
            _inputController.DisableInput();

            await _attackController.AttackEnemy();

            if (_playerCard.Health.IsAlive == false)
            {
                _gameplayStateController.Translate(typeof(EndLevelState)).Forget();
            }
            else if (_gameplayModel.CurrentWave.CardCreatedDatas.Count < 
                _gameplayModel.CurrentWaveConfig.CardDatas.Length)
            {
                if (_gameplayModel.CurrentWave !=
                    _gameplayModel.LevelModel[_gameplayModel.LevelModel.Count - 1])
                {
                    _gameplayStateController.Translate(typeof(NextWaveState)).Forget();
                }
                else
                {
                    _gameplayStateController.Translate(typeof(EndLevelState)).Forget();
                }
            }
            else
                _gameplayStateController.Translate(typeof(EnemyTurnState)).Forget();
        }
    }
}