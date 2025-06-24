using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Card
{
    public class CardComponents : MonoBehaviour
    {
        [field: SerializeField] public Image MainImage { get; private set; } 
        [field: SerializeField] public TMPro.TMP_Text CardForceIndex { get; private set; }
        [field: SerializeField] public GameObject OutLineGameObject { get; private set; }
        [field: SerializeField] public ParticleSystem UpgradeParticleSystem { get; private set; }
        [field: SerializeField] public ParticleSystem OnTakeDamageParticleSystem { get; private set; }
        [field: SerializeField] public ParticleSystem OnDeadParticleSystem { get; private set; }
    }
}