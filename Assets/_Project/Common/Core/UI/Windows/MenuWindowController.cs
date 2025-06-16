using Cysharp.Threading.Tasks;
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
            BaseWindowAnimtion animtion) : base(menuWindowModel, playButton)
        {
            _menuWindowModel = menuWindowModel;
            _animtion = animtion;
        }

        public override UniTask ShowAsync() =>
            _animtion.PlayShowAnimationAsync();

        public override UniTask HideAsync() =>
            _animtion.PlayHideAnimationAsync();       

        protected override void OnClick() =>
            TriggerEvent();
    }
}