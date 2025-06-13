using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;

namespace Project.Core.GameCycle
{
    public class MenuState : IAsyncEnterState, IAsyncExitState
    {
        private readonly MenuWindowController _menuWindowController;
        private readonly ShadowPopup _shadowPopup;   
        private readonly InputController _inputController;
        private readonly BaseStateController _gameCycleStateController;

        public async UniTask AsyncEnter()
        {
            _menuWindowController.EnableMenuWindowGameObject();
            await _shadowPopup.HidePopup();
            _inputController.EnableInput();
            await _menuWindowController.AsyncWaitToClickOnGameplayButton();
            _inputController.DisableInput();
            _gameCycleStateController.Translate(typeof(GameplayState)).Forget();
        }

        public async UniTask AsyncExit()
        {
            await _menuWindowController.Hide();
            await _shadowPopup.ShowPopup();
            _menuWindowController.DisableMenuWindowGameObject();
        }
    }
}