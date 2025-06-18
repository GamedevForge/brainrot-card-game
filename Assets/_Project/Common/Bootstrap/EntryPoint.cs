using Project.Ai;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.Sevices.StateMachine;
using Project.Core.UI.Animtions;
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
        [SerializeField] private RectTransform _playerParent;

        [Header("UI:")]
        [SerializeField] private GameObject _menuWindowPrefab;
        [SerializeField] private GameObject _winWindowPrefab;
        [SerializeField] private GameObject _loseWindowPrefab;
        [SerializeField] private RectTransform _windowsParent;
        [SerializeField] private GameObject _shadowPopupPrefab;
        [SerializeField] private RectTransform _shadowPopupGameObjectParent;
        [SerializeField] private GameObject _upgradeUIElementPrefab;
        [SerializeField] private RectTransform _upgradeUIElementsParent;
        [SerializeField] private RectTransform _leftUIElementStartPositionRectTransform;
        [SerializeField] private RectTransform _leftUIElementEndPositionRectTransform;
        [SerializeField] private RectTransform _rightUIElementStartPositionRectTransform;
        [SerializeField] private RectTransform _rightUIElementEndPositionRectTransform;

        [Header("Other:")]
        [SerializeField] private GameObject _gameplayBackgroundGameObject;
        [SerializeField] private GameObject _saveLoadSystemPrefab;

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
        private ShadowPopupFactory _shadowPopupFactory;
        private ShadowPopupCraeteData _shadowPopupCraeteData;
        private SaveLoadSystemFactory _saveLoadSystemFactory;
        private SaveLoadSystemCreateData _saveLoadSystemCreateData;
        private PlayerCardFactory _playerCardFactory;
        private CardObjectPool _cardObjectPool;

        private void Start() 
        {
            _saveLoadSystemFactory = new SaveLoadSystemFactory(
                _saveLoadSystemPrefab,
                _levelsData);
            _saveLoadSystemCreateData = _saveLoadSystemFactory.Create();
            _uIFactory = new UIFactory(
                _menuWindowPrefab, 
                _loseWindowPrefab, 
                _winWindowPrefab, 
                _animationsData.WindowAnimationDuration,
                _windowsParent);
            _shadowPopupFactory = new ShadowPopupFactory(
                _shadowPopupPrefab,
                _shadowPopupGameObjectParent,
                _animationsData.PopupShowAndHideDuration);
            _shadowPopupCraeteData = _shadowPopupFactory.Create();
            _uICreateData = _uIFactory.Create();
            _inputController = new InputController(_graphicRaycaster);
            _cardSlotsGameObject = GameObject.Instantiate(_cardSlotsPrefab, _cardSlotsParent);
            _cardFactory = new CardFactory(_cardPrefab);
            _cardObjectPool = new CardObjectPool(_cardFactory, _gameData.MaxCardPoolSize);
            _levelFactory = new LevelFactory(
                new WaveFactory(
                    _cardObjectPool,
                    _cardHandlerRepository));
            _playerCardFactory = new PlayerCardFactory(
                _cardFactory,
                _playerParent,
                _playerData.DefualtCardData);
            _playerCard = _playerCardFactory.Create();
            _upgradeControllerFactory = new UpgradeControllerFactory(
                _playerCard,
                _gameData.MaxUpgradeUIElementsPoolSize,
                _upgradeUIElementPrefab,
                _upgradeUIElementsParent,
                _animationsData.UpgradeUIElementsShowAndHideDuration,
                _leftUIElementStartPositionRectTransform.position,
                _leftUIElementEndPositionRectTransform.position,
                _rightUIElementStartPositionRectTransform.position,
                _rightUIElementEndPositionRectTransform.position);
            _upgradeControllerCreateData = _upgradeControllerFactory.Create();
            _aiActor = new AiActor();
            _gameplayControllerFactory = new GameplayControllerFactory(
                _playerCard.CardGameObject.GetComponent<RectTransform>(),
                new MoveAnimation(_animationsData.CardMoveDuration),
                _playerCard,
                _levelFactory,
                _cardSlotsGameObject.GetComponent<RectTransform>(),
                _cardHandlerRepository,
                _cardObjectPool);
            _gameplayControllerCreateData = _gameplayControllerFactory.Create();
            _gameCycleStateControllerFactory = new GameCycleStateControllerFactory(
                _uICreateData.MenuWindowController,
                _uICreateData.WinWindowController,
                _uICreateData.LoseWindowController,
                _shadowPopupCraeteData.ShadowPopup,
                _inputController,
                _gameplayBackgroundGameObject,
                _levelsData,
                _saveLoadSystemCreateData.LevelProgress,
                _gameplayControllerCreateData.GameplayController,
                _playerCard);
            _gameCycleStateController = _gameCycleStateControllerFactory.Create();
            _gameplayStateControllerFactory = new GameplayStateControllerFactory(
                _gameplayControllerCreateData.AttackController,
                _inputController,
                _gameplayControllerCreateData.GameplayModel,
                _cardHandlerRepository,
                _aiActor,
                _upgradeControllerCreateData.UpgradeController,
                _upgradeControllerCreateData.UpgradeControllerView,
                _gameplayControllerCreateData.GameplayController,
                _gameCycleStateController,
                _saveLoadSystemCreateData.LevelProgress);
            _gameplayStateController = _gameplayStateControllerFactory.Create();
            _gameCycleStateControllerFactory.SetGameplayStateController(_gameplayStateController);

            _uICreateData.MenuWindowController.Initialize();
            _uICreateData.WinWindowController.Initialize();
            _uICreateData.LoseWindowController.Initialize();
            _uIFactory.Initialize();
            _gameCycleStateController.Initialize();
            _upgradeControllerCreateData.UpgradeControllerView.Initialize();
        }

        private void OnDestroy()
        {
            _uICreateData.MenuWindowController.Dispose();
            _uICreateData.WinWindowController.Dispose();
            _uICreateData.LoseWindowController.Dispose();
            _cardHandlerRepository.Dispose();
            _playerCardFactory.Dispose();
            _upgradeControllerCreateData.UpgradeControllerView.Dispose();
        }
    }
}