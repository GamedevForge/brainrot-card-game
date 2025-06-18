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
        private readonly GridLayoutGroup _gridLayoutGroup;

        public CardSlots(
            RectTransform gridLayoutTransform,
            MoveAnimation moveAnimation,
            AlphaAnimation alphaAnimation)
        {
            _gridLayoutTransform = gridLayoutTransform;
            _moveAnimation = moveAnimation;
            _alphaAnimation = alphaAnimation;
            _gridLayoutGroup = _gridLayoutTransform.GetComponent<GridLayoutGroup>();
        }

        public async UniTask Add(GameObject cardGameObject)
        {
            RectTransform cardRectTransform = cardGameObject.GetComponent<RectTransform>();
            cardRectTransform.SetParent(_gridLayoutTransform);

            //_gridLayoutGroup.CalculateLayoutInputHorizontal();
            //_gridLayoutGroup.CalculateLayoutInputVertical();
            //_gridLayoutGroup.SetLayoutHorizontal();
            //_gridLayoutGroup.SetLayoutVertical();

            //await _moveAnimation.MoveAsync(
            //    cardRectTransform,
            //    new Vector3(
            //        cardRectTransform.position.x,
            //        cardRectTransform.position.y - 50f, 
            //        cardRectTransform.position.z),
            //    cardRectTransform.position);

            //cardRectTransform.SetParent(_gridLayoutTransform);
        }

        public async UniTask Remove(GameObject cardGameObject)
        {

        }
    }
}