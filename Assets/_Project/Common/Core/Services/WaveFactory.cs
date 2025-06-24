using System.Collections.Generic;
using Project.Configs;
using Project.Core.Card;

namespace Project.Core.Sevices
{
    public class WaveFactory
    {
        private readonly CardObjectPool _cardPool;
        private readonly CardHandlerRepository _handlerRepository;

        public WaveFactory(CardObjectPool cardPool, CardHandlerRepository handlerRepository)
        {
            _cardPool = cardPool;
            _handlerRepository = handlerRepository;
        }

        public List<CardCreatedData> Create(WaveConfig waveConfig)
        {
            List<CardCreatedData> cardDatasList = new();
            
            foreach(EnemyCardConfig cardData in waveConfig.CardDatas)
            {
                CardCreatedData createdCard = _cardPool.Get();
                CardComponents cardComponents = createdCard.CardComponents;
                cardComponents.MainImage.sprite = cardData.EnemyCardData.CardSprite;
                cardComponents.CardForceIndex.text = cardData.CardForce.ToString();
                _handlerRepository.Add(createdCard);
                cardDatasList.Add(createdCard);
                createdCard.Health.SetMaxHealth(cardData.CardForce);
                createdCard.Health.Revive();
                createdCard.CardStats.CardForce = cardData.CardForce;
                createdCard.CanvasGroup.alpha = 1f;
                createdCard.AudioClip = cardData.EnemyCardData.AudioClip;
            }

            return cardDatasList;
        }
    }
}