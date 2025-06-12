using Project.Core.Card;
using UnityEngine;

namespace Project.Core.Sevices
{
    public struct CardCreatedData
    {
        public GameObject CardGameObject;
        public EnemyCardSelectionHandler SelectionHandler;
        public CardModel CardModel;
        public CardComponents CardComponents;
        public CardStats CardStats;
    }
}