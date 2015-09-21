using UnityEngine;

public class CameraFollow : MonoBehaviour {

    /*public Transform trackedObject;
    public float smoothTime = 0.0f;

    private Vector3 velocity = Vector3.zero;
    private float xAhead = 0.0f;

    void Start()
    {
        xAhead = trackedObject.position.x - transform.position.x;
    }

    void LateUpdate()
    {
        float xSmoothPos = Mathf.SmoothDamp(transform.position.x, trackedObject.position.x, ref velocity.x, smoothTime);
        transform.position = new Vector3(xSmoothPos - 1.8f * xAhead * Time.deltaTime, 0, -10);
    }*/

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
 
     void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            destination.x += 12;
            destination.y = 0.0f;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
     }


}
