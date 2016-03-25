using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class ChargeAd : MonoBehaviour {

	InterstitialAd i;

	// Use this for initialization
	void Start () {
		AdManager.LoadInterstistialAd();
	}
	
	public void Clic () {
		AdManager.ShowInterstitialAd();
	}

}
