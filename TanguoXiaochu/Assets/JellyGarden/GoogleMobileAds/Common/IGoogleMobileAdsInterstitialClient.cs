/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common {
    internal interface IGoogleMobileAdsInterstitialClient {
        // Creates an InterstitialAd.
        void CreateInterstitialAd(string adUnitId);

        // Loads a new interstitial request.
        void LoadAd(AdRequest request);

        // Determines whether the interstitial has loaded.
        bool IsLoaded();

        // Shows the InterstitialAd.
        void ShowInterstitial();

        // Destroys an InterstitialAd to free up memory.
        void DestroyInterstitial();

        // Sets in-app purchase params to use in-app purchase house ads.
        void SetInAppPurchaseParams(IInAppPurchaseListener listener, string androidPublicKey);
    }
}
