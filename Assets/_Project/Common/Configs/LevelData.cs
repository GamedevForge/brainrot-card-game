using System;
using UnityEngine;

namespace Project.Configs
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Project/LevelData")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public WaveConfig[] WaveConfigs {  get; private set; }
    }

    [Serializable]
    public class WaveConfig
    {
        public CardData[] CardDatas;
        public UpgradeRangeConfig UpgradeRangeConfig;
    }

    [Serializable]
    public class UpgradeRangeConfig
    {
        public UpgradeValueConfig From;
        public UpgradeValueConfig To;
    }

    [Serializable]
    public class UpgradeValueConfig
    {
        public int Value;
        public UpgradeValueType Type;
    }

    public enum UpgradeValueType
    {
        Addition,
        Multiplication
    }
}