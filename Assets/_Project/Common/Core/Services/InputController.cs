using UnityEngine.UI;

namespace Project.Core.Services
{
    public class InputController
    {
        private readonly GraphicRaycaster _graphicRaycaster;

        public InputController(GraphicRaycaster graphicRaycaster) =>
            _graphicRaycaster = graphicRaycaster;

        public void EnableInput() =>
            _graphicRaycaster.enabled = true;

        public void DisableInput() => 
            _graphicRaycaster.enabled = false;
    }
}