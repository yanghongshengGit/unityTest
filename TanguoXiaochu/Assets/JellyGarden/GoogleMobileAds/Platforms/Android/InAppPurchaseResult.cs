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
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Android
{
    internal class InAppPurchaseResult : IInAppPurchaseResult
    {
        private UnityEngine.AndroidJavaObject result;
        public InAppPurchaseResult(AndroidJavaObject result)
        {
            this.result = result;
        }

        public void FinishPurchase() {
            result.Call("finishPurchase");
        }

        public string ProductId {
            get { return result.Call<string>("getProductId"); }
        }

        public bool IsSuccessful {
            get {
                AndroidJavaObject pluginUtils = new AndroidJavaObject(Utils.PluginUtilsClassName);
                return pluginUtils.CallStatic<bool>("isResultSuccess", result);
            }
        }

        public bool IsVerified {
            get { return result.Call<bool>("isVerified"); }
        }
    }
}

#endif
