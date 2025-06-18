using System;
using Project.Core.Sevices;
using UnityEngine;

namespace Project.Core.Card
{
    public class CardHealth
    {
        public event Action<CardCreatedData> OnDead;
        public event Action<int> OnTakedGamage;
        public event Action<int> OnRevive;

        private readonly CardCreatedData _cardCreatedData;
        
        private int _health;
        private int _maxHealth;

        public bool IsAlive { get; private set; } = true;
        
        public CardHealth(CardCreatedData cardCreatedData) =>
            _cardCreatedData = cardCreatedData;
        
        public void SetHealth(int health)
        {
            _health = health;
            if (_health > _maxHealth)
                _maxHealth = _health;
            _cardCreatedData.CardStats.CardForce = _health;
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            _cardCreatedData.CardStats.CardForce = _health;
        }

        public void Revive()
        {
            IsAlive = true;
            _health = _maxHealth;
            _cardCreatedData.CardStats.CardForce = _health;
            OnRevive?.Invoke(_health);
        }
        
        public void TakeDamage(int damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            _cardCreatedData.CardStats.CardForce = _health;
            OnTakedGamage?.Invoke(_health);

            if (_health == 0)
            {
                IsAlive = false;
                OnDead?.Invoke(_cardCreatedData);
            }
        }
    }
}