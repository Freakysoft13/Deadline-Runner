using UnityEngine;
using System.Collections;

public class ObjectChainSpawner : MonoBehaviour
{
    public string[] availableObjects;
    public int[] probabilities;
    public bool spawnOnCollision = true;
    public string spawnerTag = "Player";
    public float xOffset = 0.0f;
    public int selfRespawnChace = 0;
    public int selfDiminishingReturn;

    protected GameObject nextObject;
    protected int nextObjIndex = 0;
    protected ObjectPool objectPool;
    protected int storedSelfSpawnChance;

    protected virtual void Start()
    {
        objectPool = ObjectPool.Instance;
        storedSelfSpawnChance = selfRespawnChace;
    }

    //Probabilities should start from least possible and up
    protected void SelectNextObject()
    {
        int selfChance = Random.Range(0, 100);
        if (selfRespawnChace > selfChance)
        {
            nextObject = objectPool.GetObject(name);
            nextObject.GetComponent<ObjectChainSpawner>().selfRespawnChace -= selfDiminishingReturn;
            return;
        }
        if (probabilities.Length > 0 && probabilities.Length == availableObjects.Length)
        {
            int randomValue = Random.Range(0, 100);
            for (int i = 0; i < probabilities.Length; i++)
            {
                if (probabilities[i] > randomValue)
                {
                    nextObjIndex = i;
                    break;
                }
            }
        }
        else
        {
            print("Chain Spawner Configuration Issue. Fallback Scenario. -AR ;) FOR " + name);
            nextObjIndex = Random.Range(0, availableObjects.Length);
        }
        nextObject = objectPool.GetObject(availableObjects[nextObjIndex]);
    }

    private Vector2 CalculateNextObjectPosition()
    {
        Vector2 nextObjectPosition = Vector2.zero;
        BoxCollider2D currentCollider = GetComponent<BoxCollider2D>();
        BoxCollider2D nextObjectCollider = nextObject.GetComponent<BoxCollider2D>();
        float xPosition = currentCollider.bounds.max.x + (nextObjectCollider.bounds.size.x / 2.0f);
        nextObjectPosition.x = xPosition - xOffset;
        return nextObjectPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(spawnerTag) && objectPool != null && spawnOnCollision)
        {
            SpawnNextObject();
            ResetSelfSapwnChance();
        }
    }

    protected virtual void SpawnNextObject()
    {
        SelectNextObject();
        nextObject.transform.position = CalculateNextObjectPosition();
    }

    public void ResetSelfSapwnChance()
    {
        selfRespawnChace = storedSelfSpawnChance;
    }
}
