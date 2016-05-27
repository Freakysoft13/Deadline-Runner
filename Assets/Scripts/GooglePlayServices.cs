#if UNITY_ANDROID
using UnityEngine;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour
{
    private bool isLoggedIn = false;


    private static GooglePlayServices instance = null;

    public static GooglePlayServices Instance
    {
        get { return instance; }
    }
    // Use this for initialization
    void Awake()
    {
        PlayGamesPlatform.Activate();
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
        Login();
    }

    public void Login(Action callback = null)
    {
        Social.localUser.Authenticate((bool success) =>
        {
            isLoggedIn = success;
            if (callback != null)
            {
                callback();
            }
        });
    }

    public void ShowAchievements()
    {
            Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
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
}
#endif