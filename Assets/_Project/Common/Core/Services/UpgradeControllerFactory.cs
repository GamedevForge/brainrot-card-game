using Project.Core.Card;
using Project.Core.UI;
using Project.Core.UpgradeSystem;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class UpgradeControllerFactory
    {
        private readonly CardCreatedData _playerCard;
        private readonly int _upgradeUIElementsPoolSize;
        private readonly GameObject _upgradeUIElementPrefab;
        private readonly RectTransform _upgradeUIElementsParent;
        private readonly float _showAndHideAnimationDuration;
        private readonly Vector3 _leftUIElementStartPosition;
        private readonly Vector3 _leftUIElementEndPosition;
        private readonly Vector3 _rightUIElementStartPosition;
        private readonly Vector3 _rightUIElementEndPosition;
        private readonly AudioSource _audioSource;

        public UpgradeControllerFactory(
            CardCreatedData playerCard,
            int upgradeUIElementsPoolSize,
            GameObject upgradeUIElementPrefab,
            RectTransform upgradeUIElementsParent,
            float showAndHideAnimationDuration,
            Vector3 leftUIElementStartPosition,
            Vector3 leftUIElementEndPosition,
            Vector3 rightUIElementStartPosition,
            Vector3 rightUIElementEndPosition,
            AudioSource audioSource)
        {
            _playerCard = playerCard;
            _upgradeUIElementsPoolSize = upgradeUIElementsPoolSize;
            _upgradeUIElementPrefab = upgradeUIElementPrefab;
            _upgradeUIElementsParent = upgradeUIElementsParent;
            _showAndHideAnimationDuration = showAndHideAnimationDuration;
            _leftUIElementStartPosition = leftUIElementStartPosition;
            _leftUIElementEndPosition = leftUIElementEndPosition;
            _rightUIElementStartPosition = rightUIElementStartPosition;
            _rightUIElementEndPosition = rightUIElementEndPosition;
            _audioSource = audioSource;
        }

        public UpgradeControllerCreateData Create()
        {
            UpgradeControllerCreateData data = new();

            data.UpgradeModel = new UpgradeModel();
            data.UpgradeController = new UpgradeController(_playerCard, data.UpgradeModel, _audioSource);
            data.UpgradeControllerView = new UpgradeControllerView(
                new UpgradeUIELementsPool(
                    new UpgradeUIElementsFactory(
                        _upgradeUIElementPrefab,
                        _upgradeUIElementsParent),
                    _upgradeUIElementsPoolSize),
                new UI.Animtions.MoveAnimation(_showAndHideAnimationDuration),
                data.UpgradeModel,
                data.UpgradeController,
                _leftUIElementStartPosition,
                _leftUIElementEndPosition,
                _rightUIElementStartPosition,
                _rightUIElementEndPosition,
                _playerCard);

            return data;
        }
    }
}