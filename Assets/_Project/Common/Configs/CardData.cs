using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Project/CardData")]
    public class CardData : BaseCardData
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
}