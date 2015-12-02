using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;

    [Header("Ortographic configuration")]
    public float desiredScreenWidth = 1920.0f;
    public float desiredScreenHeight = 1200.0f;
    public int currentPixelToUnits = 100;

    [Header("Shake Configuration")]
    public float duration = 3.0f;
    public float magnitudeX = 0.2f;
    public float magnitudeY = 0.2f;

    public float xDelta = 2.0f;

    void Start() {
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

    IEnumerator Shake() {
        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration) {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitudeX * damper;
            y *= magnitudeY * damper;

            Camera.main.transform.position = new Vector3(x + Camera.main.transform.position.x,
                Mathf.Clamp(y + Camera.main.transform.position.y, -2, 2), originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}
