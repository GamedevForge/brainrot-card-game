using Project.Core.Gameplay;
using Project.SaveLoadSystem;
using UnityEngine;

namespace Project.Core.Services
{
    public struct SaveLoadSystemCreateData
    {
        public GameObject SaveLoadSystemGameObject;
        public SaveLoadController SaveLoadController;
        public LevelProgress LevelProgress;
    }
}