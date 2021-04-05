using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;

public class MobAdsSimple : MonoBehaviour
{
    private InterstitialAd interstitialAd;

#if UNITY_ANDROID
    private const string interstitialUnitId = "ca-app-pub-3169956978186495/6590052756"; //тестовый айди
#elif UNITY_IPHONE
    private const string interstitialUnitId = "ca-app-pub-3169956978186495/5487889920";
#else
    private const string interstitialUnitId = "unexpected_platform";
#endif
    void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    public void ShowAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
            interstitialAd = new InterstitialAd(interstitialUnitId);
            AdRequest adRequest = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequest);
        }
    }
}
