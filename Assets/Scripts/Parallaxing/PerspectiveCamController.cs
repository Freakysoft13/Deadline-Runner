using UnityEngine;
using System.Collections;

public class PerspectiveCamController : MonoBehaviour {

	// Use this for initialization
	void Update () {
        GetComponent<Camera>().orthographicSize = Camera.main.orthographicSize;
        Vector3 newPos = transform.position;
        newPos.y = Camera.main.transform.position.y;
        transform.position = newPos;
    }
}
