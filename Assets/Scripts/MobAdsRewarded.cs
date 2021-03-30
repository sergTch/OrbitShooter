using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using GoogleMobileAds.Api;

public class MobAdsRewarded : MonoBehaviour
{
    private RewardedAd rewardedAd;
    [SerializeField] private Text cointsText;
    private int coints = 0;

#if UNITY_ANDROID
    private const string rewardedUnitId = "ca-app-pub-3169956978186495/4085795452"; //тестовый айди
#elif UNITY_IPHONE
    private const string rewardedUnitId = "ca-app-pub-3169956978186495~4996935333";
#else
    private const string rewardedUnitId = "unexpected_platform";
#endif
    void OnEnable()
    {
        rewardedAd = new RewardedAd(rewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);

        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    void OnDisable()
    {
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();

            rewardedAd = new RewardedAd(rewardedUnitId);
            AdRequest adRequest = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(adRequest);
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        PlayerPrefs.SetString(Core.menu.reward, (DateTime.Now + TimeSpan.FromMinutes(2)).ToString());
        Core.menu.LoadPurchases();
    }
}
