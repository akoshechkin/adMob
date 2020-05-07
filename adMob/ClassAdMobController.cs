using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// РЕКЛАМА ЭДМОБ - КОНТРОЛЛЕР

public class ClassAdMobController: ClassDebugConsol {

    public bool isNeedShowAdsInMenu = false;
    private static ClassAdMobController m_Instance = null;
    public ClassAdMobeMenu adMobeMenu = null;
    public ClassAdMobeInterlevelAds adMobeInterlevel  = null;
    public ClassAdMobeReward adMobeReward = null;
    bool isShowAds = true;

    public static ClassAdMobController GetI {
        get {
            if (m_Instance == null) {
                m_Instance = ((GameObject)Instantiate(Resources.Load("adMob"))).GetComponent<ClassAdMobController>();
            }
            return m_Instance;
        }
    }

    // Initialize the Google Mobile Ads SDK.
    public void init() {
        MobileAds.Initialize(initStatus => { });
    }

    void Awake() {
        showTextConsol("Awake");
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
        ClassDelay.DelayFramesCallBack(this, 2, loadAds);
    }

    // Инициализируем рекламу
    void loadAds() {
        showTextConsol("loadAds");
        if (isShowAds) {
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

    // Прекращаем показ рекламы (например, произошла покупка её отключения)
    public void stopShowAds() {
        isShowAds = false;
    }

    // Показать рекламу в меню
    public void showAdMenu() {
        if (isShowAds && isNeedShowAdsInMenu) {
            adMobeMenu.tryShowAd();
        }
    }

    // Показать межуровневую рекламу
    public void showInterlevel() {
        if (isShowAds) { 
            adMobeInterlevel.tryShowAd();
        }
    }

    // Видеореклама
    public void showRewardedAd() {
        if (isShowAds) {
            adMobeReward.tryShowAd();
        }
    }

    void OnDestroy() {
        m_Instance = null;
    }

}
