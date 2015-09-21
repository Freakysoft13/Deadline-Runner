public class BackgroundChainSpawner : ObjectChainSpawner
{
    protected override void SpawnNextObject()
    {
        base.SpawnNextObject();
        BackgroundInit initScript = nextObject.GetComponent<BackgroundInit>();
        if (initScript != null)
        {
            initScript.Init();
        }
    }
}
