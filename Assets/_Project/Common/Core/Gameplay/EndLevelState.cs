using Cysharp.Threading.Tasks;
using Project.Core.GameCycle;
using Project.Core.Sevices.StateMachine;

namespace Project.Core.Gameplay
{
    public class EndLevelState : IEnterState
    {
        private readonly GameplayModel _gameplayModel;
        private readonly BaseStateController _gameCycleStateController;

        public EndLevelState(
            GameplayModel gameplayModel, 
            BaseStateController gameCycleStateController)
        {
            _gameplayModel = gameplayModel;
            _gameCycleStateController = gameCycleStateController;
        }

        public void Enter()
        {
            if (_gameplayModel.PlayerCard.Health.IsAlive)
                _gameCycleStateController.Translate(typeof(WinState)).Forget();
            else
                _gameCycleStateController.Translate(typeof(LoseState)).Forget();
        }
    }
}