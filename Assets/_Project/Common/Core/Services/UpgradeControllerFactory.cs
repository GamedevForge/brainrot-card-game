using Project.Core.Card;
using Project.Core.UI;
using Project.Core.UpgradeSystem;

namespace Project.Core.Sevices
{
    public class UpgradeControllerFactory
    {
        private readonly CardStats _playerCardStats;

        public UpgradeControllerFactory(CardStats playerCardStats)
        {
            _playerCardStats = playerCardStats;
        }

        public UpgradeControllerCreateData Create()
        {
            UpgradeControllerCreateData data = new();

            data.UpgradeController = new UpgradeController(_playerCardStats);
            data.UpgradeControllerView = new UpgradeControllerView();

            return data;
        }
    }
}