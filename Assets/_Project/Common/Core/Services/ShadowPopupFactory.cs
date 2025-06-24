using Project.Core.UI.Popup;
using UnityEngine;

namespace Project.Core.Services
{
    public class ShadowPopupFactory
    {
        private readonly GameObject _popupPrefab;
        private readonly RectTransform _popupGameObjectParent;
        private readonly float _popupAnimationDuration;

        public ShadowPopupFactory(
            GameObject popupPrefab, 
            RectTransform popupGameObjectParent, 
            float popupAnimationDuration)
        {
            _popupPrefab = popupPrefab;
            _popupGameObjectParent = popupGameObjectParent;
            _popupAnimationDuration = popupAnimationDuration;
        }

        public ShadowPopupCraeteData Create()
        {
            ShadowPopupCraeteData data = new();

            data.ShadowPopupGameObject = GameObject.Instantiate(
                _popupPrefab, 
                _popupGameObjectParent);
            data.ShadowPopup = new ShadowPopup(
                data.ShadowPopupGameObject.GetComponent<CanvasGroup>(),
                new UI.Animtions.AlphaAnimation(_popupAnimationDuration),
                data.ShadowPopupGameObject);

            return data;
        }
    }
}