using UnityEngine;

public class ParallaxMove : MonoBehaviour
{
    public float speedScale;
    public float yOffset = 0;

    private float startSpeed = 0;
    private Player player;
 
    void Awake() {
        startSpeed = speedScale;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        EventManager.OnBeforePlayerDied += () => { speedScale = 0; };
        EventManager.OnPlayerResurrected += () => { speedScale = startSpeed; };
    }

    void Update() {
        transform.Translate(Vector2.right * speedScale * player.GetSpeed() * Time.deltaTime);
    }
}
