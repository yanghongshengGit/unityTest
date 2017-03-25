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
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using DebugLevel = Advertisement.DebugLevel;

  internal static class Utils {
    private static void Log(DebugLevel debugLevel, string message) {
      if((Advertisement.debugLevel & debugLevel) != DebugLevel.None) {
        Debug.Log(message);
      }
    }

    public static void LogDebug(string message) {
      Log (DebugLevel.Debug,"Debug: " + message);
    }
    
    public static void LogInfo(string message) {
      Log (DebugLevel.Info, "Info:" + message);
    }
    
    public static void LogWarning(string message) {
      Log (DebugLevel.Warning,"Warning:" + message);
    }
    
    public static void LogError(string message) {
      Log (DebugLevel.Error, "Error: " + message);
    }
  }
}

#endif
