using GoogleMobileAds.Api;
using System;

// Межстаничная реклама базовый класс

public class ClassBaseAdMobInterstitialAd : ClassDebugConsol {

    InterstitialAd interstitialAd = null;
    string adUnitId_ANDROID="", adUnitId_IOS="";

    internal void init(string _adUnitId_ANDROID, string _adUnitId_IOS) {
        showTextConsol("init");
        adUnitId_ANDROID = _adUnitId_ANDROID;
        adUnitId_IOS = _adUnitId_IOS;
        load();
    }

    internal void load() {
        showTextConsol("load");
        StopAllCoroutines();
        string adUnitId = "unused";
#if UNITY_ANDROID
        adUnitId = adUnitId_ANDROID;
#elif UNITY_IPHONE
        adUnitId = adUnitId_IOS;
#endif
        interstitialAd = new InterstitialAd(adUnitId);
        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        interstitialAd.OnAdClosed += HandleOnAdClosed;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("C7A5FD3B48E3CC0A4D8A875CDC47EFEF")
            .Build();
        interstitialAd.LoadAd(request);
    }

    // ***************************************************************** Start Handle ********************************

    virtual public void HandleOnAdFailedToLoad(object sender, EventArgs args) {
        showTextConsol("HandleOnAdFailedToLoad ");
        destroy();
        ClassDelay.DelaySecondCallBack(this, 30, load);
    }

    virtual public void HandleOnAdLoaded(object sender, EventArgs args) {
        showTextConsol("HandleOnAdLoaded");
    }

    virtual public void HandleOnAdClosed(object sender, EventArgs args) {
        showTextConsol("HandleOnAdClosed");
        destroy();
    }

    // ***************************************************************** End Handle ********************************

    virtual public void tryShowAd() {
        showTextConsol("tryShowAd!");
        if (interstitialAd == null) {
            showTextConsol("no: interstitialAd==null");
        } else if (interstitialAd.IsLoaded() == true) {
            showTextConsol("ok: show");
            interstitialAd.Show();
        } else {
            showTextConsol("no: not loaded");
        }
    }

    internal void destroy() {
        showTextConsol("destroy");
        interstitialAd.Destroy();
        interstitialAd = null;
    }


}
