using Project.Configs;
using Project.Core.Card;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Services
{
    public class CardFactory
    {
        private readonly GameObject _basePrefab;
        private readonly AnimationsData _animationsData;

        public CardFactory(GameObject basePrefab, AnimationsData animationsData)
        {
            _basePrefab = basePrefab;
            _animationsData = animationsData;
        }

        public CardCreatedData Create()
        {
            CardCreatedData data = new();
            
            data.CardGameObject = GameObject.Instantiate(_basePrefab);
            data.SelectionHandler = new EnemyCardSelectionHandler(
                data.CardGameObject.GetComponentInChildren<Button>(),
                data);
            data.Health = new CardHealth(data, new UI.CardHealthView(
                data, 
                _animationsData.OnAttackDuration, 
                _animationsData.OnAttackRotateDelta,
                _animationsData.OnDeadMoveOffset,
                _animationsData.OnDeadDuration));
            data.CardComponents = data.CardGameObject.GetComponent<CardComponents>();
            data.CardStats = new CardStats();
            data.CardView = new UI.CardView(data);
            data.CardSelectionView = new UI.CardSelectionView(
                data.CardComponents.OutLineGameObject,
                data.SelectionHandler);
            data.CanvasGroup = data.CardGameObject.GetComponentInChildren<CanvasGroup>();

            return data;
        }
    }
}