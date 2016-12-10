using UnityEngine;
using Side = GameManager.Side;

public class FlyingObstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public float distanceToPlayer = 10.0f;
    public float distanceFromPlayer = 30.0f;

    public float minRespawnDelay = 2.0f;
    public float maxRespawnDelay = 10.0f;

    public float startSpawnDistance = 200.0f;
    public float hardModeDistance = 1000.0f;

    public float threshold = 2000.0f;

    private Player player;
    private bool isWarningShown = false;
    private Side side = Side.NONE;
    private bool shouldSpawn = false;
    private bool shouldRespawn = true;

    // Use this for initialization
    void Start()
    {
        player = GameManager.Instance.Player;
        Invoke("Spawn", Mathf.Clamp(threshold / (player.GetDistance() + 1), minRespawnDelay, maxRespawnDelay));
    }


    private void Spawn()
    {
        if (player.GetDistance() < startSpawnDistance || !canRespawn())
        {
            Invoke("Spawn", Mathf.Clamp(threshold / (player.GetDistance() + 1), minRespawnDelay, maxRespawnDelay));
            return;
        }
        Vector3 pos = transform.position;
        if (side.Equals(Side.NONE) || hardModeDistance < player.GetDistance())
        {
            side = player.Side;
        }
        else
        {
            side = side.Equals(Side.UPPER) ? Side.BOTTOM : Side.UPPER;
        }
        if (side.Equals(Side.UPPER))
        {
            pos.y = 2;
        }
        else
        {
            pos.y = -7;
        }
        pos.x = player.GetDistance() + distanceFromPlayer;
        transform.position = pos;
        if (!isWarningShown)
        {
            EventManager.FireObstacleWarning(true, side);
            isWarningShown = true;
        }
        Invoke("Spawn", Mathf.Clamp(threshold / (player.GetDistance() + 1), minRespawnDelay, maxRespawnDelay));
    }

    void Update()
    {
        transform.Translate(speed * Vector2.left * Time.deltaTime);
        if (shouldDisableWarning() && isWarningShown)
        {
            EventManager.FireObstacleWarning(false, side);
            EarthShaker.instance.StartShaking(0.04f, 0.04f, 2f); // SHAKE!
            isWarningShown = false;
        }
    }

    private bool shouldDisableWarning()
    {
        return transform.position.x - player.transform.position.x < distanceToPlayer;
    }
    private bool canRespawn()
    {
        return player.transform.position.x - transform.position.x > distanceToPlayer;
    }
}
