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

  internal abstract class UnityAdsPlatform {
    public abstract void init(string gameId, bool testModeEnabled, string gameObjectName, string unityVersion);
    public abstract bool show(string zoneId, string rewardItemKey, string options);
    public abstract void hide();
    public abstract bool isSupported();
    public abstract string getSDKVersion();
    public abstract bool canShowZone(string zone);
    public abstract bool hasMultipleRewardItems();
    public abstract string getRewardItemKeys();
    public abstract string getDefaultRewardItemKey();
    public abstract string getCurrentRewardItemKey();
    public abstract bool setRewardItemKey(string rewardItemKey);
    public abstract void setDefaultRewardItemAsRewardItem();
    public abstract string getRewardItemDetailsWithKey(string rewardItemKey);
    public abstract string getRewardItemDetailsKeys();
    public abstract void setLogLevel(Advertisement.DebugLevel logLevel);
  }
}

#endif
