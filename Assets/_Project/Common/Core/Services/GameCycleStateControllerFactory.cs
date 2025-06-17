using Project.Core.GameCycle;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class GameCycleStateControllerFactory
    {
        private readonly MenuWindowController _menuWindowController;
        private readonly WinWindowController _winWindowController;
        private readonly LoseWindowController _loseWindowController;
        private readonly ShadowPopup _shadowPopup;
        private readonly InputController _inputController;
        private readonly GameObject _backgorundGameObject;

        private GameplayState _gameplayState;

        public GameCycleStateControllerFactory(
            MenuWindowController menuWindowController,
            WinWindowController winWindowController,
            LoseWindowController loseWindowController,
            ShadowPopup shadowPopup,
            InputController inputController,
            GameObject backgorundGameObject)
        {
            _menuWindowController = menuWindowController;
            _winWindowController = winWindowController;
            _loseWindowController = loseWindowController;
            _shadowPopup = shadowPopup;
            _inputController = inputController;
            _backgorundGameObject = backgorundGameObject;
        }

        public BaseStateController Create()
        {
            _gameplayState = new GameplayState(
                    _shadowPopup,
                    _backgorundGameObject);

            IState[] states = new IState[]
            {
                new MenuState(
                    _menuWindowController,
                    _shadowPopup,
                    _inputController),
                _gameplayState,
                new WinState(
                    _winWindowController,
                    _backgorundGameObject,
                    _inputController,
                    _shadowPopup),
                new LoseState(
                    _loseWindowController,
                    _backgorundGameObject,
                    _inputController,
                    _shadowPopup),
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