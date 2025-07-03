using Cysharp.Threading.Tasks;
using GamePush;
using Project.Core.Gameplay;
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
        private readonly LevelProgress _levelProgress;

        private BaseStateController _gameCycleStateController;

        public MenuState(
            MenuWindowController menuWindowController,
            ShadowPopup shadowPopup,
            InputController inputController,
            LevelProgress levelProgress)
        {
            _menuWindowController = menuWindowController;
            _shadowPopup = shadowPopup;
            _inputController = inputController;
            _levelProgress = levelProgress;
        }

        public void Set(BaseStateController stateController) =>
            _gameCycleStateController = stateController;

        public async UniTask AsyncEnter()
        {
            _menuWindowController.EnableWindowGameObject();
            _menuWindowController.SetNextLevelNumber(_levelProgress.CurrentLevelIndex);
            await _shadowPopup.HidePopup();
            _inputController.EnableInput();
            await _menuWindowController.AsyncWaitToClickOnGameplayButton();
            _inputController.DisableInput();
            GP_Analytics.Goal("level_start", _levelProgress.CurrentLevelNumber);
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