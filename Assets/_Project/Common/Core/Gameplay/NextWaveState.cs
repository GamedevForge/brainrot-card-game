using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI;
using Project.Core.UpgradeSystem;

namespace Project.Core.Gameplay
{
    public class NextWaveState : IAsyncEnterState, IAsyncExitState
    {
        private readonly BaseStateController _baseStateController;
        private readonly UpgradeController _upgradeController;
        private readonly UpgradeControllerView _upgradeControllerView;
        private readonly InputController _inputController;
        private readonly GameplayController _gameplayController;

        public NextWaveState(
            BaseStateController baseStateController, 
            UpgradeController upgradeController, 
            UpgradeControllerView upgradeControllerView, 
            InputController inputController, 
            GameplayController gameplayController)
        {
            _baseStateController = baseStateController;
            _upgradeController = upgradeController;
            _upgradeControllerView = upgradeControllerView;
            _inputController = inputController;
            _gameplayController = gameplayController;
        }

        public async UniTask AsyncEnter()
        {
            _gameplayController.ReleaseAllCardOnCurrentWave();
            await _upgradeControllerView.ShowUpgrades();
            _inputController.EnableInput();
            await _upgradeController.AsyncWaitToUpgrade();
            _inputController.DisableInput();
            await _upgradeControllerView.HideUpgrades();
            _baseStateController.Translate(typeof(PlayerTurnState)).Forget();
        }

        public UniTask AsyncExit() =>
            _gameplayController.GoToNextWave();
    }
}