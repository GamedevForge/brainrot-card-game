using System.IO;
using UnityEngine;

namespace Project.SaveLoadSystem
{
    public class SaveLoad
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
            string playerSaveDataJson = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<PlayerSaveData>(playerSaveDataJson);
        }
    }
}