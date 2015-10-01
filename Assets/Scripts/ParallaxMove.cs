using UnityEngine;

public class ParallaxMove : MonoBehaviour
{

    public float speedScale = 0.9f;

    private float speed;
    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = player.GetComponent<Player>().speed;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * speedScale * Time.deltaTime);
    }
}
