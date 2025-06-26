using System.IO;
using UnityEngine;

namespace Project.SaveLoadSystem
{
    public class SaveLoad : ISaveLoad
    {
        private readonly string _filePath;

        public SaveLoad() =>
            _filePath = Path.Combine(Application.persistentDataPath, "PlayerData.txt");

        public void Save(PlayerSaveData playerSaveData)
        {
            string playerSaveDataJson = JsonUtility.ToJson(playerSaveData);
            File.WriteAllText(_filePath, playerSaveDataJson);
        }

        public PlayerSaveData Load()
        {
            if (File.Exists(_filePath) == false)
            {
                PlayerSaveData initialData = new();
                Save(initialData);
                return initialData;
            }

            string playerSaveDataJson = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<PlayerSaveData>(playerSaveDataJson);
        }
    }
}