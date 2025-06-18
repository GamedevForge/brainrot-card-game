using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "AnimationsData", menuName = "Project/AnimationsData")]
    public class AnimationsData : ScriptableObject
    {
        [field: SerializeField] public float CardMoveDuration { get; private set; }
        [field: SerializeField] public float PopupShowAndHideDuration { get; private set; }
        [field: SerializeField] public float WindowAnimationDuration { get; private set; }
        [field: SerializeField] public float UpgradeUIElementsShowAndHideDuration { get; private set; }
    }
}