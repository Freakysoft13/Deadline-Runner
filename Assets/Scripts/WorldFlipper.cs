using UnityEngine;

public class WorldFlipper : MonoBehaviour {

    public int flipped = 1;
    public Transform topClouds;
    public Transform botClouds;
    public int chance = 50;

    private static WorldFlipper instance = null;

    public static WorldFlipper Instance {
        get { return instance; }
    }

    void Awake() {
        instance = this;
        if (Random.Range(0, 101) < chance) {
            Flip();
        }
    }

	public void Flip() {
        flipped *= -1;
        Vector3 topCloudsPos = topClouds.position;
        topClouds.position = botClouds.position;
        botClouds.position = topCloudsPos;
    }
}
