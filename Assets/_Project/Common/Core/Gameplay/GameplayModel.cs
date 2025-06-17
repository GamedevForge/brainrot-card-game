using System.Collections.Generic;
using Project.Configs;
using Project.Core.Sevices;

namespace Project.Core.Gameplay
{
    public class GameplayModel
    {
        public LevelData CurrentLevelData { get; set; }
        public List<WaveModel> LevelModel { get; set; }
        public WaveModel CurrentWave { get; set; }
        public CardCreatedData PlayerCard { get; }

        public GameplayModel(CardCreatedData playerCard) =>
            PlayerCard = playerCard;

        public bool AllEnemyCardDead()
        {
            foreach (CardCreatedData card in CurrentWave.CardCreatedDatas)
            {
                if (card.Health.IsAlive)
                    return false;
            }
            return true;
        }
    }
}