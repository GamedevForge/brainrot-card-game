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
            
            foreach(CardData cardData in waveConfig.CardDatas)
            {
                CardCreatedData createdCard = _cardPool.Get();
                CardComponents cardComponents = createdCard.CardComponents;
                cardComponents.MainSprite = cardData.Sprite;
                cardComponents.DamageTextIndex.text = cardData.Damage.ToString();
                cardComponents.HealthTextIndex.text = cardData.Health.ToString();
                _handlerRepository.Add(createdCard);
                cardDatasList.Add(createdCard);
                createdCard.Health.SetMaxHealth(cardData.Health);
                createdCard.Health.Revive();
            }

            return cardDatasList;
        }
    }
}