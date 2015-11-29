using UnityEngine;
using UnityEditor;
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
    private float lastEffectSpawnPointX;

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
            Spawn(minObstaclesSpread, maxObstaclesSpread, ref lastObstacleSpawnPointX, ConvertObstaclesEnum(),
                obstaclesDistribution, obstaclesPadding, "obstacle");

            yield return new WaitForSeconds(.2f);
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
            while (lastEffectSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            Spawn(minEffectsSpread, maxEffectsSpread, ref lastEffectSpawnPointX, ConvertEffectsEnum(), effectsDistribution, effectsPadding, "crystal");

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
        string objectToSpawnName = "";
        for (int i = 0; i < objectsDistribution.Length; i++) {
            if (rng < objectsDistribution[i]) {
                objectToSpawnName = ObjectTypesDataHolder.Instance.GetObjectNameForType(objectsToSpawn[i]);
                if (objectsPadding.Length == objectsToSpawn.Length) {
                    spread = spread < objectsPadding[i] ? objectsPadding[i] : spread;
                }
                break;
            }
        }
        float spawnPointX = lastSpawnPointX == 0 ? startPoint + spread + lastSpawnPointX : spread + lastSpawnPointX;
        lastSpawnPointX = spawnPointX;
        SpawnableObject objectToSpawn = pool.GetObject(objectToSpawnName).GetComponent<SpawnableObject>();
        RestoreScaleRecursively(objectToSpawn.transform);
        int sideIndicator = side == Side.UPPER ? 1 : -1;
        Vector2 spawnPosition = new Vector2(spawnPointX, sideIndicator * objectToSpawn.yPosition);
        Vector3 modifiedObjectScale = new Vector3(objectToSpawn.transform.localScale.x,
            sideIndicator * objectToSpawn.transform.localScale.y,
            objectToSpawn.transform.localScale.z);
        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.transform.localScale = modifiedObjectScale;
        /*float lastPoint = PlayerPrefs.GetFloat("lastPoint" + collisionTag, -1000);
        float lastSize = PlayerPrefs.GetFloat("lastSize" + collisionTag, -1000);
        if (lastPoint + lastSize / 2 > spawnPosition.x - 1) {
            spawnPosition.x += (lastPoint + lastSize / 2) - (spawnPosition.x - 1);
            print(((lastPoint + lastSize / 2) - (spawnPosition.x - 1)));
        }
        PlayerPrefs.SetFloat("lastPoint" + collisionTag, spawnPosition.x);
        PlayerPrefs.SetFloat("lastSize" + collisionTag, 2);*/
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