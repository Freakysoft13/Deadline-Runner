using UnityEngine;

public class Follower : MonoBehaviour {

	public Transform trackedObject;
    private Vector3 velocity = Vector3.zero;
    private float xAhead = 0.0f;

    void Start()
    {
        xAhead = trackedObject.position.x - transform.position.x;
    }

    void LateUpdate()
    {
        Vector3 futurePos = transform.position;
        futurePos.x = trackedObject.position.x - xAhead;
        transform.position = Vector3.SmoothDamp(transform.position, futurePos, ref velocity, 0.04f);
    }
}
