/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

namespace OnePF
{
    /**
     * Represents an in-app billing purchase.
     */
    public class Purchase
    {
        /// <summary>
        /// ITEM_TYPE_INAPP or ITEM_TYPE_SUBS
        /// </summary>
        public string ItemType { get; private set; }
        /// <summary>
        /// A unique order identifier for the transaction. This corresponds to the Google Wallet Order ID.
        /// </summary>
        public string OrderId { get; private set; }
        /// <summary>
        /// The application package from which the purchase originated.
        /// </summary>
        public string PackageName { get; private set; }
        /// <summary>
        /// The item's product identifier. Every item has a product ID, which you must specify in the application's product list on the Google Play Developer Console.
        /// </summary>
        public string Sku { get; private set; }
        /// <summary>
        /// The time the product was purchased, in milliseconds since the epoch (Jan 1, 1970).
        /// </summary>
        public long PurchaseTime { get; private set; }
        /// <summary>
        /// The purchase state of the order. Possible values are 0 (purchased), 1 (canceled), or 2 (refunded).
        /// </summary>
        public int PurchaseState { get; private set; }
        /// <summary>
        /// A developer-specified string that contains supplemental information about an order. You can specify a value for this field when you make a getBuyIntent request.
        /// </summary>
        public string DeveloperPayload { get; private set; }
        /// <summary>
        /// A token that uniquely identifies a purchase for a given item and user pair. 
        /// </summary>
        public string Token { get; private set; }
        /// <summary>
        /// JSON sent by the current store
        /// </summary>
        public string OriginalJson { get; private set; }
        /// <summary>
        /// Signature of the JSON string
        /// </summary>
        public string Signature { get; private set; }
        /// <summary>
        /// Current store name
        /// </summary>
        public string AppstoreName { get; private set; }
        /// <summary>
        /// Purchase Receipt of the order (iOS only)
        /// </summary>
        public string Receipt { get; private set;}

        private Purchase()
        {
        }

        /**
         * Create purchase from json string
         * @param jsonString data serialized to json
         */
        public Purchase(string jsonString)
        {
            var json = new JSON(jsonString);
            ItemType = json.ToString("itemType");
            OrderId = json.ToString("orderId");
            PackageName = json.ToString("packageName");
            Sku = json.ToString("sku");
            PurchaseTime = json.ToLong("purchaseTime");
            PurchaseState = json.ToInt("purchaseState");
            DeveloperPayload = json.ToString("developerPayload");
            Token = json.ToString("token");
            OriginalJson = json.ToString("originalJson");
            Signature = json.ToString("signature");
            AppstoreName = json.ToString("appstoreName");
			Receipt = json.ToString("receipt");
        }

#if UNITY_IOS
        public Purchase(JSON json) {
            ItemType = json.ToString("itemType");
            OrderId = json.ToString("orderId");
            PackageName = json.ToString("packageName");
            Sku = json.ToString("sku");
            PurchaseTime = json.ToLong("purchaseTime");
            PurchaseState = json.ToInt("purchaseState");
            DeveloperPayload = json.ToString("developerPayload");
            Token = json.ToString("token");
            OriginalJson = json.ToString("originalJson");
            Signature = json.ToString("signature");
            AppstoreName = json.ToString("appstoreName");

			Sku = OpenIAB_iOS.StoreSku2Sku(Sku);
        }
#endif

        /**
         * For debug purposes and editor mode
         * @param sku product ID
         */ 
        public static Purchase CreateFromSku(string sku)
        {
            return CreateFromSku(sku, "");
        }

        public static Purchase CreateFromSku(string sku, string developerPayload)
        {
            var p = new Purchase();
            p.Sku = sku;
            p.DeveloperPayload = developerPayload;
#if UNITY_IOS
			AddIOSHack(p);
#endif
            return p;
        }

        /**
         * ToString
         * @return original json
         */ 
        public override string ToString()
        {
            return "SKU:" + Sku + ";" + OriginalJson;
        }

#if UNITY_IOS
		private static void AddIOSHack(Purchase p) {
			if(string.IsNullOrEmpty(p.AppstoreName)) {
				p.AppstoreName = "com.apple.appstore";
			}
			if(string.IsNullOrEmpty(p.ItemType)) {
				p.ItemType = "InApp";
			}
			if(string.IsNullOrEmpty(p.OrderId)) {
				p.OrderId = System.Guid.NewGuid().ToString();
			}
		}
#endif

        /**
         * Serilize to json
         * @return json string
         */ 
        public string Serialize()
        {
            var j = new JSON();
            j["itemType"] = ItemType;
            j["orderId"] = OrderId;
            j["packageName"] = PackageName;
            j["sku"] = Sku;
            j["purchaseTime"] = PurchaseTime;
            j["purchaseState"] = PurchaseState;
            j["developerPayload"] = DeveloperPayload;
            j["token"] = Token;
            j["originalJson"] = OriginalJson;
            j["signature"] = Signature;
            j["appstoreName"] = AppstoreName;
            j["receipt"] = Receipt;
            return j.serialized;
        }
    }
}
