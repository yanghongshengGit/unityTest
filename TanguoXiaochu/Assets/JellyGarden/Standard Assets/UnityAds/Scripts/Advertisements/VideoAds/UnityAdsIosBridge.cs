/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if UNITY_IPHONE

namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;
  using System.Runtime.InteropServices;

	internal static class UnityAdsIosBridge {
		[DllImport ("__Internal")]
		public static extern void UnityAdsInit (string gameId, bool testModeEnabled, bool debugModeEnabled, string gameObjectName, string unityVersion);
		
		[DllImport ("__Internal")]
		public static extern bool UnityAdsShow (string zoneId, string rewardItemKey, string options);
		
		[DllImport ("__Internal")]
		public static extern void UnityAdsHide ();
		
		[DllImport ("__Internal")]
		public static extern bool UnityAdsIsSupported ();
		
		[DllImport ("__Internal")]
		public static extern string UnityAdsGetSDKVersion ();
		
		[DllImport ("__Internal")]
		public static extern bool UnityAdsCanShow ();

		[DllImport ("__Internal")]
		public static extern bool UnityAdsCanShowZone (string zone);

		[DllImport ("__Internal")]
		public static extern bool UnityAdsHasMultipleRewardItems ();
		
		[DllImport ("__Internal")]
		public static extern string UnityAdsGetRewardItemKeys ();
		
		[DllImport ("__Internal")]
		public static extern string UnityAdsGetDefaultRewardItemKey ();
		
		[DllImport ("__Internal")]
		public static extern string UnityAdsGetCurrentRewardItemKey ();
		
		[DllImport ("__Internal")]
		public static extern bool UnityAdsSetRewardItemKey (string rewardItemKey);
		
		[DllImport ("__Internal")]
		public static extern void UnityAdsSetDefaultRewardItemAsRewardItem ();
		
		[DllImport ("__Internal")]
		public static extern string UnityAdsGetRewardItemDetailsWithKey (string rewardItemKey);
		
		[DllImport ("__Internal")]
		public static extern string UnityAdsGetRewardItemDetailsKeys ();

		[DllImport ("__Internal")]
		public static extern void UnityAdsSetDebugMode(bool debugMode);

		[DllImport ("__Internal")]
		public static extern void UnityAdsEnableUnityDeveloperInternalTestMode ();
	}
}

#endif
