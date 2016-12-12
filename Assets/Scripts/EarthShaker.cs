using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class EarthShaker : MonoBehaviour
{
    public Transform[] shakeTargets;

    public float maxShakeDuration = 10;

    public static EarthShaker instance;

    private bool loopShake = false;

    public bool LoopShake { get { return loopShake; } set { loopShake = value; } }

    private Dictionary<string, bool> shakerStatus = new Dictionary<string, bool>();


    void Awake()
    {
        instance = this;
    }

    public void StartShaking(string shakerId, float magnitudeX, float magnitudeY, float duration)
    {
        StartCoroutine(Shake(shakerId, magnitudeX, magnitudeY, duration));
    }
    public void StartShaking(string shakerId, float magnitudeX, float magnitudeY)
    {
        SetShakerStatus(shakerId, true);
        StartCoroutine(Shake(shakerId, magnitudeX, magnitudeY, 0));
    }

    public void StopShaking(string shakerId)
    {
        SetShakerStatus(shakerId, false);
        //StartShaking(0.03f, 0.03f, 0.5f);
    }

    private void SetShakerStatus(string shakerId, bool isRunning)
    {
        if (shakerStatus.ContainsKey(shakerId))
        {
            shakerStatus[shakerId] = isRunning;
        }
    }

    IEnumerator Shake(string shakerId, float magnitudeX, float magnitudeY, float duration)
    {
        Vector3[] originalCamPos = new Vector3[shakeTargets.Length];
        float elapsed = 0.0f;
        bool loopShake = duration == 0;
        while ((duration != 0 && elapsed < duration) || (loopShake && elapsed < maxShakeDuration))
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            if (duration == 0) damper = 1;

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
            loopShake = shakerStatus[shakerId];
            yield return null;
        }
        for (int i = 0; i < shakeTargets.Length; i++)
        {
            shakeTargets[i].position = originalCamPos[i];
        }
    }
}
