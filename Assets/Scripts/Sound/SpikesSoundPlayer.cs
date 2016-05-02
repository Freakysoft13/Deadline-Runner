using UnityEngine;

public class SpikesSoundPlayer : MonoBehaviour
{
    private const float DELAY = 2f;
    private const float OVERALL_ANIMATION = 4;

    private AudioSource source;
    private float timeScale = 1.0f;

    private float playbackTime = 0.0f;
	private Transform player;

    void Start()
    {
        source = GetComponent<AudioSource>();
        timeScale = GetComponent<SkeletonAnimation>().timeScale;
		playbackTime = Time.time;
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if ((Time.time - playbackTime > DELAY * timeScale) && Mathf.Abs(transform.position.x - player.position.x) < 7)
        {
			playbackTime = Time.time + DELAY;
			source.Play();
        }
    }
}
