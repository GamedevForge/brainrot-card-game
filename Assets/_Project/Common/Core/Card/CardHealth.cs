using System;
using Project.Core.Sevices;
using UnityEngine;

namespace Project.Core.Card
{
    public class CardHealth
    {
        public event Action<CardCreatedData> OnDead;
        public event Action<int> OnTakedGamage;

        private readonly CardCreatedData _cardCreatedData;
        
        private int _health;
        private int _maxHealth;

        public bool IsAlive { get; private set; } = true;
        
        public CardHealth(CardCreatedData cardCreatedData) =>
            _cardCreatedData = cardCreatedData;
        
        public void SetMaxHealth(int maxHealth) =>
            _maxHealth = maxHealth;

        public void Revive()
        {
            IsAlive = true;
            _health = _maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            OnTakedGamage?.Invoke(_health);

            if (_health == 0)
            {
                IsAlive = false;
                OnDead?.Invoke(_cardCreatedData);
            }
        }
    }
}