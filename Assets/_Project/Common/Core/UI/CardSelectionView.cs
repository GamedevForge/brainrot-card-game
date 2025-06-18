using Project.Core.Card;
using Project.Core.Sevices;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Project.Core.UI
{
    public class CardSelectionView : IInitializable, IDisposable
    {
        private readonly Outline _cardOutLine;
        private readonly EnemyCardSelectionHandler _enemyCardSelectionHandler;

        public CardSelectionView(
            Outline cardOutLine,
            EnemyCardSelectionHandler enemyCardSelectionHandler)
        {
            _cardOutLine = cardOutLine;
            _enemyCardSelectionHandler = enemyCardSelectionHandler;
        }

        public void Initialize()
        {
            _enemyCardSelectionHandler.OnSelect += Selecet;
            _enemyCardSelectionHandler.OnReset += Reset;
        }

        public void Dispose()
        {
            _enemyCardSelectionHandler.OnSelect -= Selecet;
            _enemyCardSelectionHandler.OnReset -= Reset;
        }

        private void Selecet(CardCreatedData _) =>
            _cardOutLine.enabled = true;

        private void Reset() =>
            _cardOutLine.enabled = false;
    }
}