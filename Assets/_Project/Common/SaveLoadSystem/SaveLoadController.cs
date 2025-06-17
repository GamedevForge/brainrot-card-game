using Project.Core.Gameplay;
using UnityEngine;

namespace Project.SaveLoadSystem
{
    public class SaveLoadController : MonoBehaviour 
    {
        private SaveLoad _saveLoad;
        private LevelProgress _levelProgress;
        private PlayerSaveData _currentSaveData;

        public void Initilialize(
            LevelProgress levelProgress,
            SaveLoad saveLoad)
        {
            _levelProgress = levelProgress;
            _saveLoad = saveLoad;
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