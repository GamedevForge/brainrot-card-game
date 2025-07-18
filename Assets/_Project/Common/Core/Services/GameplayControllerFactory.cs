﻿using Project.Configs;
using Project.Core.Gameplay;
using Project.Core.UI;
using Project.Core.UI.Animtions;
using UnityEngine;

namespace Project.Core.Services
{
    public class GameplayControllerFactory
    {
        private readonly RectTransform _playerCardRectTransform;
        private readonly MoveAnimation _moveAnimation;
        private readonly CardCreatedData _playerCard;
        private readonly LevelFactory _levelFactory;
        private readonly RectTransform _cardSlotParent;
        private readonly CardHandlerRepository _cardHandlerRepository;
        private readonly CardObjectPool _cardObjectPool;
        private readonly AudioSource _audioSource;
        private readonly AnimationsData _animationsData;
        private readonly SoundsData _soundsData;

        public GameplayControllerFactory(
            RectTransform playerCardRectTransform,
            MoveAnimation moveAnimation,
            CardCreatedData playerCard,
            LevelFactory levelFactory,
            RectTransform cardSlotParent,
            CardHandlerRepository cardHandlerRepository,
            CardObjectPool cardObjectPool,
            AudioSource audioSource,
            AnimationsData animationsData,
            SoundsData soundsData)
        {
            _playerCardRectTransform = playerCardRectTransform;
            _moveAnimation = moveAnimation;
            _playerCard = playerCard;
            _levelFactory = levelFactory;
            _cardSlotParent = cardSlotParent;
            _cardHandlerRepository = cardHandlerRepository;
            _cardObjectPool = cardObjectPool;
            _audioSource = audioSource;
            _animationsData = animationsData;
            _soundsData = soundsData;
        }

        public GameplayControllerCreateData Create()
        {
            GameplayControllerCreateData data = new();

            data.AttackController = new AttackController(
                _playerCardRectTransform,
                _moveAnimation,
                _cardHandlerRepository,
                _playerCard,
                _audioSource,
                _soundsData);
            data.GameplayModel = new GameplayModel(_playerCard);
            data.CardSlots = new CardSlots(
                _cardSlotParent,
                new MoveAnimation(_animationsData.CardAppearanceAnimationDuration),
                new AlphaAnimation(_animationsData.CardAppearanceAnimationDuration),
                _animationsData,
                _audioSource,
                _soundsData);
            data.GameplayController = new GameplayController(
                _levelFactory,
                data.CardSlots,
                data.GameplayModel,
                _cardHandlerRepository,
                _cardObjectPool);

            return data;
        }
    }
}