using UnityEngine;

namespace Project.Core.UI.Windows
{
    public class WinWindowModel : IWindowModel
    {
        public GameObject WindowGameObject { get; }
        public CanvasGroup CanvasGroup { get; }

        public WinWindowModel(
            GameObject windowGameObject,
            CanvasGroup canvasGroup)
        {
            WindowGameObject = windowGameObject;
            CanvasGroup = canvasGroup;
        }
    }
}