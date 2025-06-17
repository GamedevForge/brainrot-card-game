using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class StartLevelState : IAsyncEnterState, ISetableState<BaseStateController>
    {
        private readonly GameplayController _gameplayController;
        private readonly LevelsData _levelData;
        private readonly InputController _inputController;

        private BaseStateController _gameplayStateController;
        
        public StartLevelState(
            GameplayController gameplayController,
            LevelsData levelData,
            InputController inputController)
        {
            _gameplayController = gameplayController;
            _levelData = levelData;
            _inputController = inputController;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {
            _inputController.DisableInput();
            _gameplayController.SetCurrentLevel(_levelData.LevelDatas[0]);
            _gameplayController.StartLevel();
            await _gameplayController.AddOnSlotAllCardFromCurrentWave();
            await _gameplayStateController.Translate(typeof(PlayerTurnState));
        }
    }
}