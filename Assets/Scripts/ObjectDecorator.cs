using UnityEngine;

public class ObjectDecorator
{
    public void SpawnDecorationForObject(GameObject target, ObjectTypesDataHolder.DecorationObjectType type, bool isUpper)
    {
        Bounds currentBounds = target.GetComponent<BoxCollider2D>().bounds;
        float minX = currentBounds.min.x;
        float maxX = currentBounds.max.x;
        float spawnX = Random.Range(minX, maxX);
        float spawnY = 0;
        GameObject decoration = ObjectPool.Instance.GetObject(ObjectTypesDataHolder.Instance.GetDecorationNameForType(type));
        if (isUpper)
        {
            spawnY = 4;
        }
        else
        {
            spawnY = -4;
        }
        decoration.transform.position = new Vector2(spawnX, spawnY);
    }

    public void SpawnParallaxObjectForObject(GameObject target, ObjectTypesDataHolder.ParallaxObjectType type)
    {
        Bounds currentBounds = target.GetComponent<BoxCollider2D>().bounds;
        float spawnX = currentBounds.max.x;
        float spawnY = 4;
        GameObject parallaxObject = ObjectPool.Instance.GetObject(ObjectTypesDataHolder.Instance.GetParallaxNameForType(type));
        if (parallaxObject != null)
        {
            parallaxObject.transform.position = new Vector2(spawnX, spawnY);
        }
    }
}
