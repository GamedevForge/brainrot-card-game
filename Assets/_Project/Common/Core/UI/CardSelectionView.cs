using Project.Core.Card;
using Project.Core.Sevices;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Core.UI
{
    public class CardSelectionView : IInitializable, IDisposable
    {
        private readonly GameObject _cardOutLine;
        private readonly EnemyCardSelectionHandler _enemyCardSelectionHandler;

        public CardSelectionView(
            GameObject cardOutLine,
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
            _cardOutLine.SetActive(true);

        private void Reset() =>
            _cardOutLine.SetActive(false);
    }
}