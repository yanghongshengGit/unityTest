/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
    internal class GoogleMobileAdsClientFactory
    {
        internal static IGoogleMobileAdsBannerClient GetGoogleMobileAdsBannerClient(
                IAdListener listener)
        {
            #if UNITY_EDITOR
                // Testing UNITY_EDITOR first because the editor also responds to the currently
                // selected platform.
                return new GoogleMobileAds.Common.DummyClient(listener);
            #elif UNITY_ANDROID
                return new GoogleMobileAds.Android.AndroidBannerClient(listener);
            #elif UNITY_IPHONE
                return new GoogleMobileAds.iOS.IOSBannerClient(listener);
            #else
                return new GoogleMobileAds.Common.DummyClient(listener);
            #endif
        }

        internal static IGoogleMobileAdsInterstitialClient GetGoogleMobileAdsInterstitialClient(
                IAdListener listener)
        {
            #if UNITY_EDITOR
                // Testing UNITY_EDITOR first because the editor also responds to the currently
                // selected platform.
                return new GoogleMobileAds.Common.DummyClient(listener);
            #elif UNITY_ANDROID
                return new GoogleMobileAds.Android.AndroidInterstitialClient(listener);
            #elif UNITY_IPHONE
                return new GoogleMobileAds.iOS.IOSInterstitialClient(listener);
            #else
                return new GoogleMobileAds.Common.DummyClient(listener);
            #endif
        }
    }
}
