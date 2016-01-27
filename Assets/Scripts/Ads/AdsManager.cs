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
        string[] args = new string[2];
        if(Application.isMobilePlatform) {
            args[0] = "bba15f80-e19f-4471-83a7-b4dc040764e111569621";
            args[1] = "11565268";
        } else {
            args[0] = "795675e4-9376-416a-89a2-1e6fe8249ed5";
            args[1] = "11569621";
        }
        RequestVideoAd(args);
    }

    public void ShowVideo(object arg) {
        ShowVideoAd(arg);
    }
}
