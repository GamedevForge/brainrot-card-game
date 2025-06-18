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
                .position;
            _playerCard.CardStats = new CardStats();
            _playerCard.CardStats.Health = _cardData.CardForce;
            _playerCard.CardStats.Damage = _cardData.CardForce;
            _playerCard.CardComponents = _playerCard
                .CardGameObject
                .GetComponent<CardComponents>();
            _playerCard.CardComponents.MainImage.sprite = _cardData.Sprite;
            _playerCard.CardComponents.CardForceIndex.text = _cardData.CardForce.ToString();
            _playerCard.Health = new CardHealth(_playerCard);
            _playerCard.Health.SetMaxHealth(_cardData.CardForce);
            _playerCard.Health.Revive();
            _playerCard.CardView.Initialize();

            return _playerCard;
        }
    }
}