using Project.Ai;
using Project.Core.Gameplay;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI;
using Project.Core.UpgradeSystem;

namespace Project.Core.Sevices
{
    public class GameplayStateController
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

        public GameplayStateController(
            AttackController attackController, 
            InputController inputController, 
            GameplayModel gameplayModel, 
            CardHandlerRepository cardHandlerRepository, 
            AiActor aiActor, 
            UpgradeController upgradeController, 
            UpgradeControllerView upgradeControllerView, 
            GameplayController gameplayController, 
            BaseStateController gameCycleStateController)
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
        }

        public BaseStateController Create()
        {
            BaseStateController stateController = new();

            stateController = new(
                new IState[] 
                { 
                    new PlayerTurnState(
                        _attackController,
                        _inputController,
                        stateController,
                        _gameplayModel,
                        _cardHandlerRepository),
                    new EnemyTurnState(
                        _aiActor,
                        stateController,
                        _gameplayModel,
                        _attackController),
                    new EndLevelState(
                        _gameplayModel,
                        _gameCycleStateController),
                    new NextWaveState(
                        stateController,
                        _upgradeController,
                        _upgradeControllerView,
                        _inputController,
                        _gameplayController)
                },
                new ITransition[]
                { 
                    new Transition<PlayerTurnState, EnemyTurnState>(),
                    new Transition<PlayerTurnState, NextWaveState>(),
                    new Transition<PlayerTurnState, EndLevelState>(),
                    new Transition<EnemyTurnState, PlayerTurnState>(),
                    new Transition<EnemyTurnState, EndLevelState>(),
                    new Transition<NextWaveState, PlayerTurnState>()
                },
                typeof(PlayerTurnState));

            return stateController;
        }
    }
}