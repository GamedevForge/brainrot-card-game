using Cysharp.Threading.Tasks;
using GamePush;
using Project.Configs;
using Project.Core.Gameplay;
using Project.Core.Services;
using Project.Core.Services.StateMachine;
using Project.Core.UI.Popup;
using Project.Core.UI.Windows;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class WinState : IAsyncEnterState, IAsyncExitState, ISetableState<BaseStateController>
    {
        private readonly WinWindowController _winWindowController;
        private readonly GameObject _gamePlayBackground;
        private readonly InputController _inputController;
        private readonly ShadowPopup _shadowPopup;
        private readonly LevelProgress _levelProgress;
        private readonly LevelsData _levelsData;
        private readonly GameplayController _gameplayController;
        private readonly InterstitialController _interstitialController;
        private readonly GameData _gameData;
        
        private BaseStateController _gameCycleStateController;
        private int _currentLevelCompletedCountBeforeShowingAds;

        public WinState(
            WinWindowController winWindowController,
            GameObject gamePlayBackground,
            InputController inputController,
            ShadowPopup shadowPopup,
            LevelProgress levelProgress,
            LevelsData levelsData,
            GameplayController gameplayController,
            InterstitialController interstitialController,
            GameData gameData)
        {
            _winWindowController = winWindowController;
            _gamePlayBackground = gamePlayBackground;
            _inputController = inputController;
            _shadowPopup = shadowPopup;
            _levelProgress = levelProgress;
            _levelsData = levelsData;
            _gameplayController = gameplayController;
            _interstitialController = interstitialController;
            _gameData = gameData;
        }

        public void Set(BaseStateController stateController) =>
            _gameCycleStateController = stateController;

        public async UniTask AsyncEnter()
        {
            GP_Analytics.Goal("level_complete", _levelProgress.CurrentLevelNumber);
            _levelProgress.SetCurrentLevelIndex(_levelProgress.CurrentLevelIndex + 1);
            
            _inputController.DisableInput();
            _winWindowController.EnableWindowGameObject();
            await _gameplayController.RemoveAllCardOnCurrentWave();
            await _winWindowController.ShowAsync();
            _inputController.EnableInput();
            await _winWindowController.AsyncWaitToClickOnGameplayButton();
            _inputController.DisableInput();
            await _winWindowController.HideAsync();
            await _shadowPopup.ShowPopup();

            if (_currentLevelCompletedCountBeforeShowingAds == _gameData.LevelCompletedCountBeforeShowingAds)
            {
                _currentLevelCompletedCountBeforeShowingAds = 0;
                await _interstitialController.ShowInterstitial();
            }
            else
                _currentLevelCompletedCountBeforeShowingAds++;

            _gameCycleStateController.Translate(typeof(MenuState)).Forget();
        }

        public async UniTask AsyncExit()
        {
            await _winWindowController.HideAsync();
            _winWindowController.DisableWindowGameObject();
            _gamePlayBackground.SetActive(false);
        }
    }
}