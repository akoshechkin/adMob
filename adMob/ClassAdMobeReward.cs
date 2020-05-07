using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

// Реклама за награду

public class ClassAdMobeReward : ClassDebugConsol {

    public static string ON_REWARD_AD_FAILED_TO_SHOW = "ON_REWARD_AD_FAILED_TO_SHOW";
    public static string ON_REWARD_AD_CLOSED = "ON_REWARD_AD_CLOSED";
    public static string ON_REWARD_AD_EARNED = "ON_REWARD_AD_EARNED";
    public static string ON_REWARD_START_SHOW = "ON_REWARD_START_SHOW";

    public RewardBasedVideoAd rewardBasedVideo = null;
    public float delaySecondLoadAfterClosed = 2;
    int countFailedLoads = 0;

    public void init() {
        showTextConsol("init");
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        //rewardBasedVideo.OnAdOpening+=HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        //rewardBasedVideo.OnAdStarted+=HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //rewardBasedVideo.OnAdLeavingApplication+=HandleOnAdLeavingApplication;
        load();
    }

    // Загрузка рекламы
    void load() {
        showTextConsol("load");

        string adUnitIdReward = "unused";
#if UNITY_ANDROID
        adUnitIdReward = "ca-app-pub-11111";
#elif UNITY_IPHONE
        adUnitIdReward = "ca-app-pub-00000";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("C7A5FD3B48E3CC0A4D8A875CDC47EFEF")
            .Build();
        // Load the rewarded ad with the request.
        rewardBasedVideo.LoadAd(request, adUnitIdReward);
    }

    // ***************************************************************** Start Handle ********************************

    // Ошибка загрузки - пауза перед повторной
    void HandleOnAdFailedToLoad(object sender, EventArgs args) {
        float delaySecondsLoadAfterFailed = ++countFailedLoads >= 3 ? 10 : 30;
        showTextConsol("HandleOnAdFailedToLoad! delay=" + delaySecondsLoadAfterFailed);
        ClassDelay.DelaySecondCallBack(this, delaySecondsLoadAfterFailed, load);
    }

    // Реклама загрузилась
    void HandleOnAdLoaded(object sender, EventArgs args) {
        countFailedLoads = 0;
        showTextConsol("HandleOnAdLoaded!");
    }

    // Реклама закрыта
    void HandleOnAdClosed(object sender, EventArgs args) {
        showTextConsol("HandleOnAdClosed! delay=" + delaySecondLoadAfterClosed);
        NotificationCenter.GetI.postNotification(ON_REWARD_AD_CLOSED,this);
        ClassDelay.DelaySecondCallBack(this, delaySecondLoadAfterClosed, load);
    }

    // Награда за просмотр
    void HandleUserEarnedReward(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        int countPrizes = Mathf.RoundToInt((float)amount);
        showTextConsol("HandleUserEarnedReward! countPrizes=" + countPrizes + " type=" + type);
        NotificationCenter.GetI.postNotification(ON_REWARD_AD_EARNED, this);
    }

    // ***************************************************************** End Handle ********************************

    // Показываем рекламу ******
    public void tryShowAd() {
        showTextConsol("tryShowAd:");
        if (rewardBasedVideo == null) {
            showTextConsol("no: rewardedAd==null!");
            NotificationCenter.GetI.postNotification(ON_REWARD_AD_FAILED_TO_SHOW, this);
        } else if (rewardBasedVideo.IsLoaded() == true) {
            showTextConsol("ok: show");
            rewardBasedVideo.Show();
            NotificationCenter.GetI.postNotification(ON_REWARD_START_SHOW, this);
        } else {
            showTextConsol("no: not loaded!");
            NotificationCenter.GetI.postNotification(ON_REWARD_AD_FAILED_TO_SHOW, this);
        }
    }
}
