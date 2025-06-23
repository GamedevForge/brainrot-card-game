using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Core.Sevices;
using Project.Core.UI.Animtions;
using UnityEngine;

namespace Project.Core.UI
{
    public class CardHealthView
    {
        private readonly CardCreatedData _card;
        private readonly RectTransform _cardRectTransform;
        private readonly float _takeDamageDuration;
        private readonly float _rotateDelta;
        private readonly AlphaAnimation _alphaAnimation;
        private readonly MoveAnimation _moveAnimation;
        private readonly float _moveOffsetOnDead;

        public CardHealthView(
            CardCreatedData card,
            float takeDamageDuration,
            float rotateDelta,
            float moveOffsetOnDead,
            float onDeadAnimationDuration)
        {
            _card = card;
            _cardRectTransform = _card.CardGameObject.GetComponent<RectTransform>();
            _takeDamageDuration = takeDamageDuration;
            _rotateDelta = rotateDelta;
            _alphaAnimation = new AlphaAnimation(onDeadAnimationDuration);
            _moveAnimation = new MoveAnimation(onDeadAnimationDuration);
            _moveOffsetOnDead = moveOffsetOnDead;
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
            await UniTask.WhenAll(
                _alphaAnimation.PlayAnimationAsync(
                    _card.CanvasGroup, 
                    0f),
                _moveAnimation.MoveAsync(
                    _cardRectTransform,
                    _cardRectTransform.position, 
                    _cardRectTransform.position - new Vector3(0f, _moveOffsetOnDead, 0f)));
            _card.CardComponents.OnDeadParticleSystem.Play();
            await UniTask.WaitForSeconds(_card.CardComponents.OnDeadParticleSystem.main.duration * 3.2f);
        }
    }
}