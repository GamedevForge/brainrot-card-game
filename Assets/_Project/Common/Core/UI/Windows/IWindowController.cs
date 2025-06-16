using Cysharp.Threading.Tasks;

namespace Project.Core.UI.Windows
{
    public interface IWindowController
    {
        UniTask ShowAsync();
        UniTask HideAsync();
    }
}