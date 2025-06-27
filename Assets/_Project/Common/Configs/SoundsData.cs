using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "SoundsData", menuName = "Project/SoundsData")]
    public class SoundsData : ScriptableObject
    {
        [field: SerializeField] public AudioClip WindowButtonSFX { get; private set; }
        [field: SerializeField] public AudioClip CardSelectSFX { get; private set; }
        [field: SerializeField] public AudioClip OnAttackCardSFX { get; private set; }
        [field: SerializeField] public AudioClip OnCardDeadSFX { get; private set; }
    }
}