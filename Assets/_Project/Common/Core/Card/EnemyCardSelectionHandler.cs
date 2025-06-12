using System;
using Project.Core.Sevices;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Project.Core.Card
{
    public class EnemyCardSelectionHandler : IInitializable, IDisposable
    {
        public event Action<CardCreatedData> OnSelect;
        public event Action<CardCreatedData> OnAttack;

        private readonly Button _button;
        private readonly CardCreatedData _character;

        private int _clickCount;

        public bool IsSelection { get; private set; } = false; 

        public EnemyCardSelectionHandler(Button button, CardCreatedData character)
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
}