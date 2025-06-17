using Project.Configs;

namespace Project.Core.Gameplay
{
    public class LevelProgress
    {
        private readonly LevelsData _levelsData;

        public int CurrentLevelIndex { get; private set; }

        public void SetCurrentLevelIndex(int levelIndex) =>
            CurrentLevelIndex = levelIndex;

        public LevelData GetCurrentLevelData() =>
            _levelsData.LevelDatas[CurrentLevelIndex];

        public int GetCurrentLevelIndex() =>
            CurrentLevelIndex;
    }
}