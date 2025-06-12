using Project.Configs;
using Project.Core.Sevices;
using UnityEngine;

namespace Project.Bootstrap
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Datas:")]
        [SerializeField] private LevelsData _levelsData;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GameData _gameData;

        [Header("Cards:")]
        [SerializeField] private GameObject _cardPrefab;

        private CardObjectPool _cardObjectPool;

        private void Start() 
        {
            _cardObjectPool = new CardObjectPool(
                new CardFactory(_cardPrefab), 
                _gameData.MaxCardPoolSize);
        }
    }
}