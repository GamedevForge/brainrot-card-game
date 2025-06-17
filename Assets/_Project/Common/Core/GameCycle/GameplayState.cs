using Cysharp.Threading.Tasks;
using Project.Core.Gameplay;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class GameplayState : IAsyncEnterState
    {
        private readonly ShadowPopup _shadowPopup;
        private readonly GameObject _gamePlayBackground;
        
        private BaseStateController _gameplayStateController;

        public GameplayState(
            ShadowPopup shadowPopup, 
            GameObject gamePlayBackground)
        {
            _shadowPopup = shadowPopup;
            _gamePlayBackground = gamePlayBackground;
        }

        public async UniTask AsyncEnter()
        {
            _gamePlayBackground.SetActive(true);
            await _shadowPopup.HidePopup();
            await _gameplayStateController.Translate(typeof(StartLevelState));
        }

        public void SetGameplayStateController(BaseStateController gameplayStateController) =>
            _gameplayStateController = gameplayStateController;
    }
}