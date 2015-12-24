using UnityEngine;

public class ParallaxMove : MonoBehaviour
{
    public float speed;

    private float startSpeed = 0;
 
    void Start()
    {
        startSpeed = speed;
        EventManager.Instance.OnBeforePlayerDied += () => { speed = 0; };
        EventManager.Instance.OnPlayerResurrected += () => { speed = startSpeed; };
    }

    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
