using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using Project.Core.Card;

namespace Project.Core.Services
{
    public class CardHandlerRepository : IDisposable
    {
        public event Action<CardCreatedData> OnAttack;
        
        private readonly List<CardCreatedData> _selectionHanlders = new();

        public CardCreatedData CurrentCardModel { get; private set; }

        public void Dispose()
        {
            foreach (CardCreatedData selectionHandler in _selectionHanlders)
            {
                selectionHandler.SelectionHandler.OnAttack -= SendAttackedCard;
                selectionHandler.SelectionHandler.OnSelect -= ResetAllSelectedCard;
            }
        }

        public void Add(CardCreatedData selectionHandler)
        {
            _selectionHanlders.Add(selectionHandler);
            selectionHandler.SelectionHandler.OnAttack += SendAttackedCard;
            selectionHandler.SelectionHandler.OnSelect += ResetAllSelectedCard;
        }

        public void Remove(CardCreatedData selectionHandler)
        {
            _selectionHanlders.Remove(selectionHandler);
            selectionHandler.SelectionHandler.OnAttack -= SendAttackedCard;
            selectionHandler.SelectionHandler.OnSelect -= ResetAllSelectedCard;
        }

        public void ResetAllSelectedCard(CardCreatedData currentSelectionHandler)
        {
            foreach (CardCreatedData selectionHandler in _selectionHanlders)
            {
                if (selectionHandler == currentSelectionHandler)
                    continue;
                else if (selectionHandler.SelectionHandler.IsSelection)
                    selectionHandler.SelectionHandler.ResetSelect();
            }
        }

        public async UniTask AsyncWaitToAttack(CancellationToken cancellationToken = default)
        {
            var tcs = new UniTaskCompletionSource();

            void OnDone(object _)
            {
                OnAttack -= OnDone;
                tcs.TrySetResult();
            }

            OnAttack += OnDone;
            try
            {
                await tcs.Task.AttachExternalCancellation(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        private void SendAttackedCard(CardCreatedData currentSelectionHandler)
        {
            CurrentCardModel = currentSelectionHandler;
            OnAttack?.Invoke(CurrentCardModel);
        }
    }
}