/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
    public class BannerView : IAdListener
    {
        private IGoogleMobileAdsBannerClient client;

        // These are the ad callback events that can be hooked into.
        public event EventHandler<EventArgs> AdLoaded = delegate {};
        public event EventHandler<AdFailedToLoadEventArgs> AdFailedToLoad = delegate {};
        public event EventHandler<EventArgs> AdOpened = delegate {};
        public event EventHandler<EventArgs> AdClosing = delegate {};
        public event EventHandler<EventArgs> AdClosed = delegate {};
        public event EventHandler<EventArgs> AdLeftApplication = delegate {};

        // Create a BannerView and add it into the view hierarchy.
        public BannerView(string adUnitId, AdSize adSize, AdPosition position)
        {
            client = GoogleMobileAdsClientFactory.GetGoogleMobileAdsBannerClient(this);
            client.CreateBannerView(adUnitId, adSize, position);
        }

        // Load an ad into the BannerView.
        public void LoadAd(AdRequest request)
        {
            client.LoadAd(request);
        }

        // Hide the BannerView from the screen.
        public void Hide()
        {
            client.HideBannerView();
        }

        // Show the BannerView on the screen.
        public void Show()
        {
            client.ShowBannerView();
        }

        // Destroy the BannerView.
        public void Destroy()
        {
            client.DestroyBannerView();
        }

        #region IAdListener implementation

        // The following methods are invoked from an IGoogleMobileAdsClient. Forward these calls
        // to the developer.
        void IAdListener.FireAdLoaded()
        {
            AdLoaded(this, EventArgs.Empty);
        }

        void IAdListener.FireAdFailedToLoad(string message)
        {
            AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs();
            args.Message = message;
            AdFailedToLoad(this, args);
        }

        void IAdListener.FireAdOpened()
        {
            AdOpened(this, EventArgs.Empty);
        }

        void IAdListener.FireAdClosing()
        {
            AdClosing(this, EventArgs.Empty);
        }

        void IAdListener.FireAdClosed()
        {
            AdClosed(this, EventArgs.Empty);
        }

        void IAdListener.FireAdLeftApplication()
        {
            AdLeftApplication(this, EventArgs.Empty);
        }

        #endregion
    }
}
