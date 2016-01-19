using UnityEngine;

public class ParallaxMove : MonoBehaviour
{
    public float speedScale;

    private float startSpeed = 0;
    private Player player;
 
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        startSpeed = speedScale * player.GetSpeed();
        EventManager.Instance.OnBeforePlayerDied += () => { speedScale = 0; };
        EventManager.Instance.OnPlayerResurrected += () => { speedScale = startSpeed; };
    }

    void Update() {
        transform.Translate(Vector2.right * speedScale * player.GetSpeed() * Time.deltaTime);
    }
}
