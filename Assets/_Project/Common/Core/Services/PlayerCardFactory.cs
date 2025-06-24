using System;
using Project.Configs;
using Project.Core.Card;
using UnityEngine;

namespace Project.Core.Services
{
    public class PlayerCardFactory : IDisposable
    {
        private readonly CardFactory _cardFactory;
        private readonly RectTransform _playerCardParent;
        private readonly EnemyCardConfig _cardData;
        private readonly AnimationsData _animationsData;

        private CardCreatedData _playerCard;

        public PlayerCardFactory(
            CardFactory cardFactory,
            RectTransform playerCardParent,
            EnemyCardConfig cardData,
            AnimationsData animationsData)
        {
            _cardFactory = cardFactory;
            _playerCardParent = playerCardParent;
            _cardData = cardData;
            _animationsData = animationsData;
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
            _playerCard.CardStats.CardForce = _cardData.CardForce;
            _playerCard.CardComponents = _playerCard
                .CardGameObject
                .GetComponent<CardComponents>();
            _playerCard.CardComponents.MainImage.sprite = _cardData.EnemyCardData.CardSprite;
            _playerCard.CardComponents.CardForceIndex.text = _cardData.CardForce.ToString();
            _playerCard.Health = new CardHealth(
                _playerCard, 
                new UI.CardHealthView(
                    _playerCard, 
                    _animationsData.OnAttackDuration, 
                    _animationsData.OnAttackRotateDelta,
                    _animationsData.OnDeadMoveOffset,
                    _animationsData.OnDeadDuration));
            _playerCard.AudioClip = _cardData.EnemyCardData.AudioClip;
            _playerCard.Health.SetMaxHealth(_cardData.CardForce);
            _playerCard.Health.Revive();
            _playerCard.CardView.Initialize();

            return _playerCard;
        }
    }
}