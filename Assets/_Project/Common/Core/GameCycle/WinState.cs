using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class WinState : IAsyncEnterState, IAsyncExitState
    {
        private readonly WinWindowController _winWindowController;
        private readonly GameObject _gamePlayBackground;
        private readonly InputController _inputController;
        private readonly BaseStateController _gameCycleStateController;
        private readonly ShadowPopup _shadowPopup;

        public WinState(
            WinWindowController winWindowController, 
            GameObject gamePlayBackground, 
            InputController inputController, 
            BaseStateController gameCycleStateController, 
            ShadowPopup shadowPopup)
        {
            _winWindowController = winWindowController;
            _gamePlayBackground = gamePlayBackground;
            _inputController = inputController;
            _gameCycleStateController = gameCycleStateController;
            _shadowPopup = shadowPopup;
        }

        public async UniTask AsyncEnter()
        {
            _inputController.DisableInput();
            _winWindowController.EnableWindowGameObject();
            await _winWindowController.ShowAsync();
            _inputController.EnableInput();
            await _winWindowController.AsyncWaitToClickOnGameplayButton();
            _inputController.DisableInput();
            await _winWindowController.HideAsync();
            _gameCycleStateController.Translate(typeof(MenuState)).Forget();
        }

        public async UniTask AsyncExit()
        {
            await _shadowPopup.HidePopup();
            _gamePlayBackground.SetActive(false);
        }
    }
}