using System;
using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Services;
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
        private readonly AudioSource _audioSource;
        private readonly SoundsData _soundsData;

        private UpgradeUIElementsCreateData _leftUIElement;
        private UpgradeUIElementsCreateData _rightUIElement;
        private RectTransform _leftUIElementRectTransform;
        private RectTransform _rightUIElementRectTransform;

        private bool _upgradeIsActive = false;

        public UpgradeControllerView(
            UpgradeUIELementsPool upgradeUIELementsPool,
            MoveAnimation moveAnimation,
            UpgradeModel upgradeModel,
            UpgradeController upgradeController,
            Vector3 leftUIElementStartPosition,
            Vector3 leftUIElementEndPosition,
            Vector3 rightUIElementStartPosition,
            Vector3 rightUIElementEndPosition,
            CardCreatedData playerCard,
            AudioSource audioSource,
            SoundsData soundsData)
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
            _audioSource = audioSource;
            _soundsData = soundsData;
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
            _upgradeIsActive = true;
            
            if (_upgradeModel.UpgradeFrom.Type == Configs.UpgradeValueType.Addition)
                _leftUIElement.UIElementComponents.UpgradeIndexText.text = $"+{_upgradeModel.UpgradeFrom.Value}";
            else
                _leftUIElement.UIElementComponents.UpgradeIndexText.text = $"*{_upgradeModel.UpgradeFrom.Value}";
            _leftUIElement.UIElementComponents.MainImage.sprite = _upgradeModel.UpgradeFrom.UpgradeCardData.CardSprite;

            if (_upgradeModel.UpgradeTo.Type == Configs.UpgradeValueType.Addition)
                _rightUIElement.UIElementComponents.UpgradeIndexText.text = $"+{_upgradeModel.UpgradeTo.Value}";
            else
                _rightUIElement.UIElementComponents.UpgradeIndexText.text = $"*{_upgradeModel.UpgradeTo.Value}";
            _rightUIElement.UIElementComponents.MainImage.sprite = _upgradeModel.UpgradeTo.UpgradeCardData.CardSprite;

            _audioSource.PlayOneShot(_soundsData.OnShowSFX);
            await UniTask.WhenAll(
                _moveAnimation.MoveAsync(
                    _leftUIElementRectTransform, 
                    _leftUIElementRectTransform.position,
                    _leftUIElementEndPosition),
                _moveAnimation.MoveAsync(
                    _rightUIElementRectTransform,
                    _rightUIElementRectTransform.position,
                    _rightUIElementEndPosition),
                UniTask.WaitForSeconds(_soundsData.OnShowSFX.length));
        }
        public async UniTask HideUpgrades()
        {
            _audioSource.PlayOneShot(_soundsData.OnShowSFX);
            await UniTask.WhenAll(
                _moveAnimation.MoveAsync(
                    _leftUIElementRectTransform,
                    _leftUIElementRectTransform.position,
                    _leftUIElementStartPosition),
                _moveAnimation.MoveAsync(
                    _rightUIElementRectTransform,
                    _rightUIElementRectTransform.position,
                    _rightUIElementStartPosition),
                UniTask.WaitForSeconds(_soundsData.OnShowSFX.length));
        }

        private void UpgradeFromLeftButton()
        {
            if (_upgradeIsActive == false)
                return;
            
            _upgradeIsActive = false;
            _upgradeController.UpgradeForce(_upgradeModel.UpgradeFrom);
            _playerCard.CardComponents.CardForceIndex.text = _playerCard.CardStats.CardForce.ToString();
            _playerCard.CardComponents.MainImage.sprite = _upgradeModel.UpgradeFrom.UpgradeCardData.UpgradeCardTo.CardSprite;
        }

        private void UpgradeFromRightButton()
        {
            if (_upgradeIsActive == false)
                return;

            _upgradeIsActive = false;
            _upgradeController.UpgradeForce((_upgradeModel.UpgradeTo));
            _playerCard.CardComponents.CardForceIndex.text = _playerCard.CardStats.CardForce.ToString();
            _playerCard.CardComponents.MainImage.sprite = _upgradeModel.UpgradeTo.UpgradeCardData.UpgradeCardTo.CardSprite;
        }
    }
}