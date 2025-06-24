using Project.Ai;
using Project.Core.Gameplay;
using Project.Core.Services.StateMachine;
using Project.Core.UI;
using Project.Core.UpgradeSystem;
using UnityEngine;

namespace Project.Core.Services
{
    public class GameplayStateControllerFactory
    {
        private readonly AttackController _attackController;
        private readonly InputController _inputController;
        private readonly GameplayModel _gameplayModel;
        private readonly CardHandlerRepository _cardHandlerRepository;
        private readonly AiActor _aiActor;
        private readonly UpgradeController _upgradeController;
        private readonly UpgradeControllerView _upgradeControllerView;
        private readonly GameplayController _gameplayController;
        private readonly BaseStateController _gameCycleStateController;
        private readonly LevelProgress _levelProgress;
        private readonly CardCreatedData _playerCard;
        private readonly AudioSource _audioSource;

        public GameplayStateControllerFactory(
            AttackController attackController,
            InputController inputController,
            GameplayModel gameplayModel,
            CardHandlerRepository cardHandlerRepository,
            AiActor aiActor,
            UpgradeController upgradeController,
            UpgradeControllerView upgradeControllerView,
            GameplayController gameplayController,
            BaseStateController gameCycleStateController,
            LevelProgress levelProgress,
            CardCreatedData playerCard,
            AudioSource audioSource)
        {
            _attackController = attackController;
            _inputController = inputController;
            _gameplayModel = gameplayModel;
            _cardHandlerRepository = cardHandlerRepository;
            _aiActor = aiActor;
            _upgradeController = upgradeController;
            _upgradeControllerView = upgradeControllerView;
            _gameplayController = gameplayController;
            _gameCycleStateController = gameCycleStateController;
            _levelProgress = levelProgress;
            _playerCard = playerCard;
            _audioSource = audioSource;
        }

        public BaseStateController Create()
        {
            IState[] states = new IState[] 
            {
                new PlayerTurnState(
                        _attackController,
                        _inputController,
                        _gameplayModel,
                        _cardHandlerRepository,
                        _playerCard),
                    new EnemyTurnState(
                        _aiActor,
                        _gameplayModel,
                        _attackController),
                    new EndLevelState(
                        _gameplayModel,
                        _gameCycleStateController,
                        _inputController),
                    new NextWaveState(
                        _upgradeController,
                        _upgradeControllerView,
                        _inputController,
                        _gameplayController,
                        _gameplayModel),
                    new StartLevelState(
                        _gameplayController,
                        _inputController,
                        _levelProgress,
                        _playerCard,
                        _audioSource)
            };
            
            BaseStateController stateController = new(
                states,
                new ITransition[]
                { 
                    new Transition<StartLevelState, StartLevelState>(),
                    new Transition<StartLevelState, PlayerTurnState>(),
                    new Transition<PlayerTurnState, EnemyTurnState>(),
                    new Transition<PlayerTurnState, NextWaveState>(),
                    new Transition<PlayerTurnState, EndLevelState>(),
                    new Transition<EnemyTurnState, PlayerTurnState>(),
                    new Transition<EnemyTurnState, EndLevelState>(),
                    new Transition<NextWaveState, PlayerTurnState>(),
                    new Transition<PlayerTurnState, EndLevelState>()
                },
                typeof(PlayerTurnState));

            foreach (var state in states)
            {
                if (state is ISetableState<BaseStateController> setableState)
                    setableState.Set(stateController);
            }

            return stateController;
        }
    }
}