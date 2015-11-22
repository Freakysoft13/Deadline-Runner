using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float desiredScreenWidth = 1920.0f;
    public int currentPixelToUnits = 100;

    private float xDelta;

    void Start() {
        xDelta = Mathf.Abs(transform.position.x - target.position.x);
        GetComponent<Camera>().orthographicSize = (desiredScreenWidth / Screen.width * Screen.height / 2) / currentPixelToUnits;
    }

    void Update() {
        if (target) {
            Vector3 newPos = transform.position;
            newPos.x = target.position.x + xDelta;
            newPos.y = GameManager.Instance.Player.PlayerFlip * Mathf.Abs(newPos.y);
            transform.position = newPos;
        }
    }
}
