using UnityEngine;
using System.Collections;

public class PlayServicesBridge : MonoBehaviour
{
#if UNITY_ANDROID
    public void ShowLeaderBoard()
    {
        GooglePlayServices.Instance.ShowLadder();
    }
    public void ShowAchievements()
    {
        GooglePlayServices.Instance.ShowAchievements();
    }
#endif
}
