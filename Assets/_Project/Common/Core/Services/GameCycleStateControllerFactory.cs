using Project.Configs;
using Project.Core.GameCycle;
using Project.Core.Gameplay;
using Project.Core.Services.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.Services
{
    public class GameCycleStateControllerFactory
    {
        private readonly MenuWindowController _menuWindowController;
        private readonly WinWindowController _winWindowController;
        private readonly LoseWindowController _loseWindowController;
        private readonly ShadowPopup _shadowPopup;
        private readonly InputController _inputController;
        private readonly GameObject _backgorundGameObject;
        private readonly LevelsData _levelsData;
        private readonly LevelProgress _levelProgress;
        private readonly GameplayController _gameplayController;
        private readonly CardCreatedData _playerCard;

        private GameplayState _gameplayState;

        public GameCycleStateControllerFactory(
            MenuWindowController menuWindowController,
            WinWindowController winWindowController,
            LoseWindowController loseWindowController,
            ShadowPopup shadowPopup,
            InputController inputController,
            GameObject backgorundGameObject,
            LevelsData levelsData,
            LevelProgress levelProgress,
            GameplayController gameplayController,
            CardCreatedData playerCard)
        {
            _menuWindowController = menuWindowController;
            _winWindowController = winWindowController;
            _loseWindowController = loseWindowController;
            _shadowPopup = shadowPopup;
            _inputController = inputController;
            _backgorundGameObject = backgorundGameObject;
            _levelsData = levelsData;
            _levelProgress = levelProgress;
            _gameplayController = gameplayController;
            _playerCard = playerCard;
        }

        public BaseStateController Create()
        {
            _gameplayState = new GameplayState(
                    _shadowPopup,
                    _backgorundGameObject,
                    _playerCard);

            IState[] states = new IState[]
            {
                new MenuState(
                    _menuWindowController,
                    _shadowPopup,
                    _inputController,
                    _levelProgress),
                _gameplayState,
                new WinState(
                    _winWindowController,
                    _backgorundGameObject,
                    _inputController,
                    _shadowPopup,
                    _levelProgress,
                    _levelsData,
                    _gameplayController),
                new LoseState(
                    _loseWindowController,
                    _backgorundGameObject,
                    _inputController,
                    _shadowPopup,
                    _gameplayController),
            };

            BaseStateController stateController = new(
                states,
                new ITransition[]
                {
                    new Transition<MenuState, GameplayState>(),
                    new Transition<GameplayState, WinState>(),
                    new Transition<GameplayState, LoseState>(),
                    new Transition<WinState, MenuState>(),
                    new Transition<LoseState, MenuState>(),
                },
                typeof(MenuState));

            foreach (var state in states)
            {
                if (state is ISetableState<BaseStateController> setableState)
                    setableState.Set(stateController);
            }

            return stateController;
        }

        public void SetGameplayStateController(BaseStateController stateController) =>
            _gameplayState.SetGameplayStateController(stateController);
    }
}