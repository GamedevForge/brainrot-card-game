using Cysharp.Threading.Tasks;
using GamePush;

namespace Project.Core.Services
{
    public class InterstitialController
    {
        public async UniTask ShowInterstitial()
        {
            GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
            await UniTask.WaitWhile(() => GP_Ads.IsFullscreenPlaying());
        }

        private void OnFullscreenStart() =>
            GP_Analytics.Hit("start_interstitial_ads");

        private void OnFullscreenClose(bool success) =>
            GP_Analytics.Hit("complete_interstitial_ads");
    }
}