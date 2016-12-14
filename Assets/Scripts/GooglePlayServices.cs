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
            print("Dont destroy");
        }
        else
        {
            DestroyImmediate(this);
            print("Destroy Immediate");
            return;
        }
        if (!isLoggedIn)
        {
            print("Not logged in. Logging in ...");
            Login();
            PlayGamesPlatform.Activate();
        }
        else
        {
            print("Already logged in.");
        }
    }

    public void Login(Action callback = null)
    {
        if (isLoggedIn)
        {
            print("Called login when user is authenticated");
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
            print("Reporting score. Start");
            Social.ReportScore(score, leaderboard, null);
            print("Reporting score. End");
        }
        else
        {
            print("Reporting score failure");
        }
    }

    public void ReportProgress(String achievementID, float progress)
    {
        if (isLoggedIn)
        {
            print("Reporting progress. Start");
            Social.ReportProgress(achievementID, progress, null);
            print("Reporting progress. End");
        }
        else
        {
            print("Reporting progress failure");
        }
    }

    public void ShowAchievements()
    {
        print("Show achievements. Start");
        Social.ShowAchievementsUI();
        print("Show achievements. End");
    }

    public void ShowLadder()
    {
        print("Show ladder. Start");
        Social.ShowLeaderboardUI();
        print("Show ladder. End");
    }

}
#endif