using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
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

    public void RequestVideo(object arg) {
        RequestVideoAd(arg);
    }

    public void ShowVideo(object arg) {
        ShowVideoAd(arg);
    }
}
