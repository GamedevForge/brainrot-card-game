using Project.Core.Gameplay;
using Project.Core.UI;
using Project.Core.UI.Animtions;
using UnityEngine;

namespace Project.Core.Sevices
{
    public class GameplayControllerFactory
    {
        private readonly RectTransform _playerCardRectTransform;
        private readonly MoveAnimation _moveAnimation;
        private readonly CardCreatedData _playerCard;
        private readonly LevelFactory _levelFactory;
        private readonly RectTransform _cardSlotParent;
        private readonly CardHandlerRepository _cardHandlerRepository;

        public GameplayControllerFactory(
            RectTransform playerCardRectTransform,
            MoveAnimation moveAnimation,
            CardCreatedData playerCard,
            LevelFactory levelFactory,
            RectTransform cardSlotParent,
            CardHandlerRepository cardHandlerRepository)
        {
            _playerCardRectTransform = playerCardRectTransform;
            _moveAnimation = moveAnimation;
            _playerCard = playerCard;
            _levelFactory = levelFactory;
            _cardSlotParent = cardSlotParent;
            _cardHandlerRepository = cardHandlerRepository;
        }

        public GameplayControllerCreateData Create()
        {
            GameplayControllerCreateData data = new();

            data.AttackController = new AttackController(
                _playerCardRectTransform,
                _moveAnimation,
                _cardHandlerRepository,
                _playerCard);
            data.GameplayModel = new GameplayModel();
            data.CardSlots = new CardSlots(_cardSlotParent);
            data.GameplayController = new GameplayController(
                _levelFactory,
                data.CardSlots,
                data.GameplayModel,
                _cardHandlerRepository);

            return data;
        }
    }
}