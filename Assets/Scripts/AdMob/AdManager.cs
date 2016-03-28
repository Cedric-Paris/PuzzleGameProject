using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;

public class AdManager {

	#if UNITY_ANDROID
	private const string adUnitId = "ca-app-pub-9641236307244569/6280394333";
	#elif UNITY_IPHONE
	private const string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
	#else
	private const string adUnitId = "unexpected_platform";
	#endif

	private static InterstitialAd inAd;

	static AdManager()
	{
		LoadInterstistialAd();
	}

	private static void initializeInterstitialAd()
	{
		if(inAd != null)
		{
			inAd.Destroy();
		}
		inAd = new InterstitialAd(adUnitId);
		inAd.OnAdClosed += HandleInterstitialClosed;
	}


	public static void LoadInterstistialAd()
	{

		if(inAd == null)
		{
			initializeInterstitialAd();
		}
		inAd.LoadAd(CreateRequest());
	}


	public static void ShowInterstitialAd()
	{
		try
		{
			if (inAd != null && inAd.IsLoaded())
			{
				inAd.Show();
			}
		}
		catch (Exception e) { UIMessageBox.ShowMessage(e.Message); }
	}


	private static AdRequest CreateRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("20F1166C0A4C226168BBF16FD6CCDA1F")
				.AddKeyword("game")
				.Build();
	}

	
	public static void HandleInterstitialClosed(object sender, EventArgs args)
	{
		LoadInterstistialAd();
	}


	/*
	public static void HandleInterstitialLoaded(object sender, EventArgs args) { }
	
	public static void HandleInterstitialOpened(object sender, EventArgs args) { }

	*/

}
