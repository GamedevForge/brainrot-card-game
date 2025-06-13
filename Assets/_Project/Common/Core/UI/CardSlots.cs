using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI
{
    public class CardSlots
    {
        private readonly RectTransform _gridLayoutTransform;

        public CardSlots(RectTransform gridLayoutTransform)
        {
            _gridLayoutTransform = gridLayoutTransform;
        }

        public async UniTask Add(GameObject cardGameObject)
        {

        }

        public async UniTask Remove(GameObject cardGameObject)
        {

        }
    }
}