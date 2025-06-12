using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Project/GameData")]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public int MaxCardPoolSize { get; private set; }
    }
}