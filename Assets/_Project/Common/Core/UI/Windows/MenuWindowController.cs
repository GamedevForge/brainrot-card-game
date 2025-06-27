using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI.Windows
{
    public class MenuWindowController : BaseWindowController
    {
        private readonly MenuWindowModel _menuWindowModel;
        private readonly BaseWindowAnimtion _animtion;

        public MenuWindowController(
            MenuWindowModel menuWindowModel,
            Button playButton,
            BaseWindowAnimtion animtion,
            AudioSource audioSource,
            AudioClip onPressedButtonSFX) : base(menuWindowModel, playButton, onPressedButtonSFX, audioSource)
        {
            _menuWindowModel = menuWindowModel;
            _animtion = animtion;
        }

        public override UniTask ShowAsync() =>
            _animtion.PlayShowAnimationAsync();

        public override UniTask HideAsync() =>
            _animtion.PlayHideAnimationAsync();       

        protected override void OnClick()
        {
            PlaySoundOnPressedButton();
            TriggerEvent();
        }

        public void SetNextLevelNumber(int levelNumber) =>
            _menuWindowModel.MenuWindowComponents.NextLevelText.text = $"Level {levelNumber + 1}";
    }
}