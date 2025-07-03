using GamePush;

namespace Project.SaveLoadSystem
{
    public class GPSaveLoad : ISaveLoad
    {
        public PlayerSaveData Load() =>
            new PlayerSaveData { CurrentLevel = GP_Player.GetInt("Level") };

        public void Save(PlayerSaveData playerSaveData)
        {
            GP_Player.Set("Level", playerSaveData.CurrentLevel);
            GP_Player.Sync();
        }
    }
}