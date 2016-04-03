using UnityEngine;
using Side = GameManager.Side;

public class FlyingObstacle : MonoBehaviour
{
    public float speed = 10.0f;
    public float distanceToPlayer = 10.0f;
    public float distanceFromPlayer = 30.0f;

    public float startSpawnDistance = 200.0f;

    private Player player;
    private bool isWarningShown = false;
    private Side side;
    private bool shouldSpawn = false;
    private bool shouldRespawn = true;

    // Use this for initialization
    void Start()
    {
        player = GameManager.Instance.Player;
    }


    private void Spawn()
    {
        if (player.transform.position.x < startSpawnDistance) { return; }
        Vector3 pos = transform.position;
        if (player.Side.Equals(Side.UPPER))
        {
            pos.y = 2;
        }
        else
        {
            pos.y = -7;
        }
        side = player.Side;
        pos.x = player.transform.position.x + distanceFromPlayer;
        transform.position = pos;
        if (!isWarningShown)
        {
            EventManager.FireObstacleWarning(true, side);
            isWarningShown = true;
        }
    }

    void Update()
    {
        transform.Translate(speed * Vector2.left * Time.deltaTime);
        if (shouldDisableWarning() && isWarningShown)
        {
            EventManager.FireObstacleWarning(false, side);
            isWarningShown = false;
        }
        if (shouldSpawn)
        {
            Spawn();
            shouldSpawn = false;
            shouldRespawn = true;
        }
        else if (player.transform.position.x - transform.position.x > distanceFromPlayer && shouldRespawn)
        {
            shouldRespawn = false;
            Invoke("AttemptRespawn", 2.0f);
        }
    }

    private void AttemptRespawn()
    {
        float rng = Random.Range(0, 100);
        float canonicalSpawnChance = 10.0f;
        if (rng < Mathf.Clamp(canonicalSpawnChance * (player.GetDistance() / 100.0f), canonicalSpawnChance, 50.0f))
        {
            shouldSpawn = true;
        }
        else
        {
            Invoke("AttemptRespawn", 2.0f);
        }
    }

    private bool shouldDisableWarning()
    {
        return transform.position.x - player.transform.position.x < distanceToPlayer;
    }
}
