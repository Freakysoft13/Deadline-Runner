using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleSpawnSide side;

    //spawn parameters
    public ObjectTypesDataHolder.ObstacleType[] obstacleTypes;
    public int[] obstaclePaddings;
    public float minSpread = 5.0f;
    public float maxSpread = 15.0f;
    public float startSpawnDistance = 10.0f;

    public bool shouldSyncSpawners = false;
    private const string LAST_SPAWN_POS_KEY = "lastSpawnPos";
    private const string IS_SPAWN_PAUSED_KEY = "isSpawnPaused";
    private const string PAUSED_BY_SIDE_KEY = "pausedBySide";

    //difficulty increase
    public float maxSpreadDecRate = 1.0f;
    public float minSpreadAmpl = 2.0f;
    public float decTime = 10.0f;

    private float spread;
    private float lastXPos;
    private float lastObstacleSpawnPos;
    private int nextPadding = 0;
    private float lastUpdateTime;
    private bool isSpawnPaused = false;

    private float yPos;
    private float scale;
    private Transform player;

    //crystals. MAKE INDIVIDUAL MODULE FOR CRYSTALS YOU LAZY ASS. just not now :)
    public ObjectTypesDataHolder.CrystalType[] crystalTypes;
    public float minCrystalSpawnSpread = 30.0f;
    public float maxCrystalSpawnSpread = 100.0f;
    
    private float lastCrystalSpawnPos;
    private float crystalSpread;

    private ObjectPool objectPool;

    static ObstacleSpawner()
    {
        PlayerPrefs.DeleteKey(LAST_SPAWN_POS_KEY);
        PlayerPrefs.DeleteKey(IS_SPAWN_PAUSED_KEY);
        PlayerPrefs.DeleteKey(PAUSED_BY_SIDE_KEY);
    }

    void Start()
    {
        objectPool = ObjectPool.Instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spread = Random.Range(minSpread, maxSpread);
        crystalSpread = Random.Range(minCrystalSpawnSpread, maxCrystalSpawnSpread);
        lastXPos = Mathf.NegativeInfinity;
        lastCrystalSpawnPos = Mathf.NegativeInfinity;
        lastUpdateTime = Time.time;
        if(side.Equals(ObstacleSpawnSide.UPPER))
        {
            yPos = 0.75f;
            scale = 1;
        } else
        {
            yPos = -0.75f;
            scale = -1;
        }
    }

    void Update()
    {
        SpawnObstacles();
        SpawnCrystals();
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty()
    {
        if (Time.time - lastUpdateTime > decTime && (spread - maxSpreadDecRate) > minSpread + minSpreadAmpl)
        {
            spread -= maxSpreadDecRate;
            lastUpdateTime = Time.time;
        }
    }

    private void SpawnCrystals()
    {
        if((Mathf.Abs(player.position.x - lastCrystalSpawnPos) > crystalSpread) && side == ObstacleSpawnSide.UPPER)
        {
            GameObject crystal = objectPool.GetObject(ObjectTypesDataHolder.Instance.GetCrystalNameForType(crystalTypes[0]));
            crystal.transform.position = new Vector3(player.position.x + 40.0f, 5.0f);
            lastCrystalSpawnPos = player.position.x;
            crystalSpread = Random.Range(minCrystalSpawnSpread, maxCrystalSpawnSpread);
        }
    }

    private void SpawnObstacles()
    {
        isSpawnPaused = PlayerPrefs.GetInt(IS_SPAWN_PAUSED_KEY, 0) == 1;
        if (isSpawnPaused)
        {
            isSpawnPaused = PlayerPrefs.GetInt(IS_SPAWN_PAUSED_KEY, 0) == 1 || side.ToString().Equals(PlayerPrefs.GetString(PAUSED_BY_SIDE_KEY, side.ToString()));
            if (!(lastObstacleSpawnPos - Mathf.Abs(player.position.x) > 5 * startSpawnDistance))
            {
                PlayerPrefs.SetInt(IS_SPAWN_PAUSED_KEY, 0);
            }
        }
        if ((Mathf.Abs(player.position.x - lastXPos) > spread) && !isSpawnPaused)
        {
            int obstacleIndex = Random.Range(0, obstacleTypes.Length);
            int currentPadding = obstaclePaddings[obstacleIndex] + nextPadding;
            float startOffset = lastObstacleSpawnPos == 0 ? startSpawnDistance : 0;
            float nextPosX = lastObstacleSpawnPos + spread + startOffset + currentPadding;
            ObjectTypesDataHolder.ObstacleType type = obstacleTypes[obstacleIndex];
            GameObject obstacle = objectPool.GetObject(ObjectTypesDataHolder.Instance.GetObstacleNameForType(obstacleTypes[obstacleIndex]));
            EdgeCollider2D edgeCol = obstacle.GetComponent<EdgeCollider2D>();
            float prevObstacleMinX = edgeCol.bounds.min.x;
            float prevObstacleMaxX = edgeCol.bounds.max.x;
            float halfLength = ((prevObstacleMaxX - prevObstacleMinX) / 2.0f);
            if (shouldSyncSpawners)
            {
                float lastOverallSpawnPos = PlayerPrefs.GetFloat(LAST_SPAWN_POS_KEY, 0);
                if (AreObstaclesOverlapping(nextPosX, 1.4f))
                {
                    nextPosX = lastOverallSpawnPos + spread + startOffset + currentPadding;
                }
                if (nextPosX > lastOverallSpawnPos)
                {
                    PlayerPrefs.SetFloat(LAST_SPAWN_POS_KEY, nextPosX);
                }
            }
            obstacle.transform.position = new Vector2(nextPosX, type == ObjectTypesDataHolder.ObstacleType.AXE ? 0 : yPos);
            obstacle.transform.localScale = new Vector3(obstacle.transform.localScale.x, scale * Mathf.Abs(obstacle.transform.localScale.y), obstacle.transform.localScale.z);
            spread = Random.Range(minSpread, maxSpread);
            lastXPos = player.position.x;
            //required padding: if distance between two obstacles is 0 then they should spawn side by side and not one on another.
            lastObstacleSpawnPos = nextPosX + halfLength;
            nextPadding = currentPadding;
            if (lastObstacleSpawnPos - Mathf.Abs(player.position.x) > startSpawnDistance)
            {
                PlayerPrefs.SetString(PAUSED_BY_SIDE_KEY, side.ToString());
                PlayerPrefs.SetInt(IS_SPAWN_PAUSED_KEY, 1);
            }
        }
    }
    /* delete in the future JUST NOT NOW!
    private float CorrectXPos(float position)
    {
        float futurePosition = position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(position, 0), 1.2f);        
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("obstacle"))
            {
                EdgeCollider2D edgeCol = col as EdgeCollider2D;
                float prevObstacleMinX = edgeCol.bounds.min.x;
                float prevObstacleMaxX = edgeCol.bounds.max.x;
                float halfLength = ((prevObstacleMaxX - prevObstacleMinX) / 2.0f);
                int middlePos = edgeCol.pointCount / 2;
                float distance = Vector2.Distance(new Vector2(edgeCol.bounds.center.x, 0), new Vector2(position, 0));
                float leftDiff = position - prevObstacleMinX;
                float rightDiff = prevObstacleMaxX - position;
                if (leftDiff < rightDiff)
                {
                    futurePosition = position - (2 * halfLength - distance);
                }
                else
                {
                    futurePosition = position + (2 * halfLength - distance);
                }
            }
        }

        return futurePosition;
    }*/

    private bool AreObstaclesOverlapping(float position, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(position, 0), radius + 0.2f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("obstacle"))
            {
                return true;
            }
        }
        return false;
    }

    public enum ObstacleSpawnSide
    {
        UPPER, BOTTOM
    }
}
