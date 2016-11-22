using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public int msAdsRetryCount = 1;

    public bool IsAdReady
    {
        get
        {
#if UNITY_ANDROID
            return Advertisement.IsReady();
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

    public delegate void AdsHandler(object arg);

    public static event AdsHandler RequestMsAd;

    public static event AdsHandler DisplayMsAd;
    
    private bool displayAds = false;
    private bool isMSAds = false;
    

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
        RequestMsAd(null);
    }
    public void OnMSReady(object obj)
    {
        displayAds = true;
        isMSAds = true;
    }
    public void OnMSAdCompleted(object obj)
    {
        //RequestMsAd(null);
        if(OnAdCompleted != null) {
            OnAdCompleted(obj);
            OnAdCompleted = null;
        }
    }
    public void OnMSAdCancelled(object obj)
    {
        RequestMsAd(null);
    }
    public void OnMSAdError(object obj)
    {
    }
    
    private void AdPlayableEvent(bool flag)
    {
        displayAds = false;
    }

    public void RequestVideo()
    {
        if (isMSAds)
        {
            RequestMsAd(null);
        }
    }

    public void ShowVideo(Action<object> callback)
    {
        OnAdCompleted = callback;
        if (isMSAds)
        {
            DisplayMsAd(null);
        }
    }
}
