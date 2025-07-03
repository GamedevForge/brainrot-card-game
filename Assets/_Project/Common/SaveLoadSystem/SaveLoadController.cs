using Project.Core.Gameplay;
using UnityEngine;

namespace Project.SaveLoadSystem
{
    public class SaveLoadController : MonoBehaviour 
    {
        private ISaveLoad _saveLoad;
        private LevelProgress _levelProgress;
        private PlayerSaveData _currentSaveData;

        public void Initialize(
            LevelProgress levelProgress,
            ISaveLoad saveLoad)
        {
            _levelProgress = levelProgress;
            _saveLoad = saveLoad;
            _currentSaveData = _saveLoad.Load();

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