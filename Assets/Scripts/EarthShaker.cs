using UnityEngine;

using System.Collections;

public class EarthShaker : MonoBehaviour
{
    public Transform[] shakeTargets;

    public static EarthShaker instance;

    void Start()
    {
        instance = this;
    }

    public void StartShaking(float magnitudeX, float magnitudeY, float duration)
    {
        StartCoroutine(Shake(magnitudeX, magnitudeY, duration));
    }

    IEnumerator Shake(float magnitudeX, float magnitudeY, float duration)
    {
        Vector3[] originalCamPos = new Vector3[shakeTargets.Length];
        float elapsed = 0.0f;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitudeX * damper;
            y *= magnitudeY * damper;

            for (int i = 0; i < shakeTargets.Length; i++)
            {
                originalCamPos[i] = shakeTargets[i].position;
                shakeTargets[i].position = new Vector3(x + shakeTargets[i].position.x,
                    Mathf.Clamp(y + shakeTargets[i].position.y, -2, 2), originalCamPos[i].z);
            }

            yield return null;
        }
        for (int i = 0; i < shakeTargets.Length; i++)
        {
            shakeTargets[i].position = originalCamPos[i];
        }
    }
}
