using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI;
using Project.Core.UpgradeSystem;

namespace Project.Core.Gameplay
{
    public class NextWaveState : IAsyncEnterState, IAsyncExitState, ISetableState<BaseStateController>
    {
        private readonly UpgradeController _upgradeController;
        private readonly UpgradeControllerView _upgradeControllerView;
        private readonly InputController _inputController;
        private readonly GameplayController _gameplayController;
        
        private BaseStateController _gameplayStateController;

        public NextWaveState(
            UpgradeController upgradeController, 
            UpgradeControllerView upgradeControllerView, 
            InputController inputController, 
            GameplayController gameplayController)
        {
            _upgradeController = upgradeController;
            _upgradeControllerView = upgradeControllerView;
            _inputController = inputController;
            _gameplayController = gameplayController;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {
            await _upgradeControllerView.ShowUpgrades();
            _inputController.EnableInput();
            await _upgradeController.AsyncWaitToUpgrade();
            _inputController.DisableInput();
            await _upgradeControllerView.HideUpgrades();
            _gameplayStateController.Translate(typeof(PlayerTurnState)).Forget();
        }

        public async UniTask AsyncExit()
        {
            await _gameplayController.RemoveAllCardOnCurrentWave();
            _gameplayController.GoToNextWave();
            await _gameplayController.AddOnSlotAllCardFromCurrentWave();
        }
    }
}