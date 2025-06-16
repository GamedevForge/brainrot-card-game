using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class LoseState : IAsyncEnterState, IAsyncExitState
    {
        private readonly LoseWindowController _loseWindowController;
        private readonly GameObject _gamePlayBackground;
        private readonly InputController _inputController;
        private readonly BaseStateController _gameCycleStateController;
        private readonly ShadowPopup _shadowPopup;

        public LoseState(
            LoseWindowController loseWindowController, 
            GameObject gamePlayBackground, 
            InputController inputController, 
            BaseStateController gameCycleStateController, 
            ShadowPopup shadowPopup)
        {
            _loseWindowController = loseWindowController;
            _gamePlayBackground = gamePlayBackground;
            _inputController = inputController;
            _gameCycleStateController = gameCycleStateController;
            _shadowPopup = shadowPopup;
        }

        public async UniTask AsyncEnter()
        {
            _inputController.DisableInput();
            _loseWindowController.EnableWindowGameObject();
            await _loseWindowController.ShowAsync();
            _inputController.EnableInput();
            await _loseWindowController.AsyncWaitToClickOnGameplayButton();
            _inputController.DisableInput();
            await _loseWindowController.HideAsync();
            _gameCycleStateController.Translate(typeof(MenuState)).Forget();
        }

        public async UniTask AsyncExit()
        {
            await _shadowPopup.HidePopup();
            _gamePlayBackground.SetActive(false);
        }
    }
}