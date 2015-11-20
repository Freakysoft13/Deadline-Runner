using UnityEngine;
using System.Collections;

public class EnvironmentSpawner : MonoBehaviour
{
    public float distanceFromPlayer = 20.0f;
    public float maxSpawnDistance = 50;

    public Side side;

    [Header("Obstacles")]
    public ObjectTypesDataHolder.ObstacleType[] obstaclesToSpawn;
    public int[] obstaclesDistribution;
    public float[] obstaclesPadding;
    public float minObstaclesSpread;
    public float maxObstaclesSpread;
    private float lastObstacleSpawnPointX;

    [Header("Crystals")]
    public ObjectTypesDataHolder.CrystalType[] crystalsToSpawn;
    public int[] crystalDistribution;
    public float[] crystalPadding;
    public float minCrystalsSpread;
    public float maxCrystalsSpread;
    private float lastCrystalSpawnPointX;

    [Header("Effects")]
    public LevelManager.PowerUp[] effectsToSpawn;
    public int[] effectsDistribution;
    public float[] effectsPadding;
    public float minEffectsSpread;
    public float maxEffectsSpread;

    private ObjectPool pool;
    private Transform player;
    private float startPoint;

    void Start() {
        pool = ObjectPool.Instance;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPoint = player.position.x + distanceFromPlayer;
        StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnCrystals());
        StartCoroutine(SpawnEffects());
    }

    private IEnumerator SpawnObstacles() {
        while (true) {
            while (lastObstacleSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            Spawn(minObstaclesSpread, maxObstaclesSpread, ref lastObstacleSpawnPointX, ConvertObstaclesEnum(), obstaclesDistribution, obstaclesPadding, "obstacle");

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpawnCrystals() {
        while (true) {
            while (lastCrystalSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            Spawn(minCrystalsSpread, maxCrystalsSpread, ref lastCrystalSpawnPointX, ConvertCrystalsEnum(), crystalDistribution, crystalPadding, "crystal");

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpawnEffects() {
        while (true) {
            while (lastCrystalSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            Spawn(minEffectsSpread, maxEffectsSpread, ref lastCrystalSpawnPointX, ConvertEffectsEnum(), effectsDistribution, effectsPadding, "crystal");

            yield return new WaitForSeconds(1f);
        }
    }

    private System.Enum[] ConvertObstaclesEnum() {
        System.Enum[] tmpEnum = new System.Enum[obstaclesToSpawn.Length];
        for (int i = 0; i < obstaclesToSpawn.Length; i++) {
            tmpEnum[i] = obstaclesToSpawn[i];
        }
        return tmpEnum;
    }

    private System.Enum[] ConvertEffectsEnum() {
        System.Enum[] tmpEnum = new System.Enum[effectsToSpawn.Length];
        for (int i = 0; i < effectsToSpawn.Length; i++) {
            tmpEnum[i] = effectsToSpawn[i];
        }
        return tmpEnum;
    }

    private System.Enum[] ConvertCrystalsEnum() {
        System.Enum[] tmpEnum = new System.Enum[crystalsToSpawn.Length];
        for (int i = 0; i < crystalsToSpawn.Length; i++) {
            tmpEnum[i] = crystalsToSpawn[i];
        }
        return tmpEnum;
    }

    private void Spawn(float minSpread, float maxSpread, ref float lastSpawnPointX, System.Enum[] objectsToSpawn, int[] objectsDistribution, float[] objectsPadding, string collisionTag) {
        int rng = Random.Range(0, 100);
        float spread = Random.Range(minSpread, maxSpread);
        float spawnPointX = lastSpawnPointX == 0 ? startPoint + spread + lastSpawnPointX : spread + lastSpawnPointX;
        lastSpawnPointX = spawnPointX;
        if (IsOverlappingWithOtherObjects(spawnPointX, collisionTag)) {
            return;
        }
        string objectToSpawnName = "";
        for (int i = 0; i < objectsDistribution.Length; i++) {
            if (rng < objectsDistribution[i]) {
                objectToSpawnName = ObjectTypesDataHolder.Instance.GetObjectNameForType(objectsToSpawn[i]);
                if (objectsPadding.Length == objectsDistribution.Length) {
                    spread = spread < objectsPadding[i] ? objectsPadding[i] : spread;
                }
                break;
            }
        }
        SpawnableObject objectToSpawn = pool.GetObject(objectToSpawnName, false).GetComponent<SpawnableObject>();
        RestoreScaleRecursively(objectToSpawn.transform);
        int sideIndicator = side == Side.UPPER ? 1 : -1;
        Vector2 spawnPosition = new Vector2(spawnPointX, sideIndicator * objectToSpawn.yPosition);
        Vector3 modifiedObjectScale = new Vector3(objectToSpawn.transform.localScale.x,
            sideIndicator * objectToSpawn.transform.localScale.y,
            objectToSpawn.transform.localScale.z);
        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.transform.localScale = modifiedObjectScale;
    }

    private bool IsOverlappingWithOtherObjects( float spawnPointX, string collisionTag) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(spawnPointX, 0), 2f);
        foreach (Collider2D collider in colliders) {
            if (collider.CompareTag(collisionTag)) {
                return true;
            }
        }
        return false;
    }

    private void RestoreScaleRecursively(Transform t) {
        foreach (Transform child in t) {
            RestoreScaleRecursively(child);
        }
        Vector3 restoredScale = new Vector3(Mathf.Abs(t.localScale.x), Mathf.Abs(t.localScale.y), Mathf.Abs(t.localScale.z));
        t.localScale = restoredScale;
    }

    public enum Side
    {
        UPPER, BOTTOM
    }
}