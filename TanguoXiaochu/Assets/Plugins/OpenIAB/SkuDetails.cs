/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace OnePF
{
    /**
     * Represents an in-app product's listing details.
     */
    public class SkuDetails
    {
        public string ItemType { get; private set; }
        public string Sku { get; private set; }
        public string Type { get; private set; }
        public string Price { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Json { get; private set; }
        public string CurrencyCode { get; private set; }
        public string PriceValue { get; private set; }

        // Used for Android
        public SkuDetails(string jsonString)
        {
            var json = new JSON(jsonString);
            ItemType = json.ToString("itemType");
            Sku = json.ToString("sku");
            Type = json.ToString("type");
            Price = json.ToString("price");
            Title = json.ToString("title");
            Description = json.ToString("description");
            Json = json.ToString("json");
            CurrencyCode = json.ToString("currencyCode");
            PriceValue = json.ToString("priceValue");
            ParseFromJson();
        }

#if UNITY_IOS
        public SkuDetails(JSON json) {
            ItemType = json.ToString("itemType");
            Sku = json.ToString("sku");
            Type = json.ToString("type");
            Price = json.ToString("price");
            Title = json.ToString("title");
            Description = json.ToString("description");
            Json = json.ToString("json");
			CurrencyCode = json.ToString("currencyCode");
			PriceValue = json.ToString("priceValue");

            Sku = OpenIAB_iOS.StoreSku2Sku(Sku);
        }
#endif

#if UNITY_WP8
        public SkuDetails(OnePF.WP8.ProductListing listing)
        {
            Sku = OpenIAB_WP8.GetSku(listing.ProductId);
            Title = listing.Name;
            Description = listing.Description;
            Price = listing.FormattedPrice;
        }
#endif

        private void ParseFromJson()
        {
            if (string.IsNullOrEmpty(Json)) return;
            var json = new JSON(Json);
            if (string.IsNullOrEmpty(PriceValue))
            {
                float val = json.ToFloat("price_amount_micros");
                val /= 1000000;
                PriceValue = val.ToString();
            }
            if (string.IsNullOrEmpty(CurrencyCode))
                CurrencyCode = json.ToString("price_currency_code");
        }

        /**
         * ToString
         * @return formatted string
         */ 
        public override string ToString()
        {
            return string.Format("[SkuDetails: type = {0}, SKU = {1}, title = {2}, price = {3}, description = {4}, priceValue={5}, currency={6}]",
                                 ItemType, Sku, Title, Price, Description, PriceValue, CurrencyCode);
        }
    }
}
