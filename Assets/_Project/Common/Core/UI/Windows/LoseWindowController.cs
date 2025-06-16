using Cysharp.Threading.Tasks;
using Project.Core.UI.Animtions;
using UnityEngine.UI;

namespace Project.Core.UI.Windows
{
    public class LoseWindowController : BaseWindowController
    {
        private readonly LoseWindowModel _model;
        private readonly AlphaAnimation _animation;

        public LoseWindowController(
            LoseWindowModel model,
            Button goToMenu,
            AlphaAnimation animation) : base(model, goToMenu)
        {
            _model = model;
            _animation = animation;
        }

        public override UniTask HideAsync() =>
            _animation.PlayAnimationAsync(_model.CanvasGroup, 0f);

        public override UniTask ShowAsync() =>
            _animation.PlayAnimationAsync(_model.CanvasGroup, 1f);
        
        protected override void OnClick() =>
            TriggerEvent();
    }
}