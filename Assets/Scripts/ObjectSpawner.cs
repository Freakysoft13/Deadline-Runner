using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{

    public ObstacleSpawnSide side;
    
    private Transform player;

    private float lastXPos;
    private float lastObjectSpawnPos;
    private int nextPadding = 0;
    private bool isSpawnPaused = false;
    private int sideIndicator;

    public float minSpread = 5.0f;
    public float maxSpread = 15.0f;
    public float startSpawnDistance = 10.0f;
    public bool shouldSyncSpawners = false;

    public int[] probabilities;

    protected float yPos;
    protected float lastUpdateTime;
    protected float spread;

    private string LAST_SPAWN_POS_KEY = "lastSpawnPos";
    private string IS_SPAWN_PAUSED_KEY = "isSpawnPaused";
    private string PAUSED_BY_SIDE_KEY = "pausedBySide";

    private ObjectPool objectPool;
    
    void Awake()
    {
        string extraKey = GetObjectTypes()[0].ToString();
        LAST_SPAWN_POS_KEY += extraKey;
        IS_SPAWN_PAUSED_KEY += extraKey;
        PAUSED_BY_SIDE_KEY += extraKey;
        PlayerPrefs.DeleteKey(LAST_SPAWN_POS_KEY);
        PlayerPrefs.DeleteKey(IS_SPAWN_PAUSED_KEY);
        PlayerPrefs.DeleteKey(PAUSED_BY_SIDE_KEY);
    }

    protected virtual void Start()
    {
        objectPool = ObjectPool.Instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spread = Random.Range(minSpread, maxSpread);
        lastXPos = Mathf.NegativeInfinity;
        lastUpdateTime = Time.time;
        if (side.Equals(ObstacleSpawnSide.UPPER))
        {
            sideIndicator = 1;
        }
        else
        {
            sideIndicator = -1;
        }
    }
    
    protected virtual void Update()
    {
        SpawnObjects();
    }

    protected void SpawnObjects()
    {
        isSpawnPaused = PlayerPrefs.GetInt(IS_SPAWN_PAUSED_KEY, 0) == 1;
        if (isSpawnPaused)
        {
            isSpawnPaused = PlayerPrefs.GetInt(IS_SPAWN_PAUSED_KEY, 0) == 1 || side.ToString().Equals(PlayerPrefs.GetString(PAUSED_BY_SIDE_KEY, side.ToString()));
            if (!(lastObjectSpawnPos - Mathf.Abs(player.position.x) > 2 * startSpawnDistance))
            {
                PlayerPrefs.SetInt(IS_SPAWN_PAUSED_KEY, 0);
            }
        }
        if ((Mathf.Abs(player.position.x - lastXPos) > (spread / 2)) && !isSpawnPaused)
        {
            System.Enum[] objectTypes = GetObjectTypes();
            int random = Random.Range(0, 100);
            int objectIndex = 0;
            for (int i = 0; i < probabilities.Length; i ++) {
                if(random < probabilities[i]) {
                    objectIndex = i;
                    break;
                }
            }
            int currentLeft = GetObjectLeftPaddings()[objectIndex] + nextPadding;
            float startOffset = lastObjectSpawnPos == 0 ? startSpawnDistance : 0;
            float nextPosX = lastObjectSpawnPos + spread + startOffset + currentLeft;
            GameObject objectToSpawn = objectPool.GetObject(ObjectTypesDataHolder.Instance.GetObjectNameForType(objectTypes[objectIndex]));
            if(objectToSpawn == null) {
                print(ObjectTypesDataHolder.Instance.GetObjectNameForType(objectTypes[objectIndex]));
            }
            Collider2D collider = objectToSpawn.GetComponent<Collider2D>();
            float prevObjectMinX = collider.bounds.min.x;
            float prevObjectMaxX = collider.bounds.max.x;
            float halfLength = ((prevObjectMaxX - prevObjectMinX) / 2.0f);

            if (shouldSyncSpawners)
            {

                float lastOverallSpawnPos = PlayerPrefs.GetFloat(LAST_SPAWN_POS_KEY, 0);
                if (AreObjectsOverlapping(nextPosX, 1.4f, objectToSpawn.tag))
                {
                    nextPosX = lastOverallSpawnPos + spread + startOffset + currentLeft;
                }

                if (nextPosX > lastOverallSpawnPos)
                {

                    PlayerPrefs.SetFloat(LAST_SPAWN_POS_KEY, nextPosX);
                }
            }

            objectToSpawn.transform.position = new Vector2(nextPosX, objectToSpawn.GetComponent<SpawnableObject>().yPosition * sideIndicator);
            objectToSpawn.transform.localScale = new Vector3(objectToSpawn.transform.localScale.x, sideIndicator * Mathf.Abs(objectToSpawn.transform.localScale.y), objectToSpawn.transform.localScale.z);
            
            lastXPos = player.position.x;
            spread = Random.Range(minSpread, maxSpread);

            //required padding: if distance between two obstacles is 0 then they should spawn side by side and not one on another.
            lastObjectSpawnPos = nextPosX + halfLength;
            nextPadding = GetObjectRightPaddings()[objectIndex];
            if (lastObjectSpawnPos - Mathf.Abs(player.position.x) > 2 * startSpawnDistance)
            {
                PlayerPrefs.SetString(PAUSED_BY_SIDE_KEY, side.ToString());
                PlayerPrefs.SetInt(IS_SPAWN_PAUSED_KEY, 1);
            }
        }
    }

    private bool AreObjectsOverlapping(float position, float radius, string tag)
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

    protected abstract System.Enum[] GetObjectTypes();
    protected abstract int[] GetObjectLeftPaddings();
    protected abstract int[] GetObjectRightPaddings();

    public enum ObstacleSpawnSide
    {
        UPPER, BOTTOM
    }
}