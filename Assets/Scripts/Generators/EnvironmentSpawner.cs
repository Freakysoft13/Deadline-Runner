using UnityEngine;
using System.Collections;
using Side = GameManager.Side;

public class EnvironmentSpawner : MonoBehaviour
{
    public float distanceFromPlayer = 20.0f;
    public float maxSpawnDistance = 50;

    public Side side;
    public Transform player;

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

    [Header("Difficulty")]
    public float amount = 0.2f;
    public float interval = 5f;

    private ObjectPool pool;
    private float startPoint;

    void Start() {
        pool = ObjectPool.Instance;
        startPoint = player.position.x + distanceFromPlayer;
        StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnCrystals());
        StartCoroutine(SpawnEffects());
        StartCoroutine(IncreaseDifficulty(amount, interval));
        StartCoroutine(DecreaseDifficulty(amount, interval * 2));
    }

    private IEnumerator SpawnObstacles() {
        while (true) {
            while (lastObstacleSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            Spawn(minObstaclesSpread, maxObstaclesSpread, ref lastObstacleSpawnPointX, ConvertObstaclesEnum(),
                obstaclesDistribution, obstaclesPadding, new string[] { "obstacle", "crystal" });

            yield return new WaitForSeconds(.2f);
        }
    }

    private IEnumerator IncreaseDifficulty(float amount, float interval) {
        while (true) {
            if (side == GameManager.Instance.Player.Side) {
                maxObstaclesSpread = Mathf.Clamp(maxObstaclesSpread - amount, minObstaclesSpread, maxObstaclesSpread);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator DecreaseDifficulty(float amount, float interval) {
        while (true) {
            if (side != GameManager.Instance.Player.Side) {
                maxObstaclesSpread = Mathf.Clamp(maxObstaclesSpread + amount, minObstaclesSpread, maxObstaclesSpread);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator SpawnCrystals() {
        while (true) {
            while (lastCrystalSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            Spawn(minCrystalsSpread, maxCrystalsSpread, ref lastCrystalSpawnPointX, ConvertCrystalsEnum(), crystalDistribution, crystalPadding, new string[] { "obstacle", "crystal" });

            yield return new WaitForSeconds(.2f);
        }
    }

    private IEnumerator SpawnEffects() {
        while (true) {
            while (lastEffectSpawnPointX > player.position.x + maxSpawnDistance) {
                yield return new WaitForSeconds(1f);
            }
            if(GameManager.Instance.Player.IsIncreasedBuffSpawnPassive) {
                maxEffectsSpread *= 0.8f;
            }
            Spawn(minEffectsSpread, maxEffectsSpread, ref lastEffectSpawnPointX, ConvertEffectsEnum(), effectsDistribution, effectsPadding, new string[] { "obstacle", "crystal" });

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

    private void Spawn(float minSpread, float maxSpread, ref float lastSpawnPointX, System.Enum[] objectsToSpawn, int[] objectsDistribution, float[] objectsPadding, string[] collisionTags) {
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
        SpawnableObject objectToSpawn = pool.GetObject(objectToSpawnName, false).GetComponent<SpawnableObject>();
        int sideIndicator = side == Side.UPPER ? 1 : -1;
        Vector2 spawnPosition = new Vector2(spawnPointX, sideIndicator * objectToSpawn.yPosition);
        if (IsOverlappingWith(spawnPosition, collisionTags)) {
            Vector2 mostSuitableSpawnPos = FindSuitableSpawn(spawnPosition, maxSpread - spread, spread - minSpread, collisionTags);
            if (spawnPosition != mostSuitableSpawnPos) {
                spawnPosition = mostSuitableSpawnPos;
            }
            else {
                lastSpawnPointX = spawnPosition.x;
                return;
            }
        }
        Quaternion rotation = Quaternion.identity;
        if(side == Side.BOTTOM) {
            rotation = Quaternion.Euler(0, 180, 180);
        }
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.gameObject.SetActiveRecursively(true);
        lastSpawnPointX = spawnPosition.x;
        //print(lastSpawnPointX);
    }

    private Vector2 FindSuitableSpawn(Vector2 currentSpawnPos, float forwardShifLength, float backwardShifLength, string[] collisionTags) {
        Vector2 result = currentSpawnPos;
        Vector2 searchVector = currentSpawnPos;
        for (int i = 0; i < forwardShifLength; i++) {
            searchVector.x += i;
            if (!IsOverlappingWith(searchVector, collisionTags)) {
                return searchVector;
            }
        }
        searchVector = currentSpawnPos;
        for (int i = 0; i < backwardShifLength; i++) {
            searchVector.x -= i;
            if (!IsOverlappingWith(searchVector, collisionTags)) {
                return searchVector;
            }
        }
        return result;
    }

    private bool IsOverlappingWith(Vector2 pos, string[] overlapTags) {
        Vector2 pointA = new Vector2(pos.x - 4, pos.y - 4);
        Vector2 pointB = new Vector2(pos.x + 4, pos.y + 4);
        Vector3 pos3D = new Vector3(pos.x, pos.y, 0);
        Collider2D[] colliders = Physics2D.OverlapAreaAll(pointA, pointB);
        foreach (Collider2D col in colliders) {
            foreach (string tag in overlapTags) {
                if (col.CompareTag(tag) && pos3D != col.transform.position) {
                    return true;
                }
            }
        }

        return false;
    }
}