/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if UNITY_ANDROID

using System;
using System.Collections.Generic;

using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Android
{
    internal class AndroidInterstitialClient : IGoogleMobileAdsInterstitialClient
    {
        private AndroidJavaObject interstitial;

        public AndroidInterstitialClient(IAdListener listener)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            interstitial = new AndroidJavaObject(
                    Utils.InterstitialClassName, activity, new AdListener(listener));
        }

        #region IGoogleMobileAdsInterstitialClient implementation

        public void CreateInterstitialAd(string adUnitId) {
            interstitial.Call("create", adUnitId);
        }

        public void LoadAd(AdRequest request) {
            interstitial.Call("loadAd", Utils.GetAdRequestJavaObject(request));
        }

        public bool IsLoaded() {
            return interstitial.Call<bool>("isLoaded");
        }

        public void ShowInterstitial() {
            interstitial.Call("show");
        }

        public void DestroyInterstitial() {
            interstitial.Call("destroy");
        }

        public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string publicKey) {
            interstitial.Call("setPlayStorePurchaseParams", new InAppPurchaseListener(listener), publicKey);
        }

        #endregion
    }
}

#endif
