using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{

    public string vungleMSId;

    private List<MonoBehaviour> subscirbers = new List<MonoBehaviour>();

    public bool IsAdReady
    {
        get
        {
#if UNITY_ANDROID
            return Advertisement.IsReady();
#else
            return Vungle.isAdvertAvailable();
#endif
        }
    }

    public Action<object> OnAdCompleted;

    private static AdsManager instance = null;

    public static AdsManager Instance
    {
        get { return instance; }
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
#if UNITY_WSA
    void Start()
    {
        Vungle.init("", "", vungleMSId);
        Vungle.onAdFinishedEvent += (evnt) => 
        {
            Time.timeScale = 1;
        };
    }

    public void SubscribeForAdFinishEvent(Action<object> action, MonoBehaviour subscriber)
    {
        if (!subscirbers.Contains(subscriber))
        {
            Vungle.onAdFinishedEvent += (evnt) => { action(evnt); };
            subscirbers.Add(subscriber);
        }
    }

    public void ShowVideo()
    {
        if (Vungle.isAdvertAvailable())
        {
            Vungle.playAd();
            Time.timeScale = 0;
        }
    }
#endif
}
