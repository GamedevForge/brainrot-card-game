using Cysharp.Threading.Tasks;
using Project.Core.UI.Animtions;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.UI
{
    public class CardSlots
    {
        private readonly RectTransform _gridLayoutTransform;
        private readonly MoveAnimation _moveAnimation;
        private readonly AlphaAnimation _alphaAnimation;

        public CardSlots(
            RectTransform gridLayoutTransform,
            MoveAnimation moveAnimation,
            AlphaAnimation alphaAnimation)
        {
            _gridLayoutTransform = gridLayoutTransform;
            _moveAnimation = moveAnimation;
            _alphaAnimation = alphaAnimation;
        }

        public async UniTask Add(GameObject cardGameObject)
        {
            RectTransform cardRectTransform = cardGameObject.GetComponent<RectTransform>();
            cardRectTransform.SetParent(_gridLayoutTransform);
            cardRectTransform.SetParent(null);

            await _moveAnimation.MoveAsync(
                cardRectTransform, 
                cardRectTransform.anchoredPosition3D - new Vector3(0f, 30f, 0f),
                cardRectTransform.anchoredPosition3D);

            cardRectTransform.SetParent(_gridLayoutTransform);
        }

        public async UniTask Remove(GameObject cardGameObject)
        {

        }
    }
}