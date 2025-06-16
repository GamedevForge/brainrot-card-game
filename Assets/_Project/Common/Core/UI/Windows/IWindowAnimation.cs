using Cysharp.Threading.Tasks;

namespace Project.Core.UI.Windows
{
    public interface IWindowAnimation
    {
        UniTask PlayShowAnimationAsync();

        UniTask PlayHideAnimationAsync();
    }
}