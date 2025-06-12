using Cysharp.Threading.Tasks;
using Project.Core.Sevices;
using Project.Core.UI;
using UnityEngine;

namespace Project.Core.Gameplay
{
    public class AttackController
    {
        private readonly RectTransform _playerCardRectTransform;
        private readonly MoveAnimation _moveAnimation;
        private readonly CardHandlerRepository _handlerRepository;
        private readonly CardCreatedData _playerCard;

        public AttackController(
            RectTransform playerCardRectTransform, 
            MoveAnimation moveAnimation, 
            CardHandlerRepository handlerRepository, 
            CardCreatedData playerCard)
        {
            _playerCardRectTransform = playerCardRectTransform;
            _moveAnimation = moveAnimation;
            _handlerRepository = handlerRepository;
            _playerCard = playerCard;
        }

        public async UniTask AttackEnemy()
        {
            await _moveAnimation.MoveAsync(
                _playerCardRectTransform,
                _playerCardRectTransform.anchoredPosition3D,
                _handlerRepository
                    .CurrentCardModel
                    .CardGameObject
                    .GetComponent<RectTransform>()
                    .anchoredPosition);

            _handlerRepository
                .CurrentCardModel
                .Health
                .TakeDamage(_playerCard.CardStats.Damage);

            await _moveAnimation.MoveAsync(
                _playerCardRectTransform,
                _playerCardRectTransform.anchoredPosition3D,
                _playerCard.StartPosition);
        }

        public async UniTask AttackPlayer()
        {

        }
    }
}