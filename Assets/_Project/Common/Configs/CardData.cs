using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Project/CardData")]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int CardForce { get; private set; } 
    }
}