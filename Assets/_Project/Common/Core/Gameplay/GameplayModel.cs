using System.Collections.Generic;
using Project.Configs;
using Project.Core.Sevices;

namespace Project.Core.Gameplay
{
    public class GameplayModel
    {
        public LevelData CurrentLevelData { get; private set; }
        public List<WaveModel> LevelModel { get; private set; }
        public WaveModel CurrentWave { get; private set; }

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