using Cysharp.Threading.Tasks;
using Project.Core.Services;
using Project.Core.Services.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;

namespace Project.Core.GameCycle
{
    public class MenuState : IAsyncEnterState, IAsyncExitState, ISetableState<BaseStateController>
    {
        private readonly MenuWindowController _menuWindowController;
        private readonly ShadowPopup _shadowPopup;   
        private readonly InputController _inputController;
        
        private BaseStateController _gameCycleStateController;

        public MenuState(
            MenuWindowController menuWindowController, 
            ShadowPopup shadowPopup, 
            InputController inputController)
        {
            _menuWindowController = menuWindowController;
            _shadowPopup = shadowPopup;
            _inputController = inputController;
        }

        public void Set(BaseStateController stateController) =>
            _gameCycleStateController = stateController;

        public async UniTask AsyncEnter()
        {
            _menuWindowController.EnableWindowGameObject();
            await _shadowPopup.HidePopup();
            _inputController.EnableInput();
            await _menuWindowController.AsyncWaitToClickOnGameplayButton();
            _inputController.DisableInput();
            _gameCycleStateController.Translate(typeof(GameplayState)).Forget();
        }

        public async UniTask AsyncExit()
        {
            await _menuWindowController.HideAsync();
            await _shadowPopup.ShowPopup();
            _menuWindowController.DisableWindowGameObject();
        }
    }
}