using UnityEngine;

namespace Project.Configs
{
    public abstract class BaseCardData : ScriptableObject
    {
        [field: SerializeField] public Sprite CardSprite { get; private set; }
    }
}