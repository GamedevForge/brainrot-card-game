using Cysharp.Threading.Tasks;
using Project.Core.Gameplay;
using Project.Core.Services;
using Project.Core.Services.StateMachine;
using Project.Core.UI.Popup;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class GameplayState : IAsyncEnterState
    {
        private readonly ShadowPopup _shadowPopup;
        private readonly GameObject _gamePlayBackground;
        private readonly CardCreatedData _playerCard;
        
        private BaseStateController _gameplayStateController;

        public GameplayState(
            ShadowPopup shadowPopup,
            GameObject gamePlayBackground,
            CardCreatedData playerCard)
        {
            _shadowPopup = shadowPopup;
            _gamePlayBackground = gamePlayBackground;
            _playerCard = playerCard;
        }

        public async UniTask AsyncEnter()
        {
            _gamePlayBackground.SetActive(true);
            if (_playerCard.Health.IsAlive == false)
                _playerCard.Health.Revive();

            await _shadowPopup.HidePopup();
            await _gameplayStateController.Translate(typeof(StartLevelState));
        }

        public void SetGameplayStateController(BaseStateController gameplayStateController) =>
            _gameplayStateController = gameplayStateController;
    }
}