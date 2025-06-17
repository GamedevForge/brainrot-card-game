using Cysharp.Threading.Tasks;
using Project.Core.UI.Animtions;
using UnityEngine;

namespace Project.Core.UI.Popup
{
    public class ShadowPopup
    {
        private readonly CanvasGroup _popupCanvasGroup;
        private readonly AlphaAnimation _alphaAnimation;
        private readonly GameObject _popupObject;

        public ShadowPopup(
            CanvasGroup popupCanvasGroup, 
            AlphaAnimation alphaAnimation, 
            GameObject popupObject)
        {
            _popupCanvasGroup = popupCanvasGroup;
            _alphaAnimation = alphaAnimation;
            _popupObject = popupObject;
        }

        public async UniTask ShowPopup()
        {
            _popupObject.SetActive(true);
            await _alphaAnimation.PlayAnimationAsync(_popupCanvasGroup, 1f);
        }

        public async UniTask HidePopup()
        {
            await _alphaAnimation.PlayAnimationAsync(_popupCanvasGroup, 0f);
            _popupObject.SetActive(false);
        }
    }
}