using System;
using Project.Configs;
using Project.Core.Card;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class PlayerCardFactory : IDisposable
    {
        private readonly CardFactory _cardFactory;
        private readonly RectTransform _playerCardParent;
        private readonly CardData _cardData;

        private CardCreatedData _playerCard;

        public PlayerCardFactory(
            CardFactory cardFactory, 
            RectTransform playerCardParent, 
            CardData cardData)
        {
            _cardFactory = cardFactory;
            _playerCardParent = playerCardParent;
            _cardData = cardData;
        }
        
        public void Dispose()
        {
            _playerCard.CardView.Dispose();
        }

        public CardCreatedData Create()
        {
            _playerCard = _cardFactory.Create();

            _playerCard.CardGameObject.transform.SetParent(_playerCardParent);
            _playerCard.CardGameObject.transform.localPosition = Vector3.zero;
            _playerCard.SelectionHandler = new EnemyCardSelectionHandler(
                null,
                _playerCard);
            _playerCard.StartPosition = _playerCard
                .CardGameObject
                .GetComponent<RectTransform>()
                .anchoredPosition3D;
            _playerCard.CardStats = new CardStats();
            _playerCard.CardStats.Health = _cardData.Health;
            _playerCard.CardStats.Damage = _cardData.Damage;
            _playerCard.CardComponents = _playerCard
                .CardGameObject
                .GetComponent<CardComponents>();
            _playerCard.CardComponents.MainImage.sprite = _cardData.Sprite;
            _playerCard.CardComponents.DamageTextIndex.text = _cardData.Damage.ToString();
            _playerCard.CardComponents.HealthTextIndex.text = _cardData.Health.ToString();
            _playerCard.Health = new CardHealth(_playerCard);
            _playerCard.CardView.Initialize();

            return _playerCard;
        }
    }
}