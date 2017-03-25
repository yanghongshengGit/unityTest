/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;

namespace OnePF
{
    /**
     * All options of OpenIAB can be found here
     */
    public class Options
    {
        /**
         * Default timeout (in milliseconds) for discover all OpenStores on device.
         */
        public const int DISCOVER_TIMEOUT_MS = 5000;

        /** 
         * For generic stores it takes 1.5 - 3sec
         * SamsungApps initialization is very time consuming (from 4 to 12 seconds). 
         */
        public const int INVENTORY_CHECK_TIMEOUT_MS = 10000;

        /**
         * Wait specified amount of ms to find all OpenStores on device
         */
        public int discoveryTimeoutMs = DISCOVER_TIMEOUT_MS;

        /** 
         * Check user inventory in every store to select proper store
         * <p>
         * Will try to connect to each billingService and extract user's purchases.
         * If purchases have been found in the only store that store will be used for further purchases. 
         * If purchases have been found in multiple stores only such stores will be used for further elections    
         */
        public bool checkInventory = true;

        /**
         * Wait specified amount of ms to check inventory in all stores
         */
        public int checkInventoryTimeoutMs = INVENTORY_CHECK_TIMEOUT_MS;

        /** 
         * OpenIAB could skip receipt verification by publicKey for GooglePlay and OpenStores 
         * <p>
         * Receipt could be verified in {@link OnIabPurchaseFinishedListener#onIabPurchaseFinished()}
         * using {@link Purchase#getOriginalJson()} and {@link Purchase#getSignature()}
         */
        public OptionsVerifyMode verifyMode = OptionsVerifyMode.VERIFY_EVERYTHING;

        public SearchStrategy storeSearchStrategy = SearchStrategy.INSTALLER;

        /** 
         * storeKeys is map of [ appstore name -> publicKeyBase64 ] 
         * Put keys for all stores you support in this Map and pass it to instantiate {@link OpenIabHelper} 
         * <p>
         * <b>publicKey</b> key is used to verify receipt is created by genuine Appstore using 
         * provided signature. It can be found in Developer Console of particular store
         * <p>
         * <b>name</b> of particular store can be provided by local_store tool if you run it on device.
         * For Google Play OpenIAB uses {@link OpenIabHelper#NAME_GOOGLE}.
         * <p>
         * <p>Note:
         * AmazonApps and SamsungApps doesn't use RSA keys for receipt verification, so you don't need 
         * to specify it
         */
        public Dictionary<string, string> storeKeys = new Dictionary<string, string>();

        /**
         * Used as priority list if store that installed app is not found and there are 
         * multiple stores installed on device that supports billing.
         */
        public string[] prefferedStoreNames = new string[] { };

        public string[] availableStoreNames = new string[] { };

        public int samsungCertificationRequestCode;
    }
}
