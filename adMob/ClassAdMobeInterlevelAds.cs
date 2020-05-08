using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

// Реклама между уровнями

public class ClassAdMobeInterlevelAds : ClassBaseAdMobInterstitialAd {

    public static string ON_INTERLEVEL_AD_CLOSED = "ON_INTERLEVEL_AD_CLOSED";

    public void init() {
        init("ca-app-pub-0000", "ca-app-pub-111");
    }

    public override void HandleOnAdClosed(object sender, EventArgs args) {
        base.HandleOnAdClosed(sender, args);
        NotificationCenter.GetI.postNotification(ON_INTERLEVEL_AD_CLOSED, this);
        ClassDelay.DelaySecondCallBack(this, 30, load);
    }

}
