using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Transform[] shakeTargets;

    [Header("Ortographic configuration")]
    public float desiredScreenWidth = 1920.0f;
    public float desiredScreenHeight = 1200.0f;
    public int currentPixelToUnits = 100;

    [Header("Follow Configuration")]
    public float xDelta = 2.0f;

    void Start()
    {
        GetComponent<Camera>().orthographicSize = (desiredScreenWidth / Screen.width * Screen.height / 2) / currentPixelToUnits;
        EventManager.OnAfterlifeToggled += (isEnabled) =>
        {
            if (GetComponent<Grayscale>() != null)
            {
                GetComponent<Grayscale>().enabled = isEnabled;
            }
        };
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        if (target)
        {
            newPos.x = target.position.x + xDelta;
        }
        newPos.y = GameManager.Instance.Player.PlayerFlip * Mathf.Abs(newPos.y);
        transform.position = newPos;
    }
}
