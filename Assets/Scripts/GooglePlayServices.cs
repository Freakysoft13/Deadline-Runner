using UnityEngine;
using System;
#if UNITY_ANDROID
using GooglePlayGames;
#endif
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour
{
#if UNITY_ANDROID
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

    void Start ()
    {
#if UNITY_ANDROID
        Login();
#endif
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
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        if (isLoggedIn)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Login(() =>
            {
                Social.ShowLeaderboardUI();
            });
        }
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
#endif
    }
