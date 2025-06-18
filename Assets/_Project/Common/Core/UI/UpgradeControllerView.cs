using System;
using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.UI.Animtions;
using Project.Core.UpgradeSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Core.UI
{
    public class UpgradeControllerView : IInitializable, IDisposable
    {
        private readonly UpgradeUIELementsPool _upgradeUIELementsPool;
        private readonly MoveAnimation _moveAnimation;
        private readonly UpgradeModel _upgradeModel;
        private readonly UpgradeController _upgradeController;
        private readonly Vector3 _leftUIElementStartPosition;
        private readonly Vector3 _leftUIElementEndPosition;
        private readonly Vector3 _rightUIElementStartPosition;
        private readonly Vector3 _rightUIElementEndPosition;
        private readonly CardCreatedData _playerCard;

        private UpgradeUIElementsCreateData _leftUIElement;
        private UpgradeUIElementsCreateData _rightUIElement;
        private RectTransform _leftUIElementRectTransform;
        private RectTransform _rightUIElementRectTransform;

        public UpgradeControllerView(
            UpgradeUIELementsPool upgradeUIELementsPool,
            MoveAnimation moveAnimation,
            UpgradeModel upgradeModel,
            UpgradeController upgradeController,
            Vector3 leftUIElementStartPosition,
            Vector3 leftUIElementEndPosition,
            Vector3 rightUIElementStartPosition,
            Vector3 rightUIElementEndPosition,
            CardCreatedData playerCard)
        {
            _upgradeUIELementsPool = upgradeUIELementsPool;
            _moveAnimation = moveAnimation;
            _upgradeModel = upgradeModel;
            _upgradeController = upgradeController;
            _leftUIElementStartPosition = leftUIElementStartPosition;
            _leftUIElementEndPosition = leftUIElementEndPosition;
            _rightUIElementStartPosition = rightUIElementStartPosition;
            _rightUIElementEndPosition = rightUIElementEndPosition;
            _playerCard = playerCard;
        }

        public void Initialize()
        {
            _leftUIElement = _upgradeUIELementsPool.Get();
            _rightUIElement = _upgradeUIELementsPool.Get();
            _leftUIElement.UIELementGameObject.transform.position = _leftUIElementStartPosition;
            _rightUIElement.UIELementGameObject.transform.position = _rightUIElementStartPosition;
            _leftUIElementRectTransform = _leftUIElement.UIELementGameObject.GetComponent<RectTransform>();
            _rightUIElementRectTransform = _rightUIElement.UIELementGameObject.GetComponent<RectTransform>();

            _leftUIElement.UIElementComponents.UpgradeButton.onClick.AddListener(UpgradeFromLeftButton);
            _rightUIElement.UIElementComponents.UpgradeButton.onClick.AddListener(UpgradeFromRightButton);
        }
       
        public void Dispose()
        {
            _leftUIElement.UIElementComponents.UpgradeButton.onClick.RemoveListener(UpgradeFromLeftButton);
            _rightUIElement.UIElementComponents.UpgradeButton.onClick.RemoveListener(UpgradeFromRightButton);
        }

        public async UniTask ShowUpgrades()
        {
            _leftUIElement.UIElementComponents.UpgradeIndexText.text = $"+{_upgradeModel.UpgradeFrom}";
            _rightUIElement.UIElementComponents.UpgradeIndexText.text = $"+{_upgradeModel.UpgradeTo}";

            await UniTask.WhenAll(
                _moveAnimation.MoveAsync(
                    _leftUIElementRectTransform, 
                    _leftUIElementRectTransform.position,
                    _leftUIElementEndPosition),
                _moveAnimation.MoveAsync(
                    _rightUIElementRectTransform,
                    _rightUIElementRectTransform.position,
                    _rightUIElementEndPosition));
        }
        public UniTask HideUpgrades() =>
            UniTask.WhenAll(
                _moveAnimation.MoveAsync(
                    _leftUIElementRectTransform,
                    _leftUIElementRectTransform.position,
                    _leftUIElementStartPosition),
                _moveAnimation.MoveAsync(
                    _rightUIElementRectTransform,
                    _rightUIElementRectTransform.position,
                    _rightUIElementStartPosition));

        private void UpgradeFromLeftButton()
        {
            _upgradeController.UpgradeForce(_upgradeModel.UpgradeFrom);
            _playerCard.CardComponents.CardForceIndex.text = _playerCard.CardStats.CardForce.ToString();
        }

        private void UpgradeFromRightButton()
        {
            _upgradeController.UpgradeForce((_upgradeModel.UpgradeTo));
            _playerCard.CardComponents.CardForceIndex.text = _playerCard.CardStats.CardForce.ToString();
        }
    }
}