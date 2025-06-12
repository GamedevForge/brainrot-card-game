using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Project.Core.Card;

namespace Project.Core.UpgradeSystem
{
    public class UpgradeController
    {
        public event Action OnUpgrade;
        
        private readonly CardStats _playerCardStats;

        public UpgradeController(CardStats playerCardStats) =>
            _playerCardStats = playerCardStats;

        public void UpgradeHealth(int health)
        {
            _playerCardStats.Health += health;
            OnUpgrade?.Invoke();
        }

        public void UpgradeDamage(int damage)
        {
            _playerCardStats.Damage += damage;
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
    }
}