using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// РЕКЛАМА ЭДМОБ - КОНТРОЛЛЕР

public class ClassAdMobController: ClassDebugConsol {

    public bool isNeedShowAdsInMenu = false;
    private static ClassAdMobController instance = null;
    public ClassAdMobeMenu adMobeMenu = null;
    public ClassAdMobeInterlevelAds adMobeInterlevel  = null;
    public ClassAdMobeReward adMobeReward = null;
    bool isAdsAllowed = true;

    public static ClassAdMobController GetI {
        get {
            if (instance == null) {
                instance = ((GameObject)Instantiate(Resources.Load("adMob"))).GetComponent<ClassAdMobController>();
            }
            return instance;
        }
    }

    // инициализируем MobileAds
    public void init(bool isDebugVisible, bool isAllowAds) {
        if (!IsActive()) {
            MobileAds.Initialize(initStatus => { });
        }
        setDedugVisible(isDebugVisible);
        allowAds = isAllowAds;
    }

    public static bool IsActive() {
        return instance != null;
    }

    void Awake() {
        showTextConsol("Awake");
        instance = this;
        DontDestroyOnLoad(gameObject);
        ClassDelay.DelayFramesCallBack(this, 2, loadAds);
    }

    // Инициализируем рекламу
    void loadAds() {
        if (isAdsAllowed) {
            showTextConsol("loadAds");
            if (isNeedShowAdsInMenu == true) {
                adMobeMenu.init();
            }
            adMobeInterlevel.init();
            adMobeReward.init();
        }
    }

    // Скрываем или отображаем на экране дебажный текст
    public override void setDedugVisible(bool isVisible) {
        base.setDedugVisible(isVisible);
        adMobeMenu.setDedugVisible(isVisible);
        adMobeReward.setDedugVisible(isVisible);
        adMobeInterlevel.setDedugVisible(isVisible);
    }

    public bool allowAds {
        set {
            isAdsAllowed = value;
        }
    }

    // Показать рекламу в меню
    public void showAdMenu() {
        if (isAdsAllowed && isNeedShowAdsInMenu) {
            adMobeMenu.tryShowAd();
        }
    }

    // Показать межуровневую рекламу
    public void showInterlevel() {
        if (isAdsAllowed) { 
            adMobeInterlevel.tryShowAd();
        }
    }

    // Видеореклама
    public void showRewardedAd() {
        if (isAdsAllowed) {
            adMobeReward.tryShowAd();
        }
    }

    void OnDestroy() {
        instance = null;
    }

}
