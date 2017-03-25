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
using System;

using System.Collections.Generic;
using Assets.Plugins.SmartLevelsMap.Scripts;
using UnityEngine.Advertisements;
using ChartboostSDK;
using GoogleMobileAds.Api;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Target
{
	SCORE,
	COLLECT,
	INGREDIENT,
	BLOCKS
}

public enum LIMIT
{
	MOVES,
	TIME
}

public enum Ingredients
{
	None = 0,
	Ingredient1,
	Ingredient2
}

public enum CollectItems
{
	None = 0,
	Item1,
	Item2,
	Item3,
	Item4,
	Item5,
	Item6
}

public enum RewardedAdsType
{
	GetLifes,
	GetGems,
	GetGoOn
}

public class InitScript : MonoBehaviour
{
	public static InitScript Instance;
	public static int openLevel;


	public static float RestLifeTimer;
	public static string DateOfExit;
	public static DateTime today;
	public static DateTime DateOfRestLife;
	public static string timeForReps;
	private static int Lifes;

	bool loginForSharing;

	public RewardedAdsType currentReward;

	public static int lifes {
		get { return InitScript.Lifes; }
		set {
			InitScript.Lifes = value;
		}
	}

	public int CapOfLife = 5;
	public float TotalTimeForRestLifeHours = 0;
	public float TotalTimeForRestLifeMin = 15;
	public float TotalTimeForRestLifeSec = 60;
	public int FirstGems = 20;
	public static int Gems;
	public static int waitedPurchaseGems;
	private int BoostExtraMoves;
	private int BoostPackages;
	private int BoostStripes;
	private int BoostExtraTime;
	private int BoostBomb;
	private int BoostColorful_bomb;
	private int BoostHand;
	private int BoostRandom_color;

	public static bool sound = false;
	public static bool music = false;
	private bool adsReady;
	public string unityAdsIDAndroid;
	public string unityAdsIDIOS;
	public bool enableAds;
	public string rewardedVideoZone;
	public string nonRewardedVideoZone;
	public int ShowChartboostAdsEveryLevel;
	public int ShowAdmobAdsEveryLevel;
	private bool leftControl;
	private InterstitialAd interstitial;
	public string admobUIDAndroid;
	public string admobUIDIOS;
	private string lastResponse;
	private AdRequest requestAdmob;
	public int ShowRateEvery;
	public string RateURL;
	private GameObject rate;

	// Use this for initialization
	void Awake ()
	{
		Instance = this;
		RestLifeTimer = PlayerPrefs.GetFloat ("RestLifeTimer");

		DateOfExit = PlayerPrefs.GetString ("DateOfExit", "");
		Gems = PlayerPrefs.GetInt ("Gems");
		lifes = PlayerPrefs.GetInt ("Lifes");
		if (PlayerPrefs.GetInt ("Lauched") == 0) {    //First lauching
			lifes = CapOfLife;
			PlayerPrefs.SetInt ("Lifes", lifes);
			Gems = FirstGems;
			PlayerPrefs.SetInt ("Gems", Gems);
			PlayerPrefs.SetInt ("Music", 1);
			PlayerPrefs.SetInt ("Sound", 1);

			PlayerPrefs.SetInt ("Lauched", 1);
			PlayerPrefs.Save ();
		}
		rate = Instantiate (Resources.Load ("Prefabs/Rate")) as GameObject;
		rate.SetActive (false);
		rate.transform.SetParent (GameObject.Find ("CanvasGlobal").transform);
		rate.transform.localPosition = Vector3.zero;
		rate.GetComponent<RectTransform> ().anchoredPosition = (Resources.Load ("Prefabs/Rate") as GameObject).GetComponent<RectTransform> ().anchoredPosition;
		rate.transform.localScale = Vector3.one;

		GameObject.Find ("Music").GetComponent<AudioSource> ().volume = PlayerPrefs.GetInt ("Music");
		SoundBase.Instance.GetComponent<AudioSource> ().volume = PlayerPrefs.GetInt ("Sound");

		if (Advertisement.isSupported) {

			#if UNITY_ANDROID
			Advertisement.Initialize (unityAdsIDAndroid);
			#elif UNITY_IOS
			Advertisement.Initialize (unityAdsIDIOS);
			#else
			Advertisement.Initialize (unityAdsIDAndroid);
			#endif
		}
		#if UNITY_ANDROID
		interstitial = new InterstitialAd (admobUIDAndroid);
		#elif UNITY_IOS
		interstitial = new InterstitialAd (admobUIDIOS);
		#else
		interstitial = new InterstitialAd (admobUIDAndroid);
		#endif



		// Create an empty ad request.
		requestAdmob = new AdRequest.Builder ().Build ();
		// Load the interstitial with the request.
		interstitial.LoadAd (requestAdmob);

		Transform canvas = GameObject.Find ("CanvasGlobal").transform;
		foreach (Transform item in canvas) {
			item.gameObject.SetActive (false);
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftControl))
			leftControl = true;
		if (Input.GetKeyUp (KeyCode.LeftControl))
			leftControl = false;

