using System.Collections.Generic;
using Project.Configs;
using Project.Core.Sevices;

namespace Project.Core.Gameplay
{
    public class GameplayModel
    {
        public LevelData CurrentLevelData { get; }
        public List<WaveModel> LevelModel { get; }
        public WaveModel CurrentWave { get; }
        public CardCreatedData PlayerCard { get; }

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