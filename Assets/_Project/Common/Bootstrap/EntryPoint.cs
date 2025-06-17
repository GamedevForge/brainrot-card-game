using Project.Ai;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Animtions;
using Project.Core.UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Datas:")]
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GameData _gameData;
        [SerializeField] private AnimationsData _animationsData;

        [Header("Cards:")]
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private GameObject _cardSlotsPrefab;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private RectTransform _cardSlotsParent;

        [Header("UI:")]
        [SerializeField] private GameObject _menuWindowPrefab;
        [SerializeField] private GameObject _winWindowPrefab;
        [SerializeField] private GameObject _loseWindowPrefab;
        [SerializeField] private RectTransform _windowsParent;

        [Header("Other:")]
        [SerializeField] private GameObject _gameplayBackgroundGameObject;

        private readonly CardHandlerRepository _cardHandlerRepository = new();

        private BaseStateController _gameCycleStateController;
        private BaseStateController _gameplayStateController;
        private GameCycleStateControllerFactory _gameCycleStateControllerFactory;
        private GameplayStateControllerFactory _gameplayStateControllerFactory;
        private GameplayControllerFactory _gameplayControllerFactory;
        private GameplayControllerCreateData _gameplayControllerCreateData;
        private CardCreatedData _playerCard;
        private CardFactory _cardFactory;
        private LevelFactory _levelFactory;
        private GameObject _cardSlotsGameObject;
        private InputController _inputController;
        private AiActor _aiActor;
        private UpgradeControllerFactory _upgradeControllerFactory;
        private UpgradeControllerCreateData _upgradeControllerCreateData;
        private UIFactory _uIFactory;
        private UICreateData _uICreateData;

        private void Start() 
        {
            _uIFactory = new UIFactory(
                _menuWindowPrefab, 
                _loseWindowPrefab, 
                _winWindowPrefab, 
                _animationsData.WindowAnimationDuration,
                _windowsParent);
            _uICreateData = _uIFactory.Create();
            _gameCycleStateController = new BaseStateController();
            _gameplayStateController = new BaseStateController();
            _inputController = new InputController(_graphicRaycaster);
            _cardSlotsGameObject = GameObject.Instantiate(_cardSlotsPrefab, _cardSlotsParent);
            _cardFactory = new CardFactory(_cardPrefab);
            _levelFactory = new LevelFactory(
                new WaveFactory(
                    new CardObjectPool(_cardFactory, _gameData.MaxCardPoolSize),
                    _cardHandlerRepository));
            _playerCard = _cardFactory.Create();
            _upgradeControllerFactory = new UpgradeControllerFactory(_playerCard.CardStats);
            _upgradeControllerCreateData = _upgradeControllerFactory.Create();
            _aiActor = new AiActor();
            _gameplayControllerFactory = new GameplayControllerFactory(
                _playerCard.CardGameObject.GetComponent<RectTransform>(),
                new MoveAnimation(_animationsData.CardMoveDuration),
                _playerCard,
                _levelFactory,
                _cardSlotsGameObject.GetComponent<RectTransform>(),
                _cardHandlerRepository);
            _gameplayControllerCreateData = _gameplayControllerFactory.Create();
            _gameplayStateControllerFactory = new GameplayStateControllerFactory(
                _gameplayControllerCreateData.AttackController,
                _inputController,
                _gameplayControllerCreateData.GameplayModel,
                _cardHandlerRepository,
                _aiActor,
                _upgradeControllerCreateData.UpgradeController,
                _upgradeControllerCreateData.UpgradeControllerView,
                _gameplayControllerCreateData.GameplayController,
                _gameCycleStateController);
            _gameCycleStateController = _gameCycleStateControllerFactory.Create();
            _gameCycleStateControllerFactory = new GameCycleStateControllerFactory(
                _uICreateData.MenuWindowController,
                _uICreateData.WinWindowController,
                _uICreateData.LoseWindowController,
                new ShadowPopup(),
                _inputController,
                _gameplayBackgroundGameObject,
                _gameplayStateController);
            _gameplayStateController = _gameplayStateControllerFactory.Create();
        }
    }
}