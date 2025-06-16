using Cysharp.Threading.Tasks;
using Project.Core.UI.Animtions;
using UnityEngine;

namespace Project.Core.UI.Windows
{
    public class BaseWindowAnimtion : IWindowAnimation
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly AlphaAnimation _alphaAnimation;
        
        public BaseWindowAnimtion(
            CanvasGroup canvasGroup, 
            AlphaAnimation alphaAnimation)
        {
            _canvasGroup = canvasGroup;
            _alphaAnimation = alphaAnimation;
        }

        public UniTask PlayShowAnimationAsync() =>
            _alphaAnimation.PlayAnimationAsync(_canvasGroup, 1f);

        public UniTask PlayHideAnimationAsync() =>
            _alphaAnimation.PlayAnimationAsync(_canvasGroup, 0f);
    }
}