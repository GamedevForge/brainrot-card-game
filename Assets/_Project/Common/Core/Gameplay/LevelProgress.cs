using Project.Configs;

namespace Project.Core.Gameplay
{
    public class LevelProgress
    {
        private readonly LevelsData _levelsData;

        public LevelProgress(LevelsData levelsData)
        {
            _levelsData = levelsData;
        }

        public int CurrentLevelIndex { get; private set; } = 0;

        public void SetCurrentLevelIndex(int levelIndex) =>
            CurrentLevelIndex = levelIndex;

        public LevelData GetCurrentLevelData() =>
            _levelsData.LevelDatas[CurrentLevelIndex % _levelsData.LevelDatas.Length];

        public int GetCurrentLevelIndex() =>
            CurrentLevelIndex;
    }
}