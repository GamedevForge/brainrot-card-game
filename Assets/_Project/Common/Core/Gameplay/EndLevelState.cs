using Cysharp.Threading.Tasks;
using Project.Core.GameCycle;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class EndLevelState : IEnterState
    {
        private readonly GameplayModel _gameplayModel;
        private readonly BaseStateController _gameCycleStateController;
        private readonly InputController _inputController;

        public EndLevelState(
            GameplayModel gameplayModel,
            BaseStateController gameCycleStateController,
            InputController inputController)
        {
            _gameplayModel = gameplayModel;
            _gameCycleStateController = gameCycleStateController;
            _inputController = inputController;
        }

        public void Enter()
        {
            _inputController.EnableInput();
            if (_gameplayModel.PlayerCard.Health.IsAlive)
                _gameCycleStateController.Translate(typeof(WinState)).Forget();
            else
                _gameCycleStateController.Translate(typeof(LoseState)).Forget();
        }
    }
}