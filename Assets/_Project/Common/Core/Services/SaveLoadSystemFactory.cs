using Project.Configs;
using Project.Core.Gameplay;
using Project.SaveLoadSystem;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class SaveLoadSystemFactory
    {
        private readonly GameObject _saveLoadSystemPrefab;
        private readonly LevelsData _levelsData;

        public SaveLoadSystemFactory(GameObject saveLoadSystemPrefab, LevelsData levelsData)
        {
            _saveLoadSystemPrefab = saveLoadSystemPrefab;
            _levelsData = levelsData;
        }

        public SaveLoadSystemCreateData Create()
        {
            SaveLoadSystemCreateData data = new();

            data.SaveLoadSystemGameObject = GameObject.Instantiate(_saveLoadSystemPrefab);
            data.SaveLoadController = data.SaveLoadSystemGameObject.GetComponent<SaveLoadController>();
            data.LevelProgress = new LevelProgress(_levelsData);

            data.SaveLoadController.Initilialize(data.LevelProgress, new SaveLoad());

            return data;
        }
    }
}