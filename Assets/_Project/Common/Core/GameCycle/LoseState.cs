using Cysharp.Threading.Tasks;
using Project.Core.Gameplay;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class LoseState : IAsyncEnterState, IAsyncExitState, ISetableState<BaseStateController>
    {
        private readonly LoseWindowController _loseWindowController;
        private readonly GameObject _gamePlayBackground;
        private readonly InputController _inputController;
        private readonly ShadowPopup _shadowPopup;
        private readonly GameplayController _gameplayController;
        
        private BaseStateController _gameCycleStateController;

        public LoseState(
            LoseWindowController loseWindowController,
            GameObject gamePlayBackground,
            InputController inputController,
            ShadowPopup shadowPopup,
            GameplayController gameplayController)
        {
            _loseWindowController = loseWindowController;
            _gamePlayBackground = gamePlayBackground;
            _inputController = inputController;
            _shadowPopup = shadowPopup;
            _gameplayController = gameplayController;
        }

        public void Set(BaseStateController stateController) =>
            _gameCycleStateController = stateController;

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
            await _gameplayController.RemoveAllCardOnCurrentWave();
            await _loseWindowController.HideAsync();
            await _shadowPopup.ShowPopup();
            _loseWindowController.DisableWindowGameObject();
            _gamePlayBackground.SetActive(false);
        }
    }
}