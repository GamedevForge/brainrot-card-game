using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class StartLevelState : IAsyncEnterState, ISetableState<BaseStateController>
    {
        private readonly GameplayController _gameplayController;
        private readonly LevelProgress _levelProgress;
        private readonly InputController _inputController;

        private BaseStateController _gameplayStateController;

        public StartLevelState(
            GameplayController gameplayController,
            InputController inputController,
            LevelProgress levelProgress)
        {
            _gameplayController = gameplayController;
            _inputController = inputController;
            _levelProgress = levelProgress;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {
            _inputController.DisableInput();
            _gameplayController.SetCurrentLevel(_levelProgress.GetCurrentLevelData());
            _gameplayController.StartLevel();
            await _gameplayController.AddOnSlotAllCardFromCurrentWave();
            await _gameplayStateController.Translate(typeof(PlayerTurnState));
        }
    }
}