using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.UI;

namespace Project.Core.Gameplay
{
    public class GameplayController : IDisposable
    {
        private readonly LevelFactory _levelFactory;
        private readonly CardSlots _cardSlots;
        private readonly GameplayModel _gameplayModel;
        private readonly CardHandlerRepository _cardHandlerRepository;

        private readonly List<CardCreatedData> _subscribedCards = new(); 

        private int _currentWaveIndex = 0;

        public GameplayController(
            LevelFactory levelFactory, 
            CardSlots cardSlots, 
            GameplayModel gameplayModel, 
            CardHandlerRepository cardHandlerRepository)
        {
            _levelFactory = levelFactory;
            _cardSlots = cardSlots;
            _gameplayModel = gameplayModel;
            _cardHandlerRepository = cardHandlerRepository;
        }

        public void Dispose()
        {
            foreach (var card in _subscribedCards) 
                card.Health.OnDead -= RemoveCardOnDead;
        }

        public void SetCurrentLevel(LevelData levelData)
        {
            _gameplayModel.CurrentLevelData = levelData; 
        }

        public void StartLevel()
        {
            _currentWaveIndex = 0;
            CreateLevel();
            SubscribeOnAllDaedEvent();
            _gameplayModel.CurrentWave = _gameplayModel.LevelModel[_currentWaveIndex];
            DeactivateAllCardsWithoutCurrent();
        }

        public void GoToNextWave()
        {
            _currentWaveIndex++;

            if (_currentWaveIndex >= _gameplayModel.LevelModel.Count)
                return;

            _gameplayModel.CurrentWave = _gameplayModel.LevelModel[_currentWaveIndex];
        }

        public async UniTask AddOnSlotAllCardFromCurrentWave()
        {
            foreach (CardCreatedData cardCreatedData in _gameplayModel.CurrentWave.CardCreatedDatas)
                await _cardSlots.Add(cardCreatedData.CardGameObject);
        }

        private async void RemoveCardOnDead(CardCreatedData card)
        {
            _cardHandlerRepository.Remove(card);
            await _cardSlots.Remove(card.CardGameObject);
            card.Health.OnDead -= RemoveCardOnDead;
            _subscribedCards.Remove(card);
        }

        private void CreateLevel() =>
            _gameplayModel.LevelModel = _levelFactory.Create(_gameplayModel.CurrentLevelData);

        private void DeactivateAllCardsWithoutCurrent()
        {
            foreach (WaveModel waveModel in _gameplayModel.LevelModel)
            {
                if (waveModel == _gameplayModel.CurrentWave)
                    continue;
                else
                {
                    foreach(CardCreatedData cardCreatedData in waveModel.CardCreatedDatas)
                        cardCreatedData.CardGameObject.SetActive(false);
                }
            }
        }

        private void SubscribeOnAllDaedEvent()
        {
            foreach (WaveModel waveModel in _gameplayModel.LevelModel)
            {
                foreach (CardCreatedData cardCreatedData in waveModel.CardCreatedDatas)
                {
                    cardCreatedData.Health.OnDead += RemoveCardOnDead;
                    _subscribedCards.Add(cardCreatedData);
                }
            }
        }
    }
}