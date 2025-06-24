using Project.Core.Gameplay;
using Project.Core.UI;

namespace Project.Core.Services
{
    public struct GameplayControllerCreateData
    {
        public AttackController AttackController;
        public GameplayController GameplayController;
        public CardSlots CardSlots;
        public GameplayModel GameplayModel;
    }
}