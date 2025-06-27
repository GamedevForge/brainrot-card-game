using Cysharp.Threading.Tasks;
using Project.Core.UI.Animtions;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI.Windows
{
    public class WinWindowController : BaseWindowController
    {
        private readonly WinWindowModel _model;
        private readonly AlphaAnimation _animation;

        public WinWindowController(
            WinWindowModel model,
            Button goToMenu,
            AlphaAnimation animation,
            AudioSource audioSource,
            AudioClip onPressedbuttonSFX) : base(model, goToMenu, onPressedbuttonSFX, audioSource)
        {
            _model = model;
            _animation = animation;
        }

        public override UniTask HideAsync() =>
            _animation.PlayAnimationAsync(_model.CanvasGroup, 0f);

        public override UniTask ShowAsync() =>
            _animation.PlayAnimationAsync(_model.CanvasGroup, 1f);

        protected override void OnClick()
        {
            PlaySoundOnPressedButton();
            TriggerEvent();
        }
    }
}