using UnityEngine;
using System.Collections;

public class NearbySoundPlayer : MonoBehaviour
{

    public float soundRange = 0.0f;
    private AudioSource source;
    private Transform player;
    private float a;
    private float b;
    private float startVolume;

    void Start()
    {
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        a = transform.position.x - soundRange;
        b = transform.position.x + soundRange;
        startVolume = source.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < soundRange && !source.isPlaying)
        {
            source.Play();
        }
        if (Mathf.Abs(player.position.x - transform.position.x) > soundRange && source.isPlaying)
        {
            source.Stop();
            source.volume = startVolume;
        }
        if (source.isPlaying)
        {
            source.volume = getCurrentVolume(player.position.x);
        }
    }

    private float getCurrentVolume(float x)
    {
        float result;
        float center = transform.position.x;
        float halfLen = 2 * soundRange / startVolume;
        if (x < center)
        {
            result = x - center + soundRange;
        }
        else
        {
            result = x - center - soundRange;
        }
        return Mathf.Abs(result / halfLen);
    }
}
