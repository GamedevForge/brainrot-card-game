using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class StartLevelState : IAsyncEnterState
    {
        private readonly GameplayController _gameplayController;
        private readonly LevelsData _levelData;
        private readonly BaseStateController _gameplayStateController;
        private readonly InputController _inputController;

        public StartLevelState(
            GameplayController gameplayController,
            LevelsData levelData,
            BaseStateController gameplayStateController,
            InputController inputController)
        {
            _gameplayController = gameplayController;
            _levelData = levelData;
            _gameplayStateController = gameplayStateController;
            _inputController = inputController;
        }

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