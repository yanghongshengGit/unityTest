/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.iOS
{
    internal class IOSInterstitialClient : IGoogleMobileAdsInterstitialClient
    {
        #region Interstitial callback types

        internal delegate void GADUInterstitialDidReceiveAdCallback(IntPtr interstitialClient);
        internal delegate void GADUInterstitialDidFailToReceiveAdWithErrorCallback(
                IntPtr interstitialClient, string error);
        internal delegate void GADUInterstitialWillPresentScreenCallback(IntPtr interstitialClient);
        internal delegate void GADUInterstitialWillDismissScreenCallback(IntPtr interstitialClient);
        internal delegate void GADUInterstitialDidDismissScreenCallback(IntPtr interstitialClient);
        internal delegate void GADUInterstitialWillLeaveApplicationCallback(
                IntPtr interstitialClient);

        #endregion

        private IAdListener listener;
        private IntPtr interstitialPtr;
        private static Dictionary<IntPtr, IOSBannerClient> bannerClients;

        // This property should be used when setting the interstitialPtr.
        private IntPtr InterstitialPtr
        {
            get
            {
                return interstitialPtr;
            }
            set
            {
                Externs.GADURelease(interstitialPtr);
                interstitialPtr = value;
            }
        }

        public IOSInterstitialClient(IAdListener listener)
        {
            this.listener = listener;
        }

        #region IGoogleMobileAdsInterstitialClient implementation

        public void CreateInterstitialAd(string adUnitId) {
            IntPtr interstitialClientPtr = (IntPtr) GCHandle.Alloc(this);
            InterstitialPtr = Externs.GADUCreateInterstitial(interstitialClientPtr, adUnitId);
            Externs.GADUSetInterstitialCallbacks(
                    InterstitialPtr,
                    InterstitialDidReceiveAdCallback,
                    InterstitialDidFailToReceiveAdWithErrorCallback,
                    InterstitialWillPresentScreenCallback,
                    InterstitialWillDismissScreenCallback,
                    InterstitialDidDismissScreenCallback,
                    InterstitialWillLeaveApplicationCallback);
        }

        public void LoadAd(AdRequest request) {
            IntPtr requestPtr = Externs.GADUCreateRequest();
            foreach (string keyword in request.Keywords)
            {
                Externs.GADUAddKeyword(requestPtr, keyword);
            }
            foreach (string deviceId in request.TestDevices)
            {
                Externs.GADUAddTestDevice(requestPtr, deviceId);
            }
            if (request.Birthday.HasValue)
            {
                DateTime birthday = request.Birthday.GetValueOrDefault();
                Externs.GADUSetBirthday(requestPtr, birthday.Year, birthday.Month, birthday.Day);
            }
            if (request.Gender.HasValue)
            {
                Externs.GADUSetGender(requestPtr, (int)request.Gender.GetValueOrDefault());
            }
            if (request.TagForChildDirectedTreatment.HasValue) {
                Externs.GADUTagForChildDirectedTreatment(
                        requestPtr, request.TagForChildDirectedTreatment.GetValueOrDefault());
            }
            foreach (KeyValuePair<string, string> entry in request.Extras)
            {
                Externs.GADUSetExtra(requestPtr, entry.Key, entry.Value);
            }
            // Indicate that the request is coming from this Unity plugin.
            Externs.GADUSetExtra(requestPtr, "unity", "1");
            Externs.GADURequestInterstitial(InterstitialPtr, requestPtr);
            Externs.GADURelease(requestPtr);
        }

        public bool IsLoaded() {
            return Externs.GADUInterstitialReady(InterstitialPtr);
        }

        public void ShowInterstitial() {
            Externs.GADUShowInterstitial(InterstitialPtr);
        }

        public void DestroyInterstitial() {
            InterstitialPtr = IntPtr.Zero;
        }

        #endregion

        #region Banner callback methods

        [MonoPInvokeCallback(typeof(GADUInterstitialDidReceiveAdCallback))]
        private static void InterstitialDidReceiveAdCallback(IntPtr interstitialClient)
        {
            IntPtrToInterstitialClient(interstitialClient).listener.FireAdLoaded();
        }

        [MonoPInvokeCallback(typeof(GADUInterstitialDidFailToReceiveAdWithErrorCallback))]
        private static void InterstitialDidFailToReceiveAdWithErrorCallback(
                IntPtr interstitialClient, string error)
        {
            IntPtrToInterstitialClient(interstitialClient).listener.FireAdFailedToLoad(error);
        }

        [MonoPInvokeCallback(typeof(GADUInterstitialWillPresentScreenCallback))]
        private static void InterstitialWillPresentScreenCallback(IntPtr interstitialClient)
        {
            IntPtrToInterstitialClient(interstitialClient).listener.FireAdOpened();
        }

        [MonoPInvokeCallback(typeof(GADUInterstitialWillDismissScreenCallback))]
        private static void InterstitialWillDismissScreenCallback(IntPtr interstitialClient)
        {
            IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosing();
        }

        [MonoPInvokeCallback(typeof(GADUInterstitialDidDismissScreenCallback))]
        private static void InterstitialDidDismissScreenCallback(IntPtr interstitialClient)
        {
            IntPtrToInterstitialClient(interstitialClient).listener.FireAdClosed();
        }

        [MonoPInvokeCallback(typeof(GADUInterstitialWillLeaveApplicationCallback))]
        private static void InterstitialWillLeaveApplicationCallback(IntPtr interstitialClient)
        {
            IntPtrToInterstitialClient(interstitialClient).listener.FireAdLeftApplication();
        }

        private static IOSInterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
        {
            GCHandle handle = (GCHandle) interstitialClient;
            return handle.Target as IOSInterstitialClient;
        }

        public void SetInAppPurchaseParams(IInAppPurchaseListener listener, string publicKey)
        {
            // iOS currently does not support in-app purchase ads.
        }

        #endregion
    }
}
