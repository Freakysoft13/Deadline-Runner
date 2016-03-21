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
    public OnEvent RequestVideoAd; //arg = bool isAdDuplex
    public OnEvent ShowVideoAd; //arg = bool isAdDuplex
    public OnEvent NoVideoAd; //arg = bool isAdDuplex
    public OnEvent OnAdLoadFailed;
    public OnEvent OnAdLoadSuccessful;
    public OnEvent OnAdFinished;

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
        NoVideoAd += (arg) => {
            isAdReady = false;
        };
    }

    void FallbackAds() {
        try {
            Vungle.init(androidAppID, iosAppID, winAppID);
            Vungle.adPlayableEvent += AdPlayableEvent;
        }
        catch (Exception) {
            isAdReady = false;
            RequestVideoAd(true);
        }
    }

    private void AdPlayableEvent(bool flag) {
        if (!flag) {
            RequestVideoAd(true);
        }
        isAdReady = true;
    }

    public void RequestVideo() {
        if (RequestVideoAd != null) {
            RequestVideoAd(false);
        }
        else {
            FallbackAds();
        }
    }

    public void ShowVideo(object arg, string subscriberUID, Action<object> onAdCompleted) {
        if (isOuterAdReady && ShowVideoAd != null) {
            ShowVideoAd(false);
            OnAdFinished += (argument) => {
                onAdCompleted(null);
            };
        }
        else if(Vungle.isAdvertAvailable()) {
            Vungle.playAdWithOptions(new Dictionary<string, object>());
            if (!subscribers.Keys.Contains(subscriberUID)) {
                Action<AdFinishedEventArgs> sub = (param) => {
                    onAdCompleted(param);
                };
                Vungle.onAdFinishedEvent += sub;
                subscribers.Add(subscriberUID, sub);
            }
        } else {
            ShowVideoAd(true);
            OnAdFinished += (argument) => {
                onAdCompleted(null);
            };
        }
    }

    public void Unsubscribe(string subscriberUID) {
        if (subscribers.Keys.Contains(subscriberUID)) {
            Vungle.onAdFinishedEvent -= subscribers[subscriberUID];
        }
        if(OnAdFinished != null) {
            OnAdFinished = null;
        }
    }

    private Dictionary<string, Action<AdFinishedEventArgs>> subscribers = new Dictionary<string, Action<AdFinishedEventArgs>>();
}
