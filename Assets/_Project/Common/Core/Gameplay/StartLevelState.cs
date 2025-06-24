using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using UnityEngine;

namespace Project.Core.Gameplay
{
    public class StartLevelState : IAsyncEnterState, ISetableState<BaseStateController>
    {
        private readonly GameplayController _gameplayController;
        private readonly LevelProgress _levelProgress;
        private readonly InputController _inputController;
        private readonly CardCreatedData _playerCard;
        private readonly AudioSource _audioSource;

        private BaseStateController _gameplayStateController;

        public StartLevelState(
            GameplayController gameplayController,
            InputController inputController,
            LevelProgress levelProgress,
            CardCreatedData playerCard,
            AudioSource audioSource)
        {
            _gameplayController = gameplayController;
            _inputController = inputController;
            _levelProgress = levelProgress;
            _playerCard = playerCard;
            _audioSource = audioSource;
        }

        public void Set(BaseStateController stateController) =>
            _gameplayStateController = stateController;

        public async UniTask AsyncEnter()
        {
            if (_playerCard.AudioClip != null)
            {
                _audioSource.PlayOneShot(_playerCard.AudioClip);
                await UniTask.WaitForSeconds(_playerCard.AudioClip.length);
            }
            _inputController.DisableInput();
            _gameplayController.SetCurrentLevel(_levelProgress.GetCurrentLevelData());
            _gameplayController.StartLevel();
            await _gameplayController.AddOnSlotAllCardFromCurrentWave();
            await _gameplayStateController.Translate(typeof(PlayerTurnState));
        }
    }
}