		if (Input.GetKeyUp (KeyCode.U)) {
			print ("11");
			for (int i = 1; i < GameObject.Find ("Levels").transform.childCount; i++) {
				SaveLevelStarsCount (i, 1);
			}

		}
	}

	public void SaveLevelStarsCount (int level, int starsCount)
	{
		Debug.Log (string.Format ("Stars count {0} of level {1} saved.", starsCount, level));
		PlayerPrefs.SetInt (GetLevelKey (level), starsCount);

	}

	private string GetLevelKey (int number)
	{
		return string.Format ("Level.{0:000}.StarsCount", number);
	}

	public void ShowRewardedAds ()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show (rewardedVideoZone, new ShowOptions {
				resultCallback = result => {
					if (result == ShowResult.Finished) {
						CheckRewardedAds ();
					}
				}
			});
		}
	}

	public void ShowAds (bool chartboost = true)
	{
		if (chartboost) {
			Chartboost.showInterstitial (CBLocation.Default);
			Chartboost.cacheInterstitial (CBLocation.Default);
		} else {
			if (interstitial.IsLoaded ()) {
				interstitial.Show ();
				#if UNITY_ANDROID
				interstitial = new InterstitialAd (admobUIDAndroid);
				#elif UNITY_IOS
				interstitial = new InterstitialAd (admobUIDIOS);
				#else
				interstitial = new InterstitialAd (admobUIDAndroid);
				#endif

				// Create an empty ad request.
				requestAdmob = new AdRequest.Builder ().Build ();
				// Load the interstitial with the request.
				interstitial.LoadAd (requestAdmob);
			}
		}
	}

	public void ShowRate ()
	{
		rate.SetActive (true);
	}


	void CheckRewardedAds ()
	{
		Reward reward = GameObject.Find ("CanvasGlobal").transform.Find ("Reward").GetComponent<Reward> ();
		if (currentReward == RewardedAdsType.GetGems) {
			reward.SetIconSprite (0);

			reward.gameObject.SetActive (true);
			AddGems (LevelManager.THIS.gemsProducts [0].count);
			GameObject.Find ("CanvasGlobal").transform.Find ("GemsShop").GetComponent<AnimationManager> ().CloseMenu ();
		} else if (currentReward == RewardedAdsType.GetLifes) {
			reward.SetIconSprite (1);
			reward.gameObject.SetActive (true);
			RestoreLifes ();
			GameObject.Find ("CanvasGlobal").transform.Find ("LiveShop").GetComponent<AnimationManager> ().CloseMenu ();
		} else if (currentReward == RewardedAdsType.GetGoOn) {
			GameObject.Find ("CanvasGlobal").transform.Find ("PreFailed").GetComponent<AnimationManager> ().GoOnFailed ();
		}

	}

	public void AddGems (int count)
	{
		Gems += count;
		PlayerPrefs.SetInt ("Gems", Gems);
		PlayerPrefs.Save ();
	}

	public void SpendGems (int count)
	{
		SoundBase.Instance.GetComponent<AudioSource> ().PlayOneShot (SoundBase.Instance.cash);
		Gems -= count;
		PlayerPrefs.SetInt ("Gems", Gems);
		PlayerPrefs.Save ();
	}


	public void RestoreLifes ()
	{
		lifes = CapOfLife;
		PlayerPrefs.SetInt ("Lifes", lifes);
		PlayerPrefs.Save ();
	}

	public void AddLife (int count)
	{
		lifes += count;
		if (lifes > CapOfLife)
			lifes = CapOfLife;
		PlayerPrefs.SetInt ("Lifes", lifes);
		PlayerPrefs.Save ();
	}

	public int GetLife ()
	{
		if (lifes > CapOfLife) {
			lifes = CapOfLife;
			PlayerPrefs.SetInt ("Lifes", lifes);
			PlayerPrefs.Save ();
		}
		return lifes;
	}

	public void PurchaseSucceded ()
	{
		AddGems (waitedPurchaseGems);
		waitedPurchaseGems = 0;
	}

	public void SpendLife (int count)
	{
		if (lifes > 0) {
			lifes -= count;
			PlayerPrefs.SetInt ("Lifes", lifes);
			PlayerPrefs.Save ();
		}
		//else
		//{
		//    GameObject.Find("Canvas").transform.Find("RestoreLifes").gameObject.SetActive(true);
		//}
	}

	public void BuyBoost (BoostType boostType, int price, int count)
	{
		PlayerPrefs.SetInt ("" + boostType, count);
		PlayerPrefs.Save ();
		//   ReloadBoosts();
	}

	public void SpendBoost (BoostType boostType)
	{
		PlayerPrefs.SetInt ("" + boostType, PlayerPrefs.GetInt ("" + boostType) - 1);
		PlayerPrefs.Save ();
	}
	//void ReloadBoosts()
	//{
	//    BoostExtraMoves = PlayerPrefs.GetInt("" + BoostType.ExtraMoves);
	//    BoostPackages = PlayerPrefs.GetInt("" + BoostType.Packages);
	//    BoostStripes = PlayerPrefs.GetInt("" + BoostType.Stripes);
	//    BoostExtraTime = PlayerPrefs.GetInt("" + BoostType.ExtraTime);
	//    BoostBomb = PlayerPrefs.GetInt("" + BoostType.Bomb);
	//    BoostColorful_bomb = PlayerPrefs.GetInt("" + BoostType.Colorful_bomb);
	//    BoostHand = PlayerPrefs.GetInt("" + BoostType.Hand);
	//    BoostRandom_color = PlayerPrefs.GetInt("" + BoostType.Random_color);

	//}
	//public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra)
	//{
	//    PurchaseSucceded();
	//}


	void OnApplicationPause (bool pauseStatus)
	{
		if (pauseStatus) {
			if (RestLifeTimer > 0) {
				PlayerPrefs.SetFloat ("RestLifeTimer", RestLifeTimer);
			}
			PlayerPrefs.SetInt ("Lifes", lifes);
			PlayerPrefs.SetString ("DateOfExit", DateTime.Now.ToString ());
			PlayerPrefs.Save ();
		}
	}

	public void OnLevelClicked (object sender, LevelReachedEventArgs args)
	{
		if (EventSystem.current.IsPointerOverGameObject (-1))
			return;
		if (!GameObject.Find ("CanvasGlobal").transform.Find ("MenuPlay").gameObject.activeSelf && !GameObject.Find ("CanvasGlobal").transform.Find ("GemsShop").gameObject.activeSelf && !GameObject.Find ("CanvasGlobal").transform.Find ("LiveShop").gameObject.activeSelf) {
			PlayerPrefs.SetInt ("OpenLevel", args.Number);
			PlayerPrefs.Save ();
			LevelManager.THIS.LoadLevel ();
			openLevel = args.Number;
			//  currentTarget = targets[args.Number];
			GameObject.Find ("CanvasGlobal").transform.Find ("MenuPlay").gameObject.SetActive (true);
		}
	}

	void OnEnable ()
	{
		LevelsMap.LevelSelected += OnLevelClicked;
	}

	void OnDisable ()
	{
		LevelsMap.LevelSelected -= OnLevelClicked;

		//		if(RestLifeTimer>0){
		PlayerPrefs.SetFloat ("RestLifeTimer", RestLifeTimer);
		//		}
		PlayerPrefs.SetInt ("Lifes", lifes);
		PlayerPrefs.SetString ("DateOfExit", DateTime.Now.ToString ());
		PlayerPrefs.Save ();

	}

	#region FaceBook

	public void CallFBInit ()
	{
		FB.Init (OnInitComplete, OnHideUnity);
	}

	private void OnInitComplete ()
	{
		Debug.Log ("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
	}

	private void OnHideUnity (bool isGameShown)
	{
		Debug.Log ("Is game showing? " + isGameShown);
	}


	public void CallFBLogin ()
	{
		FB.Login ("public_profile,email,user_friends", LoginCallback);
	}

	public void CallFBLoginForPublish ()
	{
		// It is generally good behavior to split asking for read and publish
		// permissions rather than ask for them all at once.
		//
		// In your own game, consider postponing this call until the moment
		// you actually need it.
		FB.Login ("publish_actions", LoginCallback);
	}

	void LoginCallback (FBResult result)
	{

		if (result.Error != null)
			lastResponse = "Error Response:\n" + result.Error;
		else if (!FB.IsLoggedIn) {
			lastResponse = "Login cancelled by Player";
		} else {
			lastResponse = "Login was successful!";
			if (loginForSharing) {
				loginForSharing = false;
				Share ();
			}
		}
		Debug.Log (lastResponse);
	}

	private void CallFBLogout ()
	{
		FB.Logout ();
	}

	public void Share ()
	{
		if (!FB.IsLoggedIn) {
			loginForSharing = true;
			CallFBLogin ();
			Debug.Log ("not logged, logging");
		} else {
			FB.Feed (
				link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest"),
				linkCaption: "I'm got " + LevelManager.Score + " scores! Try to beat me!"
            //picture: "https://fbexternal-a.akamaihd.net/safe_image.php?d=AQCzlvjob906zmGv&w=128&h=128&url=https%3A%2F%2Ffbcdn-photos-h-a.akamaihd.net%2Fhphotos-ak-xtp1%2Ft39.2081-0%2F11891368_513258735497916_1832270581_n.png&cfs=1"
			);
		}
	}

	#endregion

}
