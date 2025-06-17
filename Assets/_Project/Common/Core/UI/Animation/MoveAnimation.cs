using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Core.UI.Animtions
{
    public class MoveAnimation
    {
        private readonly float _duration;

        public MoveAnimation(float duration) =>
            _duration = duration;

        public async UniTask MoveAsync(
            RectTransform target, 
            Vector3 startPosition, 
            Vector3 endPosition)
        {
            Tween tween;
            
            target.position = startPosition;
            tween = target.DOMove(endPosition, _duration);
            await tween.AsyncWaitForCompletion();
        }
    }
}