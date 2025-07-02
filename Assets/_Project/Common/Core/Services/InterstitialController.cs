using Cysharp.Threading.Tasks;
using GamePush;
using UnityEngine;

namespace Project.Core.Services
{
    public class InterstitialController
    {
        private bool _interstitialIsShowing = false;

        public async UniTask ShowInterstitial()
        {
            Debug.Log(GP_Ads.IsFullscreenAvailable());
            GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
            _interstitialIsShowing = true;
            await UniTask.WaitWhile(() => _interstitialIsShowing == true);
        }

        private void OnFullscreenStart() =>
            GP_Analytics.Hit("start_interstitial_ads");

        private void OnFullscreenClose(bool success)
        {
            _interstitialIsShowing = false;
            GP_Analytics.Hit("complete_interstitial_ads");
        }
    }
}