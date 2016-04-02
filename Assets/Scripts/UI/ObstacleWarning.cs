using UnityEngine;

public class ObstacleWarning : MonoBehaviour
{
    public GameObject warningImg;

    public GameManager.Side side;

    void Start()
    {
        EventManager.OnObstacleWarning += (show, side) =>
        {
            if (!this.side.Equals(side)) { return; }
            warningImg.SetActive(show);
        };
    }
}
