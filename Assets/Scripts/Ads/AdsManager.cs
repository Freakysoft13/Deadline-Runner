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

    public bool isAdReady;

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
            isAdReady = false;
        };
        OnAdLoadSuccessful += (arg) => {
            isAdReady = true;
        };
    }

    void Start() {
        Vungle.init(androidAppID, iosAppID, winAppID);
        Vungle.adPlayableEvent += AdPlayableEvent;
    }

    private void AdPlayableEvent(bool flag) {
        isAdReady = true;
    }

    public void RequestVideo(object arg) {
        /*string[] args = new string[2];
        args[0] = "795675e4-9376-416a-89a2-1e6fe8249ed5";
        args[1] = "11569621";
        RequestVideoAd(args);*/
    }

    public void ShowVideo(object arg, Action<AdFinishedEventArgs> onAdCompleted) {
        if(ShowVideoAd != null) {
            ShowVideoAd(arg);
        }
        Vungle.playAdWithOptions(new Dictionary<string, object>());
        Vungle.onAdFinishedEvent += onAdCompleted;
    }
}
