using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
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
        private readonly AudioClip _sfxOnPreassedButton;
        private readonly AudioSource _audioSource;

        protected BaseWindowController(
            IWindowModel model,
            Button button,
            AudioClip sfxOnPreassedButton,
            AudioSource audioSource)
        {
            _model = model;
            _button = button;
            _sfxOnPreassedButton = sfxOnPreassedButton;
            _audioSource = audioSource;
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

        protected async UniTask PlaySoundOnPressedButtonAsync()
        {
            PlaySoundOnPressedButton();
            await UniTask.WaitForSeconds(_sfxOnPreassedButton.length);
        }

        protected void PlaySoundOnPressedButton() =>
            PlaySound(_sfxOnPreassedButton);

        protected void PlaySound(AudioClip audioClip) =>
            _audioSource.PlayOneShot(audioClip);
    }
}