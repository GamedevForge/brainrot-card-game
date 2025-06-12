using System.Collections.Generic;
using Project.Configs;
using Project.Core.Gameplay;

namespace Project.Core.Sevices
{
    public class LevelFactory
    {
        public readonly WaveFactory _waveFactory;

        public LevelFactory(WaveFactory waveFactory) =>
            _waveFactory = waveFactory;

        public List<WaveModel> Create(LevelData levelData)
        {
            List<WaveModel> waveModels = new();

            foreach (WaveConfig waveConfig in levelData.WaveConfigs)
                waveModels.Add(new WaveModel { CardCreatedDatas = _waveFactory.Create(waveConfig)});

            return waveModels;
        }
    }
}