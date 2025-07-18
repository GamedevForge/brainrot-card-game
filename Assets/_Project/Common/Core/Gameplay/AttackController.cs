﻿using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Services;
using Project.Core.UI.Animtions;
using UnityEngine;

namespace Project.Core.Gameplay
{
    public class AttackController
    {
        private readonly RectTransform _playerCardRectTransform;
        private readonly MoveAnimation _moveAnimation;
        private readonly CardHandlerRepository _handlerRepository;
        private readonly CardCreatedData _playerCard;
        private readonly AudioSource _audioSource;
        private readonly SoundsData _soundsData;

        public AttackController(
            RectTransform playerCardRectTransform,
            MoveAnimation moveAnimation,
            CardHandlerRepository handlerRepository,
            CardCreatedData playerCard,
            AudioSource audioSource,
            SoundsData soundsData)
        {
            _playerCardRectTransform = playerCardRectTransform;
            _moveAnimation = moveAnimation;
            _handlerRepository = handlerRepository;
            _playerCard = playerCard;
            _audioSource = audioSource;
            _soundsData = soundsData;
        }

        public async UniTask AttackEnemy()
        {
            Vector3 playerCardStartPosition = _playerCardRectTransform.position;

            if (_handlerRepository.CurrentCardModel.AudioClip != null)
            {
                _audioSource.PlayOneShot(_handlerRepository.CurrentCardModel.AudioClip);
                await UniTask.WaitForSeconds(_handlerRepository.CurrentCardModel.AudioClip.length);
            }

            _handlerRepository.CurrentCardModel.CardGameObject.transform.SetAsLastSibling();
            _playerCard.CardGameObject.transform.SetAsLastSibling();

            await _moveAnimation.MoveAsync(
                _playerCardRectTransform,
                playerCardStartPosition,
                _handlerRepository
                    .CurrentCardModel
                    .CardGameObject
                    .GetComponent<RectTransform>()
                    .position);

            _audioSource.PlayOneShot(_soundsData.OnAttackCardSFX);
            if (_handlerRepository.CurrentCardModel.CardStats.CardForce >
                _playerCard.CardStats.CardForce)
            {
                await UniTask.WhenAll(
                    _playerCard.Health.TakeDamage(_handlerRepository.CurrentCardModel.CardStats.CardForce),
                    _moveAnimation.MoveAsync(
                        _playerCardRectTransform,
                        _playerCardRectTransform.position,
                        playerCardStartPosition));
            }
            else
            {
                _handlerRepository.CurrentCardModel.CardComponents.OnTakeDamageParticleSystem.Play();
                await UniTask.WhenAll(_handlerRepository
                        .CurrentCardModel
                        .Health
                        .TakeDamage(_playerCard.CardStats.CardForce),
                        _moveAnimation.MoveAsync(
                            _playerCardRectTransform,
                            _playerCardRectTransform.position,
                            playerCardStartPosition));
            }
        }

        public async UniTask AttackPlayer(CardCreatedData enemyCard)
        {
            RectTransform enemyCardRectTransform = enemyCard
                .CardGameObject
                .GetComponent<RectTransform>();

            Vector3 startPosition = enemyCardRectTransform.position;

            await _moveAnimation.MoveAsync(
                enemyCardRectTransform,
                startPosition,
                _playerCardRectTransform.position);
            
            await _playerCard.Health.TakeDamage(enemyCard.CardStats.CardForce);

            await _moveAnimation.MoveAsync(
                enemyCardRectTransform,
                enemyCardRectTransform.position,
                startPosition);
        }
    }
}