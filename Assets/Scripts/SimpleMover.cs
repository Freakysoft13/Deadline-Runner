using UnityEngine;

public class SimpleMover : MonoBehaviour
{

    public float speed;
    public bool stopIfDead = true;

    private float originalSpeed;

    void Start()
    {
        if (stopIfDead)
        {
            originalSpeed = speed;
            EventManager.OnBeforePlayerDied += () => { speed = 0; };
            EventManager.OnPlayerResurrected += () => { speed = originalSpeed; };
        }
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
