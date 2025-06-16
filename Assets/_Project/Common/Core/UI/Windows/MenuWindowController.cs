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

        public override async UniTask ShowAsync()
        {
        }

        public override async UniTask HideAsync()
        {
        }

        protected override void OnClick() =>
            TriggerEvent();
    }
}