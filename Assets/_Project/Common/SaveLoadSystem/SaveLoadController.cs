using Project.Core.Gameplay;
using UnityEngine;

namespace Project.SaveLoadSystem
{
    public class SaveLoadController : MonoBehaviour 
    {
        private readonly SaveLoad _saveLoad = new();

        private LevelProgress _levelProgress;
        private PlayerSaveData _currentSaveData;

        public void Initilialize(
            LevelProgress levelProgress)
        {
            _levelProgress = levelProgress;
            _currentSaveData = _saveLoad.Load();

            if (_currentSaveData == null) 
                _currentSaveData = new PlayerSaveData();

            _levelProgress.SetCurrentLevelIndex(_currentSaveData.CurrentLevel);
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnDestroy()
        {
            Save();
        }

        public void Save()
        {
            _currentSaveData.CurrentLevel = _levelProgress.GetCurrentLevelIndex();
            _saveLoad.Save(_currentSaveData);
        }
    }
}