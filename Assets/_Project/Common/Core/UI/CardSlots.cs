using Cysharp.Threading.Tasks;
using Project.Configs;
using Project.Core.Services;
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
        private readonly AnimationsData _animationsData;
        private readonly AudioSource _audioSource;
        private readonly SoundsData _soundsData;

        public readonly GridLayoutGroup GridLayoutGroup;

        public CardSlots(
            RectTransform gridLayoutTransform,
            MoveAnimation moveAnimation,
            AlphaAnimation alphaAnimation,
            AnimationsData animationsData,
            AudioSource audioSource,
            SoundsData soundsData)
        {
            _gridLayoutTransform = gridLayoutTransform;
            _moveAnimation = moveAnimation;
            _alphaAnimation = alphaAnimation;
            GridLayoutGroup = _gridLayoutTransform.GetComponent<GridLayoutGroup>();
            _animationsData = animationsData;
            _audioSource = audioSource;
            _soundsData = soundsData;
        }

        public void Add(RectTransform cardRectTransform)
        {
            cardRectTransform.SetParent(_gridLayoutTransform);

            GridLayoutGroup.CalculateLayoutInputHorizontal();
            GridLayoutGroup.CalculateLayoutInputVertical();
            GridLayoutGroup.SetLayoutHorizontal();
            GridLayoutGroup.SetLayoutVertical();
        }

        public UniTask PlayShowAnimationAsync(CardCreatedData card) =>
            UniTask.WhenAll(
                _moveAnimation.MoveAsync(
                    card.CardRectTransform,
                    card.StartPosition - Vector3.up * _animationsData.CardAppearanceMoveOffset,
                    card.StartPosition),
                _alphaAnimation.PlayAnimationAsync(
                    card.CanvasGroup,
                    1f));

        public UniTask PlayHideAnimationAsync(CardCreatedData card) =>
            UniTask.WhenAll(
                _moveAnimation.MoveAsync(
                    card.CardRectTransform,
                    card.StartPosition,
                    card.StartPosition - Vector3.up * _animationsData.CardAppearanceMoveOffset),
                _alphaAnimation.PlayAnimationAsync(
                    card.CanvasGroup,
                    0f));

        public async UniTask PlayShowSoundSFXAsync()
        {
            _audioSource.PlayOneShot(_soundsData.OnShowSFX);
            await UniTask.WaitForSeconds(_soundsData.OnShowSFX.length);
        }
    }
}