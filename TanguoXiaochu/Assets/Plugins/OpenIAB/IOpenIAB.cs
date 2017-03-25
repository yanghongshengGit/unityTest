/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace OnePF
{
    /**
     * Implement this to create billing service for new platform
     */ 
    public interface IOpenIAB
    {
        void init(Options options);
        void mapSku(string sku, string storeName, string storeSku);
        void unbindService();
        bool areSubscriptionsSupported();
        void queryInventory();
        void queryInventory(string[] inAppSkus);
        void purchaseProduct(string sku, string developerPayload = "");
        void purchaseSubscription(string sku, string developerPayload = "");
        void consumeProduct(Purchase purchase);
        void restoreTransactions();

        bool isDebugLog();
        void enableDebugLogging(bool enabled);
        void enableDebugLogging(bool enabled, string tag);
    }
}
