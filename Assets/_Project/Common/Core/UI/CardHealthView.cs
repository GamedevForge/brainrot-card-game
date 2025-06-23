using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Core.Sevices;
using UnityEngine;

namespace Project.Core.UI
{
    public class CardHealthView
    {
        private readonly CardCreatedData _card;
        private readonly RectTransform _cardRectTransform;
        private readonly float _takeDamageDuration;
        private readonly float _rotateDelta;

        public CardHealthView(
            CardCreatedData card,
            float takeDamageDuration,
            float rotateDelta)
        {
            _card = card;
            _cardRectTransform = _card.CardGameObject.GetComponent<RectTransform>();
            _takeDamageDuration = takeDamageDuration;
            _rotateDelta = rotateDelta;
        }

        public async UniTask OnTakedDamage()
        {
            Vector3 startRotation = _cardRectTransform.rotation.eulerAngles;
            await _cardRectTransform.DORotate(
                _cardRectTransform.rotation.eulerAngles + new Vector3(0f, 0f, _rotateDelta / 2f), 
                _takeDamageDuration).AsyncWaitForCompletion();
            await _cardRectTransform.DORotate(
                startRotation, 
                _takeDamageDuration / 2f).AsyncWaitForCompletion();
        }

        public async UniTask OnKill()
        {

        }
    }
}