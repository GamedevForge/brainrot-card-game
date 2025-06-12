using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Core.UI
{
    public class MoveAnimation
    {
        public async UniTask Move(
            RectTransform target, 
            Vector3 startPosition, 
            Vector3 endPosition,
            float duration)
        {
            Tween tween;
            
            target.anchoredPosition3D = startPosition;
            tween = target.DOAnchorPos3D(endPosition, duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}