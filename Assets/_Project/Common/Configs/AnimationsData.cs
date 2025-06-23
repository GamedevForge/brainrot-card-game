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
        [field: SerializeField] public float OnAttackDuration { get; private set; } = 0.3f;
        [field: SerializeField] public float OnAttackRotateDelta { get; private set; } = -15f;
        [field: SerializeField] public float OnDeadDuration { get; private set; } = 0.4f;
        [field: SerializeField] public float OnDeadMoveOffset { get; private set; } = 20f;
    }
}