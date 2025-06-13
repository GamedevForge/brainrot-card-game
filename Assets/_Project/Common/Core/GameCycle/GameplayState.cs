using Cysharp.Threading.Tasks;
using Project.Core.Gameplay;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Popup;
using UnityEngine;

namespace Project.Core.GameCycle
{
    public class GameplayState : IAsyncEnterState
    {
        private readonly BaseStateController _gameplayStateController;
        private readonly ShadowPopup _shadowPopup;
        private readonly GameObject _gamePlayBackground;

        public GameplayState(
            BaseStateController gameplayStateController, 
            ShadowPopup shadowPopup, 
            GameObject gamePlayBackground)
        {
            _gameplayStateController = gameplayStateController;
            _shadowPopup = shadowPopup;
            _gamePlayBackground = gamePlayBackground;
        }

        public async UniTask AsyncEnter()
        {
            _gamePlayBackground.SetActive(true);
            await _shadowPopup.HidePopup();
            await _gameplayStateController.Translate(typeof(StartLevelState));
        }
    }

    public class LoseState : IAsyncEnterState
    {
        
        
        public async UniTask AsyncEnter()
        {
        
        }
    }

    public class WinState
    {

    }
}