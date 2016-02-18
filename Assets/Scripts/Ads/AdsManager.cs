using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public string androidAppID;
    public string iosAppID;
    public string winAppID;

    public bool isAdReady = false;
    public bool isOuterAdReady = false;

    public delegate void OnEvent(object arg);
    public OnEvent RequestVideoAd;
    public OnEvent ShowVideoAd;
    public OnEvent OnAdLoadFailed;
    public OnEvent OnAdLoadSuccessful;

    private static AdsManager instance = null;

    public static AdsManager Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            DestroyImmediate(this);
        }
        OnAdLoadFailed += (arg) => {
            isOuterAdReady = false;
            FallbackAds();
        };
        OnAdLoadSuccessful += (arg) => {
            isAdReady = true;
            isOuterAdReady = true;
        };
    }

    void FallbackAds() {
        try {
            Vungle.init(androidAppID, iosAppID, winAppID);
            Vungle.adPlayableEvent += AdPlayableEvent;
        }
        catch (Exception) {
            isAdReady = false;
        }
    }

    private void AdPlayableEvent(bool flag) {
        isAdReady = true;
    }

    public void RequestVideo(object arg) {
        if (RequestVideoAd != null) {
            RequestVideoAd(arg);
        }
        else {
            FallbackAds();
        }
    }

    public void ShowVideo(object arg, string subscriberUID, Action<AdFinishedEventArgs> onAdCompleted) {
        if (isOuterAdReady && ShowVideoAd != null) {
            ShowVideoAd(arg);
        }
        else {
            Vungle.playAdWithOptions(new Dictionary<string, object>());
            if (!subscribers.Keys.Contains(subscriberUID)) {
                Vungle.onAdFinishedEvent += onAdCompleted;
                subscribers.Add(subscriberUID, onAdCompleted);
            }
        }
    }

    public void Unsubscribe(string subscriberUID) {
        if (subscribers.Keys.Contains(subscriberUID)) {
            Vungle.onAdFinishedEvent -= subscribers[subscriberUID];
        }
    }

    private Dictionary<string, Action<AdFinishedEventArgs>> subscribers = new Dictionary<string, Action<AdFinishedEventArgs>>();
}
