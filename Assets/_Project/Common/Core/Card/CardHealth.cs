using System;
using Cysharp.Threading.Tasks;
using Project.Core.Services;
using Project.Core.UI;
using UnityEngine;

namespace Project.Core.Card
{
    public class CardHealth
    {
        public event Action<CardCreatedData> OnDead;
        public event Action<int> OnTakedGamage;
        public event Action<int> OnRevive;

        private readonly CardCreatedData _cardCreatedData;
        private readonly CardHealthView _view;
        
        private int _health;
        private int _maxHealth;

        public bool IsAlive { get; private set; } = true;

        public CardHealth(CardCreatedData cardCreatedData, 
            CardHealthView view)
        {
            _cardCreatedData = cardCreatedData;
            _view = view;
        }

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
        
        public async UniTask TakeDamage(int damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            _cardCreatedData.CardStats.CardForce = _health;
            _cardCreatedData.CardComponents.CardForceIndex.text = _health.ToString();
            await _view.OnTakedDamage();
            OnTakedGamage?.Invoke(_health);

            if (_health == 0)
            {
                IsAlive = false;
                await _view.OnKill();
                OnDead?.Invoke(_cardCreatedData);
            }
        }
    }
}