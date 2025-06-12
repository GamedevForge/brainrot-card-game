using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "Project/LevelsData")]
    public class LevelsData : ScriptableObject
    {
        [field: SerializeField] public LevelData[] LevelDatas { get; private set; }
    }
}