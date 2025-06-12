using Project.Configs;
using Project.Core.Card;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Sevices
{
    public class CardFactory
    {
        private readonly GameObject _basePrefab;

        public CardFactory(GameObject basePrefab) =>
            _basePrefab = basePrefab;

        public CardCreatedData Create()
        {
            CardCreatedData data = new();
            
            data.CardGameObject = GameObject.Instantiate(_basePrefab);
            data.SelectionHandler = new EnemyCardSelectionHandler(
                data.CardGameObject.GetComponent<Button>(),
                data.CardModel);
            data.CardModel = new CardModel(new CardHealth(), data.SelectionHandler);
            data.CardComponents = data.CardGameObject.GetComponent<CardComponents>();
            data.CardStats = new CardStats();

            return data;
        }
    }

    public class CardObjectPool
    {
        
    }
}