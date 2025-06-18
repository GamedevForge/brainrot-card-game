using System.Collections.Generic;

namespace Project.Core.Sevices
{
    public class CardObjectPool
    {
        private readonly UnityEngine.Pool.ObjectPool<CardCreatedData> _cardPool;
        private readonly CardFactory _factory;

        public CardObjectPool(CardFactory factory, int poolMaxSize)
        {
            _factory = factory;
            _cardPool = new UnityEngine.Pool.ObjectPool<CardCreatedData>(
                OnCreate, 
                OnGet, 
                OnRelease, 
                OnDestroy, 
                true, 
                poolMaxSize);
        }

        private void OnGet(CardCreatedData createdData) =>
            createdData.CardGameObject.SetActive(true);

        private void OnRelease(CardCreatedData createdData) =>
            createdData.CardGameObject.SetActive(false);

        private void OnDestroy(CardCreatedData createdData)
        {
            createdData.SelectionHandler.Dispose();
            createdData.CardView.Dispose();
            createdData.SelectionHandler.Dispose();
        }

        private CardCreatedData OnCreate()
        {
            CardCreatedData cardCreated = _factory.Create();
            cardCreated.SelectionHandler.Initialize();
            cardCreated.CardView.Initialize();
            cardCreated.CardSelectionView.Initialize();
            return cardCreated;
        }

        public CardCreatedData Get() =>
            _cardPool.Get();

        public void Release(CardCreatedData createdData) =>
            _cardPool.Release(createdData);
    }
}