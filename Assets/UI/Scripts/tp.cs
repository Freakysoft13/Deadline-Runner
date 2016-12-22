﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class tp : MonoBehaviour
{
    public GameObject pause_pnl;
    public GameObject blockingmask;
    public GameObject scoreboard;
    public GameObject deathpnl;
    public GameObject result_pnl;

    public Text Money_txt;
    public Text Distance_txt;
    //player object for distance meeter
    public GameObject Player;
    public int distance = 8;
    public Text CrystalSumm;

    public Image audioOn_img;
    public Image audioOff_img;

    public int[] ress_price;
    public Text ress_txt;
    private int priceindex = 0;
    private bool ressurected_with_ads;
    public GameObject ress_ads_btn;
    public Text Level_txt;
    public Text levelUp_txt;
    public Text score_txt;
    public Text crystal_txt;
    public GameObject windowsStandalonePause;


    public GameObject[] unlockedAncestors;

    void Start()
    {
        // ress_ads_btn.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
        //ressurected_with_ads = false;
        pause_pnl.SetActive(false);
        blockingmask.SetActive(false);
        scoreboard.SetActive(true);
        deathpnl.SetActive(false);
        result_pnl.SetActive(false);

        Distance_txt.text = distance.ToString();

        audioOff_img.gameObject.SetActive(false);
        audioOn_img.gameObject.SetActive(true);
        CrystalSumm.text = LevelManager.Instance.GetMoney().ToString();
        EventManager.OnPlayerDied += PlayerDie;
        EventManager.OnPlayerResurrected += PlayerResurrect;
        SoundCheckOnStart();
    }
#if UNITY_ANDROID
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                GameManager.Instance.Player.Ressurect();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
#endif

    void Update()
    {
        distance_counter();
        CrystalSumm.text = GameManager.Instance.GetCrystals().ToString();
    }

    //pause_btn need to be cleaned
    public void Pause()
    {
        System.GC.Collect();
        deathpnl.SetActive(false);
#if UNITY_STANDALONE_WIN
          windowsStandalonePause.gameObject.SetActive(true);
#endif
        windowsStandalonePause.gameObject.SetActive(false);
        pause_pnl.SetActive(true);
        blockingmask.SetActive(false);
        scoreboard.SetActive(true);

        Time.timeScale = 0;

    }
    // turn of sound_btn
    public void AudioMute()
    {
        if (LevelManager.Instance.GetSoundCheck() == 0)
        {
            audioOn_img.gameObject.SetActive(false);
            audioOff_img.gameObject.SetActive(true);
            AudioListener.pause = true;
            LevelManager.Instance.SetSoundCkeck(+1);
        }
        else if (LevelManager.Instance.GetSoundCheck() == 1)
        {
            audioOff_img.gameObject.SetActive(false);
            audioOn_img.gameObject.SetActive(true);
            AudioListener.pause = false;
            LevelManager.Instance.SetSoundCkeck(-1);
        }
    }
    // resume after pause need to be cleaned
    public void Resume()
    {
        deathpnl.SetActive(false);
        pause_pnl.SetActive(false);
        blockingmask.SetActive(false);
        scoreboard.SetActive(true);

        Time.timeScale = 1;
#if UNITY_STANDALONE_WIN
        windowsStandalonePause.gameObject.SetActive(false);
#endif
#if UNITY_WSA
        windowsStandalonePause.gameObject.SetActive(false);
#endif
    }
    //redirecting to main menu
    public void Restart_to_menu()
    {
        System.GC.Collect();
        Time.timeScale = 1;
        Application.LoadLevel("StartMenu");
    }
    //test btn fortune wheel need to be cleaned
    public void Fortune()
    {
        deathpnl.SetActive(false);
        pause_pnl.SetActive(false);
        blockingmask.SetActive(true);
        scoreboard.SetActive(false);
    }

    // counting distanve for gameobject, gameobject need to be seted in menu
    private void distance_counter()
    {
        int newdistance;
        newdistance = distance + 1 * ((int)Player.transform.position.x);
        Distance_txt.text = newdistance.ToString() + " m";
    }

    // test_money mask can be replaced with prefab from shop
    //redirect link leads to special pnl in shop(if using other mask prefab add this to mask GetCrystal btn)
    public void get_crystals()
    {
        LevelManager.Instance.SetShopID(1);
        Application.LoadLevel("Shop");
    }

    // ress button for money ress no actuall ress is added only money part
    public void ressurect()
    {
        ress_txt.text = ress_price.ToString();

        if (LevelManager.Instance.GetMoney() >= ress_price[priceindex])
        {
            LevelManager.Instance.AddMoney(-ress_price[priceindex]);

            if (priceindex <= 1)
            {
                priceindex++;
            }
            print("a");
            if (priceindex <= 2)
            {
                ress_txt.text = ress_price[priceindex].ToString();
                print("b");
            }
            else
            {
                ress_txt.text = ress_price[2].ToString();
            }
            print(priceindex);
            Player.GetComponent<Player>().Ressurect();
            deathpnl.SetActive(false);
            scoreboard.SetActive(true);
        }
        else
        {
            ress_txt.text = ress_price[priceindex].ToString();
        }

    }
    // not currently used need to be added to unity ads script
    public void ressurect_video()
    {
        if (ressurected_with_ads == false)
        {
            //resscode
            ressurected_with_ads = true;
        }
        else
        {
            ress_ads_btn.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
            print("Hi");
        }
    }
    public void RestartGameOnDeath()
    {
        System.GC.Collect();
        Application.LoadLevel("Main");
    }

    public void RessurectWithAds()
    {
        deathpnl.SetActive(false);
#if UNITY_ANDROID
        ShowRewardedAd();
#endif
#if UNITY_WSA

        AdsManager.Instance.SubscribeForAdFinishEvent((arg) =>
        {
            GameManager.Instance.Player.Ressurect();
            deathpnl.SetActive(false);
            scoreboard.SetActive(true);
        }, this);
        AdsManager.Instance.ShowVideo();
#endif
    }
    public void closeRessWindow()
    {
        Result();
    }

    void Result()
    {
        scoreboard.SetActive(false);
        result_pnl.SetActive(true);
    }

    public void LoadTree()
    {
        Application.LoadLevel("TreeRevorked");
    }

    private bool hasSkippedAd = false;

    private void PlayerDie()
    {
        if (!hasSkippedAd && GameManager.Instance.Player.IsNoAdsResPassive)
        {
            GameManager.Instance.Player.Ressurect();
            hasSkippedAd = true;
            return;
        }
        if (!GameManager.Instance.HasRessurectedThisRun && AdsManager.Instance.IsAdReady)
        {
            deathpnl.SetActive(true);
        }
        else
        {
            result_pnl.SetActive(true);
        }
        scoreboard.SetActive(false);
    }

    private void PlayerResurrect()
    {
        deathpnl.SetActive(false);
        scoreboard.SetActive(true);
    }
    void SoundCheckOnStart()
    {
        if (LevelManager.Instance.GetSoundCheck() == 1)
        {
            audioOn_img.gameObject.SetActive(false);
            audioOff_img.gameObject.SetActive(true);
            AudioListener.pause = true;
        }
        else
        {
            audioOn_img.gameObject.SetActive(true);
            audioOff_img.gameObject.SetActive(false);
            AudioListener.pause = false;
        }
    }
}
