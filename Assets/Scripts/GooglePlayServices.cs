#if UNITY_ANDROID
using UnityEngine;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour
{

    private bool isLoggedIn = false;

    public bool IsLoggedIn { get { return isLoggedIn; } }


    private static GooglePlayServices instance = null;

    public static GooglePlayServices Instance
    {
        get { return instance; }
    }
    // Use this for initialization

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
            return;
        }
        if (!isLoggedIn)
        {
            Login();
            PlayGamesPlatform.Activate();
        }
    }

    public void Login(Action callback = null)
    {
        if (isLoggedIn)
        {
            if (callback != null)
            {
                callback();
            }
            return;
        }
        Social.localUser.Authenticate((bool success) =>
        {
            isLoggedIn = success;
            if (success)
            {
                print("Login Succes");
            }
            else
            {
                print("login failed");
            }
        });
    }

    public void ReportScore(String leaderboard, long score)
    {
        if (isLoggedIn)
        {
            Social.ReportScore(score, leaderboard, null);
        }
    }

    public void ReportProgress(String achievementID, float progress)
    {
        if (isLoggedIn)
        {
            Social.ReportProgress(achievementID, progress, null);
        }
    }

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLadder()
    {
        Social.ShowLeaderboardUI();
    }

}
#endif