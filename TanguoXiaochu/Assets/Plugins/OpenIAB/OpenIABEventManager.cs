/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using OnePF;

/**
 * Simple event manager. Subscribe to handle billing events
 */ 
public class OpenIABEventManager : MonoBehaviour
{
    /**
     * Successfull Init callback. Billing is supported on current platform
     */ 
    public static event Action billingSupportedEvent;

    /**
     * Failed Init callback. Billing is not supported on current platform
     */
    public static event Action<string> billingNotSupportedEvent;
    
    /**
     * Successful QueryInventory callback. Purchase history and store listings are returned
     */ 
    public static event Action<Inventory> queryInventorySucceededEvent;

    /**
     * Failed QueryInventory callback.
     */
    public static event Action<string> queryInventoryFailedEvent;
    
    /**
     * Successful purchase callback. Fired after purchase of a product or a subscription
     */ 
    public static event Action<Purchase> purchaseSucceededEvent;
    
    /**
     * Failed purchase callback
     */ 
    public static event Action<int, string> purchaseFailedEvent;
    
    /**
     * Successful consume attempt callback
     */ 
    public static event Action<Purchase> consumePurchaseSucceededEvent;

    /**
     * Failed consume attempt callback
     */ 
    public static event Action<string> consumePurchaseFailedEvent;

#pragma warning disable 0067
    /**
     * Fired when transaction was restored
     */ 
    public static event Action<string> transactionRestoredEvent;
    
    /**
     * Fired when transaction restoration process failed
     */ 
    public static event Action<string> restoreFailedEvent;
    
    /**
     * Fired when transaction restoration process succeeded
     */ 
    public static event Action restoreSucceededEvent;
#pragma warning restore 0067

    private void Awake()
    {
        // Set the GameObject name to the class name for easy access from native plugin
        gameObject.name = GetType().ToString();
        DontDestroyOnLoad(this);
    }

#if UNITY_ANDROID
    private void OnMapSkuFailed(string exception)
    {
        Debug.LogError("SKU mapping failed: " + exception);
    }

    private void OnBillingSupported(string empty)
    {
        if (billingSupportedEvent != null)
            billingSupportedEvent();
    }

    private void OnBillingNotSupported(string error)
    {
        if (billingNotSupportedEvent != null)
            billingNotSupportedEvent(error);
    }

    private void OnQueryInventorySucceeded(string json)
    {
        if (queryInventorySucceededEvent != null)
        {
            Inventory inventory = new Inventory(json);
            queryInventorySucceededEvent(inventory);
        }
    }

    private void OnQueryInventoryFailed(string error)
    {
        if (queryInventoryFailedEvent != null)
            queryInventoryFailedEvent(error);
    }

    private void OnPurchaseSucceeded(string json)
    {
        if (purchaseSucceededEvent != null)
            purchaseSucceededEvent(new Purchase(json));
    }

    private void OnPurchaseFailed(string message)
    {
		int errorCode = -1;
		string errorMessage = "Unknown error";

		if (!string.IsNullOrEmpty(message)) {
	        string[] tokens = message.Split('|');

			if (tokens.Length >= 2) {
				Int32.TryParse(tokens[0], out errorCode);
				errorMessage = tokens[1];
			} else {
				errorMessage = message;
			}
		}
        if (purchaseFailedEvent != null)
            purchaseFailedEvent(errorCode, errorMessage);
    }

    private void OnConsumePurchaseSucceeded(string json)
    {
        if (consumePurchaseSucceededEvent != null)
            consumePurchaseSucceededEvent(new Purchase(json));
    }

    private void OnConsumePurchaseFailed(string error)
    {
        if (consumePurchaseFailedEvent != null)
            consumePurchaseFailedEvent(error);
    }

    public void OnTransactionRestored(string sku)
    {
        if (transactionRestoredEvent != null)
        {
            transactionRestoredEvent(sku);
        }
    }

    public void OnRestoreTransactionFailed(string error)
    {
        if (restoreFailedEvent != null)
        {
            restoreFailedEvent(error);
        }
    }

    public void OnRestoreTransactionSucceeded(string message)
    {
        if (restoreSucceededEvent != null)
        {
            restoreSucceededEvent();
        }
    }
#endif

#if UNITY_IOS
    private void OnBillingSupported(string empty)
    {
        if (billingSupportedEvent != null)
        {
            billingSupportedEvent();
        }
    }

    private void OnBillingNotSupported(string error)
    {
        if (billingNotSupportedEvent != null)
            billingNotSupportedEvent(error);
    }

    private void OnQueryInventorySucceeded(string json)
    {
        if (queryInventorySucceededEvent != null)
        {
            Inventory inventory = new Inventory(json);
            queryInventorySucceededEvent(inventory);
        }
    }

    private void OnQueryInventoryFailed(string error)
    {
        if (queryInventoryFailedEvent != null)
            queryInventoryFailedEvent(error);
    }

    private void OnPurchaseSucceeded(string json)
    {
        if (purchaseSucceededEvent != null)
        {
            purchaseSucceededEvent(new Purchase(json));
        }
    }

    private void OnPurchaseFailed(string error)
    {
        if (purchaseFailedEvent != null)
        {
            purchaseFailedEvent(-1, error);
        }
    }

    private void OnConsumePurchaseSucceeded(string json)
    {
        if (consumePurchaseSucceededEvent != null)
            consumePurchaseSucceededEvent(new Purchase(json));
    }

    private void OnConsumePurchaseFailed(string error)
    {
        if (consumePurchaseFailedEvent != null)
            consumePurchaseFailedEvent(error);
    }

    public void OnPurchaseRestored(string sku)
    {
        if (transactionRestoredEvent != null)
        {
            transactionRestoredEvent(sku);
        }
    }

    public void OnRestoreFailed(string error)
    {
        if (restoreFailedEvent != null)
        {
            restoreFailedEvent(error);
        }
    }

    public void OnRestoreFinished(string message)
    {
        if (restoreSucceededEvent != null)
        {
            restoreSucceededEvent();
        }
    }
#endif

#if UNITY_WP8
    public void OnBillingSupported()
    {
        if (billingSupportedEvent != null)
            billingSupportedEvent();
    }

    public void OnBillingNotSupported(string error)
    {
        if (billingNotSupportedEvent != null)
            billingNotSupportedEvent(error);
    }

    private void OnQueryInventorySucceeded(Inventory inventory)
    {
        if (queryInventorySucceededEvent != null)
            queryInventorySucceededEvent(inventory);
    }

    private void OnQueryInventoryFailed(string error)
    {
        if (queryInventoryFailedEvent != null)
            queryInventoryFailedEvent(error);
    }

    private void OnPurchaseSucceeded(Purchase purchase)
    {
        if (purchaseSucceededEvent != null)
            purchaseSucceededEvent(purchase);
    }

    private void OnPurchaseFailed(string error)
    {
        if (purchaseFailedEvent != null)
            purchaseFailedEvent(-1, error);
    }

    private void OnConsumePurchaseSucceeded(Purchase purchase)
    {
        if (consumePurchaseSucceededEvent != null)
            consumePurchaseSucceededEvent(purchase);
    }

    private void OnConsumePurchaseFailed(string error)
    {
        if (consumePurchaseFailedEvent != null)
            consumePurchaseFailedEvent(error);
    }
#endif
}
