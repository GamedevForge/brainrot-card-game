using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Project.Core.UI.Windows
{
    public class MenuWindowController : BaseWindowController
    {
        private readonly MenuWindowModel _menuWindowModel;

        public MenuWindowController(
            MenuWindowModel menuWindowModel,
            Button playButton) : base(menuWindowModel, playButton)
        {
            _menuWindowModel = menuWindowModel;
        }

        public override async UniTask Show()
        {
        }

        public override async UniTask Hide()
        {
        }

        protected override void OnClick() =>
            TriggerEvent();
    }
}