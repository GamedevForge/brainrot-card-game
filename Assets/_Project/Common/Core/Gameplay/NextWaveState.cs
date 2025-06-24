using Cysharp.Threading.Tasks;
using Project.Core.Services;
using Project.Core.Services.StateMachine;
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
        private readonly GameplayModel _gameplayModel;

        private BaseStateController _gameplayStateController;

        public NextWaveState(
            UpgradeController upgradeController,
            UpgradeControllerView upgradeControllerView,
            InputController inputController,
            GameplayController gameplayController,
            GameplayModel gameplayModel)
        {
            _upgradeController = upgradeController;
            _upgradeControllerView = upgradeControllerView;
            _inputController = inputController;
            _gameplayController = gameplayController;
            _gameplayModel = gameplayModel;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {
            _upgradeController.SetFromTo(
                _gameplayModel.CurrentWaveConfig.UpgradeRangeConfig.To,
                _gameplayModel.CurrentWaveConfig.UpgradeRangeConfig.From);
            foreach(CardCreatedData enemyCard in _gameplayModel.CurrentWave.CardCreatedDatas)
                enemyCard.CardComponents.GrayScaleEffect.enabled = true;

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