using System;
using Project.Core.Sevices;
using Unity.VisualScripting;

namespace Project.Core.Card
{
    public class AttackController : IInitializable, IDisposable
    {
        private readonly CardHandlerRepository _enemyHandlerRepository;
        private readonly CardStats _playerCardStats;

        public AttackController(
            CardHandlerRepository enemyHandlerRepository, 
            CardStats playerCardStats)
        {
            _enemyHandlerRepository = enemyHandlerRepository;
            _playerCardStats = playerCardStats;
        }

        public void Initialize() =>
            _enemyHandlerRepository.OnAttack += Attack;

        public void Dispose() =>
            _enemyHandlerRepository.OnAttack -= Attack;

        public void Attack(CardModel cardModel) =>
            cardModel.Health.TakeDamage(_playerCardStats.Damage);
    }
}