/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
    internal class DummyClient : IGoogleMobileAdsBannerClient, IGoogleMobileAdsInterstitialClient
    {
        public DummyClient(IAdListener listener)
        {
            Debug.Log("Created DummyClient");
        }

        public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
        {
            Debug.Log("Dummy CreateBannerView");
        }

        public void LoadAd(AdRequest request)
        {
            Debug.Log("Dummy LoadAd");
        }

        public void ShowBannerView()
        {
            Debug.Log("Dummy ShowBannerView");
        }

        public void HideBannerView()
        {
            Debug.Log("Dummy HideBannerView");
        }

        public void DestroyBannerView()
        {
            Debug.Log("Dummy DestroyBannerView");
        }

        public void CreateInterstitialAd(string adUnitId) {
            Debug.Log("Dummy CreateIntersitialAd");
        }

        public bool IsLoaded() {
            Debug.Log("Dummy IsLoaded");
            return true;
        }

        public void ShowInterstitial() {
            Debug.Log("Dummy ShowInterstitial");
        }

        public void DestroyInterstitial() {
            Debug.Log("Dummy DestroyInterstitial");
        }

        public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey)
        {
            Debug.Log("Dummy SetInAppPurchaseParams");
        }
    }
}
