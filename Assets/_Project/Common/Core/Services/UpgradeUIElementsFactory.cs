using Project.Core.UI;
using UnityEngine;

namespace Project.Core.Services
{
    public class UpgradeUIElementsFactory
    {
        private readonly GameObject _basePrefab;
        private readonly RectTransform _uiElementsParent;

        public UpgradeUIElementsFactory(
            GameObject basePrefab, 
            RectTransform uiElementsParent)
        {
            _basePrefab = basePrefab;
            _uiElementsParent = uiElementsParent;
        }

        public UpgradeUIElementsCreateData Create()
        {
            UpgradeUIElementsCreateData data = new();

            data.UIELementGameObject = GameObject.Instantiate(_basePrefab, _uiElementsParent);
            data.UIElementComponents = data.UIELementGameObject.GetComponent<UpgradeUIElementComponents>();

            return data;
        }
    }
}