public class BackgroundChainSpawner : ObjectChainSpawner
{
    private bool hasSpawnedSelf = false;
    private bool hasSpawnedNextObject = false;

    void OnEnable() {
        hasSpawnedSelf = false;
    }

    void Update() {
        if(spawnOnCollision) {
            return;
        }
        if(transform.position.x < 1.0f && !hasSpawnedNextObject) {
            SpawnNextObject();
            ResetSelfSapwnChance();
        }
    }

    protected override void SpawnNextObject()
    {
        if(hasSpawnedSelf) {
            return;
        }

        base.SpawnNextObject();
        BackgroundInit initScript = nextObject.GetComponent<BackgroundInit>();
        if (initScript != null)
        {
            initScript.Init();
        }
        hasSpawnedSelf = true;
        hasSpawnedNextObject = true;
        print("Self coord " + transform.position);
        print("Next coord " + nextObject.transform.position);
    }
}
