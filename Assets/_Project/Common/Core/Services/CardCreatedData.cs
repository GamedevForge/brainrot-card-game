using Project.Core.Card;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class CardCreatedData
    {
        public GameObject CardGameObject;
        public EnemyCardSelectionHandler SelectionHandler;
        public CardComponents CardComponents;
        public CardStats CardStats;
        public CardHealth Health;
        public Vector3 StartPosition;
    }
}