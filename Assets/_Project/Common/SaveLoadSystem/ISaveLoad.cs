namespace Project.SaveLoadSystem
{
    public interface ISaveLoad
    {
        void Save(PlayerSaveData playerSaveData);
        PlayerSaveData Load();
    }
}