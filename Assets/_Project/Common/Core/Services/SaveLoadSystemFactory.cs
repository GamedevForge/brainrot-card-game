using Project.Core.Gameplay;
using Project.SaveLoadSystem;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class SaveLoadSystemFactory
    {
        private readonly GameObject _saveLoadSystemPrefab;

        public SaveLoadSystemFactory(GameObject saveLoadSystemPrefab)
        {
            _saveLoadSystemPrefab = saveLoadSystemPrefab;
        }

        public SaveLoadSystemCreateData Create()
        {
            SaveLoadSystemCreateData data = new();

            data.SaveLoadSystemGameObject = GameObject.Instantiate(_saveLoadSystemPrefab);
            data.SaveLoadController = data.SaveLoadSystemGameObject.GetComponent<SaveLoadController>();
            data.LevelProgress = new LevelProgress();

            data.SaveLoadController.Initilialize(data.LevelProgress);

            return data;
        }
    }
}