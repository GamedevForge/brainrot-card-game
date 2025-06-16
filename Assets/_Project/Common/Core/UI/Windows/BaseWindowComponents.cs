using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI.Windows
{
    public class BaseWindowComponents : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
    }
}