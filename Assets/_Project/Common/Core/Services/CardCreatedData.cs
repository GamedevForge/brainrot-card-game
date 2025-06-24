using Project.Core.Card;
using Project.Core.UI;
using UnityEngine;

namespace Project.Core.Services
{
    public class CardCreatedData
    {
        public GameObject CardGameObject;
        public EnemyCardSelectionHandler SelectionHandler;
        public CardComponents CardComponents;
        public CardStats CardStats;
        public CardHealth Health;
        public Vector3 StartPosition;
        public CardView CardView;
        public CardSelectionView CardSelectionView;
        public CanvasGroup CanvasGroup;
        public AudioClip AudioClip;
    }
}