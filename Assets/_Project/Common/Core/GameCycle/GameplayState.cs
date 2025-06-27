using Cysharp.Threading.Tasks;
using Project.Configs;
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
        private readonly LevelProgress _levelProgress;
        
        private BaseStateController _gameplayStateController;

        public GameplayState(
            ShadowPopup shadowPopup,
            GameObject gamePlayBackground,
            CardCreatedData playerCard,
            LevelProgress levelProgress)
        {
            _shadowPopup = shadowPopup;
            _gamePlayBackground = gamePlayBackground;
            _playerCard = playerCard;
            _levelProgress = levelProgress;
        }

        public async UniTask AsyncEnter()
        {
            CardConfig playerCardConfig = _levelProgress
                .GetCurrentLevelData()
                .PlayerCardConfig;
            
            _gamePlayBackground.SetActive(true);
            if (_playerCard.Health.IsAlive == false)
                _playerCard.Health.Revive();

            _playerCard.CardGameObject.SetActive(true);
            _playerCard.CanvasGroup.alpha = 1f;
            _playerCard.Health.SetHealth(playerCardConfig.CardForce);
            _playerCard.CardComponents.CardForceIndex.text = playerCardConfig.CardForce.ToString();
            _playerCard.CardComponents.MainImage.sprite = playerCardConfig.EnemyCardData.CardSprite;
            _playerCard.AudioClip = playerCardConfig.EnemyCardData.AudioClip;

            await _shadowPopup.HidePopup();
            await _gameplayStateController.Translate(typeof(StartLevelState));
        }

        public void SetGameplayStateController(BaseStateController gameplayStateController) =>
            _gameplayStateController = gameplayStateController;
    }
}