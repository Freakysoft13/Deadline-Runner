using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private ObjectDecorator objectDecorator;
    private ObjectTypesDataHolder dataHolder;
    public string[] decorationObjectNames;
    public ObjectTypesDataHolder.DecorationObjectType[] decorationObjectTypes;
    public string[] parallaxObjectsNames;
    public ObjectTypesDataHolder.ParallaxObjectType[] parallaxObjectTypes;
    public string[] obstacleNames;
    public ObjectTypesDataHolder.ObstacleType[] obstacleTypes;
    public string[] crystalNames;
    public ObjectTypesDataHolder.CrystalType[] crystalTypes;

    private float score;
    public int target = 60;

    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        objectDecorator = new ObjectDecorator();
        dataHolder = ObjectTypesDataHolder.Instance;
        dataHolder.ParallaxObjectNames = parallaxObjectsNames;
        dataHolder.ParallaxTypes = parallaxObjectTypes;
        dataHolder.ObstacleNames = obstacleNames;
        dataHolder.ObstacleTypes = obstacleTypes;
        dataHolder.DecorationObjectNames = decorationObjectNames;
        dataHolder.DecorationTypes = decorationObjectTypes;
        dataHolder.CrystalObjectNames = crystalNames;
        dataHolder.CrystalTypes = crystalTypes;
        dataHolder.Initialize();
    }

    void Start()
    {
        Application.targetFrameRate = target;
    }

    void Update()
    {
        if(target != Application.targetFrameRate)
        {
            Application.targetFrameRate = target;
        }
    }

    public void SpawnDecorationForObject(GameObject go, ObjectTypesDataHolder.DecorationObjectType type, bool isUpper)
    {
        objectDecorator.SpawnDecorationForObject(go, type, isUpper);
    }

    public void SpawnParallaxObjectForObject(GameObject go, ObjectTypesDataHolder.ParallaxObjectType type)
    {
        objectDecorator.SpawnParallaxObjectForObject(go, type);
    }

    public void AddScore(float amt)
    {
        score += amt;
        print(score);
    }
}
