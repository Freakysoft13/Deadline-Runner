using UnityEngine;
using System.Collections;

public class BackgroundInit : MonoBehaviour
{
    public ObjectTypesDataHolder.ParallaxObjectType[] parallaxTypes;
    public int[] parallaxSpawnChances;
    public ObjectTypesDataHolder.DecorationObjectType upperDecorationType;
    public ObjectTypesDataHolder.DecorationObjectType bottomDecorationType;
    public int decorationSpawnChance = 50;
    public bool spawnUpperDecoration = true;
    public bool spawnBottomDecoration = true;

    public void Init()
    {
        if (GameManager.Instance == null) { return; }
        int chance = 0;
        for (int i = 0; i < parallaxTypes.Length; i++)
        {
            chance = Random.Range(0, 100);
            if (chance < parallaxSpawnChances[i])
            {
                GameManager.Instance.SpawnParallaxObjectForObject(gameObject, parallaxTypes[i]);
                break;
            }
        }
        chance = Random.Range(0, 100);
        if ((chance < decorationSpawnChance) && spawnUpperDecoration)
        {
            GameManager.Instance.SpawnDecorationForObject(gameObject, upperDecorationType, true);
        }
        chance = Random.Range(0, 100);
        if ((chance < decorationSpawnChance) && spawnBottomDecoration)
        {
            GameManager.Instance.SpawnDecorationForObject(gameObject, bottomDecorationType, false);
        }
    }
}
