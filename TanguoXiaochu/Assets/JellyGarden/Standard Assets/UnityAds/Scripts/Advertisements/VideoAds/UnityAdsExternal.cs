/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if UNITY_ANDROID || UNITY_IOS

namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;
  using System.Runtime.InteropServices;

  internal static class UnityAdsExternal {

  private static UnityAdsPlatform impl;
  private static bool initialized = false;

  private static UnityAdsPlatform getImpl() {
    if (!initialized) {
      initialized = true;
#if UNITY_EDITOR
      impl = new UnityAdsEditor();
#elif UNITY_ANDROID
      impl = new UnityAdsAndroid();
#elif UNITY_IOS
      impl = new UnityAdsIos();
#else
      impl = null;
#endif
    }

    return impl;
  }

    public static void init (string gameId, bool testModeEnabled, string gameObjectName, string unityVersion) {
      getImpl().init(gameId, testModeEnabled, gameObjectName, unityVersion);
    }

    public static bool show (string zoneId, string rewardItemKey, string options) {
      return getImpl().show(zoneId, rewardItemKey, options);
    }

    public static void hide () {
      getImpl().hide();
    }

    public static bool isSupported () {
      return getImpl().isSupported();
    }

    public static string getSDKVersion () {
      return getImpl().getSDKVersion();
    }

    public static bool canShowZone (string zone) {
      return getImpl().canShowZone(zone);
    }

    public static bool hasMultipleRewardItems () {
      return getImpl().hasMultipleRewardItems();
    }

    public static string getRewardItemKeys () {
      return getImpl().getRewardItemKeys();
    }

    public static string getDefaultRewardItemKey () {
      return getImpl().getDefaultRewardItemKey();
    }

    public static string getCurrentRewardItemKey () {
      return getImpl().getCurrentRewardItemKey();
    }

    public static bool setRewardItemKey (string rewardItemKey) {
      return getImpl().setRewardItemKey(rewardItemKey);
    }

    public static void setDefaultRewardItemAsRewardItem () {
      getImpl().setDefaultRewardItemAsRewardItem();
    }

    public static string getRewardItemDetailsWithKey (string rewardItemKey) {
      return getImpl().getRewardItemDetailsWithKey(rewardItemKey);
    }

    public static string getRewardItemDetailsKeys () {
      return getImpl().getRewardItemDetailsKeys();
    }

    public static void setLogLevel(Advertisement.DebugLevel logLevel) {
      getImpl().setLogLevel(logLevel);
    }
  }
}

#endif
