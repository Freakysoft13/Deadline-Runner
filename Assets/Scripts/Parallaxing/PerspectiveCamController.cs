using UnityEngine;
using System.Collections;

public class PerspectiveCamController : MonoBehaviour {

	// Use this for initialization
	void Update () {
        Vector3 newPos = transform.position;
        newPos.y = Camera.main.transform.position.y > 0 ? 2 : -1.8f;
        transform.position = newPos;
    }
}
