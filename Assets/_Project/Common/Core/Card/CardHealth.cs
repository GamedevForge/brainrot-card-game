using System;
using UnityEngine;

namespace Project.Core.Card
{
    public class CardHealth
    {
        public event Action OnDead;
        public event Action<int> OnTakedGamage;
        
        private int _health;
        private int _maxHealth;

        public bool IsAlive { get; private set; } = true;
        
        public void SetMaxHealth(int maxHealth) =>
            _maxHealth = maxHealth;

        public void Revive()
        {
            IsAlive = true;
            _health = _maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            Mathf.Max(_health - damage, 0);
            OnTakedGamage?.Invoke(_health);

            if (_health == 0) 
                OnDead?.Invoke();
        }
    }
}