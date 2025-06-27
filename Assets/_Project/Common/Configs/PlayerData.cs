using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Project/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public CardConfig DefualtCardData { get; private set; }
    }
}