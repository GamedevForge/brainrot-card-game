using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI
{
    public class UpgradeUIElementComponents : MonoBehaviour
    {
        [field: SerializeField] public Image MainImage { get; private set; }
        [field: SerializeField] public TMPro.TMP_Text UpgradeIndexText { get; private set; }
    }
}