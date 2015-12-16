using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using UnityEngine.SocialPlatforms;

/*public class GPGController : MonoBehaviour {

    //achievements string list
    public string achievementNewbie = "CgkIobeXidUMEAIQAQ";
    public string achievementSprinter = "CgkIobeXidUMEAIQAg";
    public string achievementSomethingShiny = "CgkIobeXidUMEAIQAw";
    public string achievementGemology = "CgkIobeXidUMEAIQBA";
    public string achievementIFeelThePower = "CgkIobeXidUMEAIQBQ";
    public string achievementAddict = "CgkIobeXidUMEAIQCA";
    public string achievementFanClub = "CgkIobeXidUMEAIQCQ";
    public string achievementRunner = "CgkIobeXidUMEAIQCg";
    public string achievementLongDistanceRunner = "CgkIobeXidUMEAIQCw";
    public string achievementJeweler = "CgkIobeXidUMEAIQDA";
    public string achievementPowerSurge = "CgkIobeXidUMEAIQDQ";
    public string achievementOverpowered = "CgkIobeXidUMEAIQDg";
    public string achievementMonsterSlayer = "CgkIobeXidUMEAIQDw";
    public string leaderboardBestScore = "CgkIobeXidUMEAIQBg";
    public string leaderboardDistance = "CgkIobeXidUMEAIQBw";



    void Start ()
    {
        PlayGamesPlatform.Activate();
    }
	
	private void GPlusLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                print("You sucessfully Logged In");
            }
            else
            {
               print("Something went wrong");
            }
        });
    }
    private void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
    private void ShowLadder()
    {
        Social.ShowLeaderboardUI();
    }

    public void GPLadder()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ShowLeaderboardUI();
            }
            else
            {
                print("Something went wrong");
            }
        });
    }
    public void GPAchieves()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Social.ShowAchievementsUI();
            }
            else
            {
                print("Something went wrong");
            }
        });
    }
}
*/