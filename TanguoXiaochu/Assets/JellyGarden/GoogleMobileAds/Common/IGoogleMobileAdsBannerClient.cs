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
    internal interface IGoogleMobileAdsBannerClient {
        // Create a banner view and add it into the view hierarchy.
        void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

        // Request a new ad for the banner view.
        void LoadAd(AdRequest request);

        // Show the banner view on the screen.
        void ShowBannerView();

        // Hide the banner view from the screen.
        void HideBannerView();

        // Destroys a banner view and to free up memory.
        void DestroyBannerView();
    }
}
