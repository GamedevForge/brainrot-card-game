using Project.Core.Card;
using Project.Core.Services;
using UnityEngine;

namespace Project.Ai
{
    public class AiActor
    {
        //private readonly CardStats _playerCard;

        //public AiActor(CardStats playerCard) =>
        //    _playerCard = playerCard;

        public CardCreatedData GetCardForAttackPlayer(CardCreatedData[] enemyCardDatas) =>
            GetRandomCard(enemyCardDatas);

        private CardCreatedData GetRandomCard(CardCreatedData[] enemyCardDatas) =>
            enemyCardDatas[Random.Range(0, enemyCardDatas.Length)];
    }
}