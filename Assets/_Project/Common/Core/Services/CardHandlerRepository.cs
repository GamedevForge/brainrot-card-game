using System;
using System.Collections.Generic;
using Project.Core.Card;

namespace Project.Core.Sevices
{
    public class CardHandlerRepository : IDisposable
    {
        public event Action<CardModel> OnAttack;
        
        private readonly List<EnemyCardSelectionHandler> _selectionHanlders = new();
        
        public void Dispose()
        {
            foreach (EnemyCardSelectionHandler selectionHandler in _selectionHanlders)
            {
                selectionHandler.OnAttack -= SendAttackedCard;
                selectionHandler.OnSelect -= ResetAllSelectedCard;
            }
        }

        public void Add(EnemyCardSelectionHandler selectionHandler)
        {
            _selectionHanlders.Add(selectionHandler);
            selectionHandler.OnAttack += SendAttackedCard;
            selectionHandler.OnSelect += ResetAllSelectedCard;
        }

        public void Remove(EnemyCardSelectionHandler selectionHandler)
        {
            _selectionHanlders.Remove(selectionHandler);
            selectionHandler.OnAttack -= SendAttackedCard;
            selectionHandler.OnSelect -= ResetAllSelectedCard;
        }

        public void ResetAllSelectedCard(CardModel currentSelectionHandler)
        {
            foreach (EnemyCardSelectionHandler selectionHandler in _selectionHanlders)
            {
                if (selectionHandler == currentSelectionHandler.Handler)
                    continue;
                else if (selectionHandler.IsSelection)
                    selectionHandler.ResetSelect();
            }
        }

        private void SendAttackedCard(CardModel currentSelectionHandler) =>
            OnAttack?.Invoke(currentSelectionHandler);
    }
}