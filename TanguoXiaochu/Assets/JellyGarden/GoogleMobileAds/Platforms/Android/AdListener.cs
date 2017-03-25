/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if UNITY_ANDROID

using UnityEngine;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Android
{
    internal class AdListener : AndroidJavaProxy
    {
        private IAdListener listener;
        internal AdListener(IAdListener listener)
            : base(Utils.UnityAdListenerClassName)
        {
            this.listener = listener;
        }

        void onAdLoaded() {
            listener.FireAdLoaded();
        }

        void onAdFailedToLoad(string errorReason) {
            listener.FireAdFailedToLoad(errorReason);
        }

        void onAdOpened() {
            listener.FireAdOpened();
        }

        void onAdClosed() {
            listener.FireAdClosing();
            listener.FireAdClosed();
        }

        void onAdLeftApplication() {
            listener.FireAdLeftApplication();
        }
    }
}

#endif
