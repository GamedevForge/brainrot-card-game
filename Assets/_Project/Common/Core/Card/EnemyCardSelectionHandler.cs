using System;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Project.Core.Card
{
    public class EnemyCardSelectionHandler : IInitializable, IDisposable
    {
        public event Action<CardModel> OnSelect;
        public event Action<CardModel> OnAttack;

        private readonly Button _button;
        private readonly CardModel _character;

        private int _clickCount;

        public bool IsSelection { get; private set; } = false; 

        public EnemyCardSelectionHandler(Button button, CardModel character)
        {
            _button = button;
            _character = character;
        }

        public void Initialize() =>
            _button.onClick.AddListener(OnClick);

        public void Dispose() =>
            _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _clickCount++;

            if (_clickCount == 1)
            {
                OnSelect?.Invoke(_character);
                IsSelection = true;
            }
            else if (_clickCount == 2 && IsSelection)
            {
                OnAttack?.Invoke(_character);
                ResetSelect();
            }
        }

        public void ResetSelect()
        {
            _clickCount = 0;
            IsSelection = false;
        }
    }

    public class CardHealth
    {
        public void TakeDamage(int damage)
        {

        }
    }

    public class CardModel
    { 
        public readonly CardHealth Health;
        public readonly EnemyCardSelectionHandler Handler;

        public CardModel(CardHealth health, EnemyCardSelectionHandler handler)
        {
            Health = health;
            Handler = handler;
        }
    }
}