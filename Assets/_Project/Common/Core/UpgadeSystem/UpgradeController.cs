using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Project.Core.Sevices;
using Project.Configs;

namespace Project.Core.UpgradeSystem
{
    public class UpgradeController
    {
        public event Action OnUpgrade;
        
        private readonly CardCreatedData _playerCard;
        private readonly UpgradeModel _upgradeModel;

        public UpgradeController(
            CardCreatedData playerCard, 
            UpgradeModel upgradeModel)
        {
            _playerCard = playerCard;
            _upgradeModel = upgradeModel;
        }

        public void UpgradeForce(UpgradeValueConfig force)
        {
            if (force.Type == UpgradeValueType.Addition)
                _playerCard.Health.SetHealth(_playerCard.CardStats.CardForce + force.Value);
            else
                _playerCard.Health.SetHealth(_playerCard.CardStats.CardForce * force.Value);
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