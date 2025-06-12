using UnityEngine;

namespace Project.Core.Card
{
    public class CardComponents : MonoBehaviour
    {
        [field: SerializeField] public Sprite MainSprite { get; set; } 
        [field: SerializeField] public TMPro.TMP_Text DamageTextIndex { get; set; }
        [field: SerializeField] public TMPro.TMP_Text HealthTextIndex { get; set; }
    }
}