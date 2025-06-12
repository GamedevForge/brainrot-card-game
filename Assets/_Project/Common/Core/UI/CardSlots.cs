using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI
{
    public class CardSlots
    {
        private readonly GridLayoutGroup _layoutGroup;

        public CardSlots(GridLayoutGroup layoutGroup)
        {
            _layoutGroup = layoutGroup;
        }

        public async UniTask Add(GameObject cardGameObject)
        {

        }

        public async UniTask Remove(GameObject cardGameObject)
        {

        }
    }
}