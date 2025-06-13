using UnityEngine;

namespace Project.Core.UI.Windows
{
    public class LoseWindowModel : IWindowModel 
    {
        public GameObject WindowGameObject { get; }
        public CanvasGroup CanvasGroup { get; }

        public LoseWindowModel(
            GameObject windowGameObject, 
            CanvasGroup canvasGroup)
        {
            WindowGameObject = windowGameObject;
            CanvasGroup = canvasGroup;
        }
    }
}