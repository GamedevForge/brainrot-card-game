using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "UpgradeCardData", menuName = "Project/UpgradeCardData")]
    public class UpgradeCardData : BaseCardData
    {
        [field: SerializeField] public CardData UpgradeCardTo { get; private set; }
    }
}