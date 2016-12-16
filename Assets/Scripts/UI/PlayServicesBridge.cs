using UnityEngine;
using System.Collections;

public class PlayServicesBridge : MonoBehaviour
{

    public void ShowLeaderBoard()
    {
        GooglePlayServices.Instance.ShowLadder();
    }
    public void ShowAchievements()
    {
        GooglePlayServices.Instance.ShowAchievements();
    }
}
