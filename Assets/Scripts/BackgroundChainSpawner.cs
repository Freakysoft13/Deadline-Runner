

public class BackgroundChainSpawner : ObjectChainSpawner
{
    private bool hasSpawnedSelf = false;

    void OnEnable() {
        hasSpawnedSelf = false;
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
    }
}
