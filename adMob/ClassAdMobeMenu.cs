using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;

// Реклама в главном меню

public class ClassAdMobeMenu : ClassBaseAdMobInterstitialAd {

    public static string ON_MAINMENU_AD_CLOSED = "ON_MAINMENU_AD_CLOSED";

    public void init() {
        init("ca-app-pub-0000", "ca-app-pub-1111");
    }

    public override void HandleOnAdClosed(object sender, EventArgs args) {
        base.HandleOnAdClosed(sender, args);
        NotificationCenter.GetI.postNotification(ON_MAINMENU_AD_CLOSED, this);
    }

}


