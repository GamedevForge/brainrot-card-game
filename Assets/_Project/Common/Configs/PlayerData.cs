using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Project/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public CardData DefualtCardData { get; private set; }
    }
}