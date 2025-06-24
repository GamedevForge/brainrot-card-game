using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Project.Core.Services;
using Project.Configs;
using UnityEngine;

namespace Project.Core.UpgradeSystem
{
    public class UpgradeController
    {
        public event Action OnUpgrade;
        
        private readonly CardCreatedData _playerCard;
        private readonly UpgradeModel _upgradeModel;
        private readonly AudioSource _audioSource;

        public UpgradeController(
            CardCreatedData playerCard,
            UpgradeModel upgradeModel,
            AudioSource audioSource)
        {
            _playerCard = playerCard;
            _upgradeModel = upgradeModel;
            _audioSource = audioSource;
        }

        public async void UpgradeForce(UpgradeValueConfig force)
        {
            if (force.Type == UpgradeValueType.Addition)
                _playerCard.Health.SetHealth(_playerCard.CardStats.CardForce + force.Value);
            else
                _playerCard.Health.SetHealth(_playerCard.CardStats.CardForce * force.Value);
            _playerCard.AudioClip = force.UpgradeCardData.UpgradeCardTo.AudioClip;

            _playerCard.CardComponents.UpgradeParticleSystem.Play();

            if (_playerCard.AudioClip != null)
            {
                _audioSource.PlayOneShot(_playerCard.AudioClip);
                //_audioSource.clip = _playerCard.AudioClip;
                //audioSource.Play();
                await UniTask.WhenAll(
                    UniTask.WaitForSeconds(_playerCard.AudioClip.length),
                    UniTask.WaitForSeconds(_playerCard.CardComponents.UpgradeParticleSystem.main.duration * 3f));
            }
            else
            {
                await UniTask.WaitForSeconds(_playerCard.CardComponents.UpgradeParticleSystem.main.duration * 3f);
            }

            OnUpgrade?.Invoke();
        }

        public async UniTask AsyncWaitToUpgrade(CancellationToken cancellationToken = default)
        {
            var tcs = new UniTaskCompletionSource();

            void OnDone()
            {
                OnUpgrade -= OnDone;
                tcs.TrySetResult();
            }

            OnUpgrade += OnDone;
            try
            {
                await tcs.Task.AttachExternalCancellation(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        public void SetFromTo(
            UpgradeValueConfig from, 
            UpgradeValueConfig to)
        {
            _upgradeModel.UpgradeFrom = from;
            _upgradeModel.UpgradeTo = to;
        }
    }
}