using UnityEngine;

namespace Project.Core.Card
{
    public class CardComponents : MonoBehaviour
    {
        [field: SerializeField] public Sprite MainSprite { get; private set; } 
        [field: SerializeField] public TMPro.TMP_Text DamageTextIndex { get; private set; }
        [field: SerializeField] public TMPro.TMP_Text HealthTextIndex { get; private set; }
    }
}