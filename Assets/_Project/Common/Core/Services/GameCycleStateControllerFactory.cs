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
        private readonly BaseStateController _gameplayStateController;

        public GameCycleStateControllerFactory(
            MenuWindowController menuWindowController, 
            WinWindowController winWindowController, 
            LoseWindowController loseWindowController, 
            ShadowPopup shadowPopup, 
            InputController inputController, 
            GameObject backgorundGameObject, 
            BaseStateController gameplayStateController)
        {
            _menuWindowController = menuWindowController;
            _winWindowController = winWindowController;
            _loseWindowController = loseWindowController;
            _shadowPopup = shadowPopup;
            _inputController = inputController;
            _backgorundGameObject = backgorundGameObject;
            _gameplayStateController = gameplayStateController;
        }

        public BaseStateController Create()
        {
            BaseStateController stateController = new();

            stateController = new(
                new IState[]
                {
                    new MenuState(
                        _menuWindowController,
                        _shadowPopup,
                        _inputController,
                        stateController),
                    new GameplayState(
                        _gameplayStateController,
                        _shadowPopup,
                        _backgorundGameObject),
                    new WinState(
                        _winWindowController,
                        _backgorundGameObject,
                        _inputController,
                        stateController,
                        _shadowPopup),
                    new LoseState(
                        _loseWindowController,
                        _backgorundGameObject,
                        _inputController,
                        stateController,
                        _shadowPopup),
                },
                new ITransition[]
                {
                    new Transition<MenuState, GameplayState>(),
                    new Transition<GameplayState, WinState>(),
                    new Transition<GameplayState, LoseState>(),
                    new Transition<WinState, MenuState>(),
                    new Transition<LoseState, MenuState>(),
                },
                typeof(MenuState));

            return stateController;
        }
    }
}