#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GPG_UI_Controller : MonoBehaviour {

    public GameObject gpServicesPnl;

    void Start()
    {
        PlayGamesPlatform.Activate();
        gpServicesPnl.SetActive(false);
    }


    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void GPServicesBtnController()
    {
        if (gpServicesPnl.activeInHierarchy)
        {
            gpServicesPnl.SetActive(false);
        }
        else
            gpServicesPnl.SetActive(true);
    }
}
#endif
