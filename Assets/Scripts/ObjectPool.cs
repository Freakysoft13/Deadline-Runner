using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public string[] backgroundNames;
    public string[] floorNames;
    public string[] lightNames;
    public string[] parallaxObjectsNames;
    public string[] obstaclesNames;
    public string[] crystalNames;
    public string[] effectNames;
    public int backgroundCopies = 2;
    public int floorCopies = 20;
    public int lightCopies = 4;
    public int parallaxObjectsCopies = 4;
    public int obstaclesCopies = 4;
    public int crystalCopies = 4;
    public int effectCopies = 4;

    private static ObjectPool instance;

    public static ObjectPool Instance
    {
        get { return instance; }
    }

    private List<Dictionary<string, List<GameObject>>> allObjectMapsList = new List<Dictionary<string, List<GameObject>>>();

    private Dictionary<string, List<GameObject>> backgroundsMap = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> floorsMap = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> lightsMap = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> parallaxObjectsMap = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> obstaclesMap = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> crystalsMap = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> effectsMap = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        instance = this;
        PreloadAllObjects();
    }

    private void PreloadAllObjects()
    {
        PreloadBackgrounds();
        PreloadFloor();
        PreloadLight();
        PreloadParallaxObjects();
        PreloadObstacles();
        PreloadCyrstals();
        PreloadEffects();
    }

    private void PreloadBackgrounds()
    {
        PreloadObjects(backgroundNames, backgroundCopies, backgroundsMap);
    }

    private void PreloadFloor()
    {
        PreloadObjects(floorNames, floorCopies, floorsMap);
    }

    private void PreloadLight()
    {
        PreloadObjects(lightNames, lightCopies, lightsMap);
    }

    private void PreloadParallaxObjects()
    {
        PreloadObjects(parallaxObjectsNames, parallaxObjectsCopies, parallaxObjectsMap);
    }

    private void PreloadObstacles()
    {
        PreloadObjects(obstaclesNames, obstaclesCopies, obstaclesMap);
    }

    private void PreloadCyrstals()
    {
        PreloadObjects(crystalNames, crystalCopies, crystalsMap);
    }

    private void PreloadEffects() {
        PreloadObjects(effectNames, effectCopies, effectsMap);
    }

    private void PreloadObjects(string[] prefabNames, int copiesCount, Dictionary<string, List<GameObject>> container)
    {
        foreach (string name in prefabNames)
        {
            GameObject obj = Resources.Load(name) as GameObject;
            List<GameObject> objects = new List<GameObject>();
            for (int i = 0; i < copiesCount; i++)
            {
                GameObject instantiatedObject = GameObject.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
                instantiatedObject.name = obj.name;
                instantiatedObject.SetActive(false);
                objects.Add(instantiatedObject);
            }
            container.Add(name, objects);
        }
        allObjectMapsList.Add(container);
    }

    public GameObject GetObject(string type, bool autoActivate)
    {
        foreach (Dictionary<string, List<GameObject>> container in allObjectMapsList)
        {
            if (container.ContainsKey(type))
            {
                return GetObjectByType(type, container, autoActivate);
            }
        }
        return null;
    }

    public GameObject GetObject(string type)
    {
        foreach (Dictionary<string, List<GameObject>> container in allObjectMapsList)
        {
            if (container.ContainsKey(type))
            {
                return GetObjectByType(type, container, true);
            }
        }
        return null;
    }

    private GameObject GetObjectByType(string type, Dictionary<string, List<GameObject>> container, bool autoActivate)
    {
        List<GameObject> objectsForName = container[type];
        foreach (GameObject obj in objectsForName)
        {
            if (!obj.activeSelf)
            {
                foreach(Transform t in obj.transform)
                {
                    t.gameObject.SetActive(true);
                }
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }
}
