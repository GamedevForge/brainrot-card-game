using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Card
{
    public class CardComponents : MonoBehaviour
    {
        [field: SerializeField] public Image MainImage { get; set; } 
        [field: SerializeField] public TMPro.TMP_Text CardForceIndex { get; set; }
    }
}