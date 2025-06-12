using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Ai;
using Project.Configs;
using Project.Core.Sevices;
using Project.Core.UI;
using UnityEngine;

namespace Project.Core.Gameplay
{
    public class GameplayController
    {
        private readonly LevelFactory _levelFactory;
        private readonly CardSlots _cardSlots;
        private readonly AiActor _aiActor;
        
        private LevelData _currentLevelData;
        private List<WaveModel> _levelModel;
        private WaveModel _currentWave;

        public void SetCurrentLevel(LevelData levelData)
        { 
            _currentLevelData = levelData; 
        }

        public async UniTask StartLevel()
        {
            CreateLevel();
            _currentWave = _levelModel[0];
            DeactivateAllCardsWithoutCurrent();

            foreach (CardCreatedData cardCreatedData in _currentWave.CardCreatedDatas)
                await _cardSlots.Add(cardCreatedData.CardGameObject);
        }

        private void CreateLevel() =>
            _levelModel = _levelFactory.Create(_currentLevelData);

        private void DeactivateAllCardsWithoutCurrent()
        {
            foreach (WaveModel waveModel in _levelModel)
            {
                if (waveModel == _currentWave)
                    continue;
                else
                {
                    foreach(CardCreatedData cardCreatedData in waveModel.CardCreatedDatas)
                        cardCreatedData.CardGameObject.SetActive(false);
                }
            }
        }
    }
}