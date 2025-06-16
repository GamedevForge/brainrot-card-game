using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Project.Core.UI.Windows
{
    public abstract class BaseWindowController : 
        IWindowController,
        IInitializable,
        IDisposable
    {
        public event Action OnClickButton;

        private readonly IWindowModel _model;
        private readonly Button _button;
        
        protected BaseWindowController(
            IWindowModel model,
            Button button)
        {
            _model = model;
            _button = button;
        }

        public void Initialize() =>
            _button.onClick.AddListener(OnClick);

        public void Dispose() =>
            _button.onClick.AddListener(OnClick);

        public void EnableWindowGameObject() =>
            _model.WindowGameObject.SetActive(true);

        public void DisableWindowGameObject() =>
            _model.WindowGameObject.SetActive(false);

        public async UniTask AsyncWaitToClickOnGameplayButton(CancellationToken cancellationToken = default)
        {
            var tcs = new UniTaskCompletionSource();

            void OnDone()
            {
                OnClickButton -= OnDone;
                tcs.TrySetResult();
            }

            OnClickButton += OnDone;
            try
            {
                await tcs.Task.AttachExternalCancellation(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        public abstract UniTask HideAsync();

        public abstract UniTask ShowAsync();

        protected abstract void OnClick();

        protected void TriggerEvent() =>
            OnClickButton?.Invoke();
    }
}