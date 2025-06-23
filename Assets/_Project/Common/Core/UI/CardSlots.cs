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

        public readonly GridLayoutGroup GridLayoutGroup;
        
        public CardSlots(
            RectTransform gridLayoutTransform,
            MoveAnimation moveAnimation,
            AlphaAnimation alphaAnimation)
        {
            _gridLayoutTransform = gridLayoutTransform;
            _moveAnimation = moveAnimation;
            _alphaAnimation = alphaAnimation;
            GridLayoutGroup = _gridLayoutTransform.GetComponent<GridLayoutGroup>();
        }

        public async UniTask Add(GameObject cardGameObject)
        {
            RectTransform cardRectTransform = cardGameObject.GetComponent<RectTransform>();
            cardRectTransform.SetParent(_gridLayoutTransform);

            GridLayoutGroup.CalculateLayoutInputHorizontal();
            GridLayoutGroup.CalculateLayoutInputVertical();
            GridLayoutGroup.SetLayoutHorizontal();
            GridLayoutGroup.SetLayoutVertical();

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