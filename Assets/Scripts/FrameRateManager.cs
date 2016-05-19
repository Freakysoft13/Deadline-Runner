using UnityEngine;
using System.Collections;

public class FrameRateManager : MonoBehaviour {
    public int target = 60;
    // Use this for initialization
    void Start () {
        Application.targetFrameRate = target;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target != Application.targetFrameRate)
        {
            Application.targetFrameRate = target;
        }
    }
}
