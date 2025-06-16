using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Project.Core.UI.Animtions
{
    public class AlphaAnimation
    {
        private readonly float _duration;

        public AlphaAnimation(float duration) =>
            _duration = duration;

        public async UniTask PlayAnimationAsync(CanvasGroup target, float endValue) =>
            await target
            .DOFade(endValue, _duration)
            .AsyncWaitForCompletion();
    }
}