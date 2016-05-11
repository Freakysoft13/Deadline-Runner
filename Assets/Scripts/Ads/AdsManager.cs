using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public int msAdsRetryCount = 1;

    public string androidAppID;
    public string iosAppID;
    public string winAppID;

    public bool IsAdReady
    {
        get
        {
#if UNITY_ANDROID
            return true;
#else
            return displayAds;
#endif
        }
        set { displayAds = value; }
    }

    public Action<object> OnAdCompleted;

    private static AdsManager instance = null;

    public static AdsManager Instance
    {
        get { return instance; }
    }

    private Microsoft.UnityPlugins.IInterstittialAd msInterstitialAd;
    private const string MicrosoftAdsAppId = "bba15f80-e19f-4471-83a7-b4dc040764e1";
    private int retryCount = 0;
    private bool displayAds = false;
    private bool isMSAds = false;
    protected string NextMicrosoftAd
    {
        get
        {
            return "11569621";
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    void Start()
    {
        msInterstitialAd = Microsoft.UnityPlugins.MicrosoftAdsBridge.InterstitialAdFactory.CreateAd(OnMSReady,
            OnMSAdCompleted, OnMSAdCancelled, OnMSAdError);
        RequestMSAd();
    }
    public void OnMSReady(object obj)
    {
        displayAds = true;
    }
    public void OnMSAdCompleted(object obj)
    {
        RequestMSAd();
        OnAdCompleted(obj);
        OnAdCompleted = null;
    }
    public void OnMSAdCancelled(object obj)
    {
        RequestMSAd();
    }
    public void OnMSAdError(object obj)
    {
        FallbackAds();
        /*if (retryCount++ < msAdsRetryCount) {
            RequestMSAd();
        }
        else {
            FallbackAds();
        }*/
    }

    private void RequestMSAd()
    {
        msInterstitialAd.Request(MicrosoftAdsAppId, NextMicrosoftAd, Microsoft.UnityPlugins.AdType.Video);
    }
    private void ShowMSAd()
    {
        msInterstitialAd.Show();
    }
    private void ShowVungleAd()
    {
        Vungle.playAd();
    }

    void FallbackAds()
    {
        isMSAds = true;
        Vungle.init("com.prime31.Vungle", "vungleTest", "vungleTest");
        Vungle.adPlayableEvent += AdPlayableEvent;
        Vungle.onAdFinishedEvent += VungleAdFinished;
    }

    private void VungleAdFinished(AdFinishedEventArgs args)
    {
        OnAdCompleted(args);
        OnAdCompleted = null;
    }
    private void AdPlayableEvent(bool flag)
    {
        displayAds = false;
    }

    public void RequestVideo()
    {
        if (isMSAds)
        {
            RequestMSAd();
        }
    }

    public void ShowVideo(Action<object> callback)
    {
        OnAdCompleted = callback;
        if (isMSAds)
        {
            ShowMSAd();
        }
        else
        {
            ShowVungleAd();
        }
    }
}
