using UnityEngine;
using System.Collections.Generic;

public class ObjectTypesDataHolder
{
    private bool isInitialized = false;

    private string[] obstacleNames;
    private ObstacleType[] obstacleTypes;

    private string[] parallaxObjectNames;
    private ParallaxObjectType[] parallaxTypes;

    private string[] decorationObjectNames;
    private DecorationObjectType[] decorationTypes;

    private string[] crystalObjectNames;
    private CrystalType[] crystalTypes;

    private string[] effectObjectNames;
    private EffectType[] effectTypes;

    public string[] ObstacleNames {
        set { obstacleNames = value; }
    }

    public ObstacleType[] ObstacleTypes {
        set { obstacleTypes = value; }
    }

    public string[] ParallaxObjectNames {
        set { parallaxObjectNames = value; }
    }

    public ParallaxObjectType[] ParallaxTypes {
        set { parallaxTypes = value; }
    }

    public string[] DecorationObjectNames {
        set { decorationObjectNames = value; }
    }

    public DecorationObjectType[] DecorationTypes {
        set { decorationTypes = value; }
    }

    public string[] CrystalObjectNames {
        set { crystalObjectNames = value; }
    }

    public CrystalType[] CrystalTypes {
        set { crystalTypes = value; }
    }

    public string[] EffectObjectNames {
        get {
            return effectObjectNames;
        }

        set {
            effectObjectNames = value;
        }
    }

    public EffectType[] EffectTypes {
        get {
            return effectTypes;
        }

        set {
            effectTypes = value;
        }
    }

    private Dictionary<ObstacleType, string> obstacleNameToTypeDic = new Dictionary<ObstacleType, string>();
    private Dictionary<CrystalType, string> crystalNameToTypeDic = new Dictionary<CrystalType, string>();
    private Dictionary<EffectType, string> effectNameToTypeDic = new Dictionary<EffectType, string>();
    private Dictionary<ParallaxObjectType, string> parallaxNameToTypeDic = new Dictionary<ParallaxObjectType, string>();
    private Dictionary<DecorationObjectType, string> decorationNameToTypeDic = new Dictionary<DecorationObjectType, string>();

    private static ObjectTypesDataHolder instance = new ObjectTypesDataHolder();
    public static ObjectTypesDataHolder Instance {
        get { return instance; }
    }
    
    public void Initialize() {
        if (!isInitialized) {
            string errorMessage = "{0} Length Is Greater Than {1} Length";
            CheckConsistency(obstacleNames, obstacleTypes, string.Format(errorMessage, "Obstacle Names", "Obstacle Types"));
            PopulateDictionary<ObstacleType, string>(obstacleTypes, obstacleNames, obstacleNameToTypeDic);
            CheckConsistency(parallaxObjectNames, parallaxTypes, string.Format(errorMessage, "Parallax Names", "Parallax Types"));
            PopulateDictionary<ParallaxObjectType, string>(parallaxTypes, parallaxObjectNames, parallaxNameToTypeDic);
            CheckConsistency(parallaxObjectNames, parallaxTypes, string.Format(errorMessage, "Decoration Names", "Decoration Types"));
            PopulateDictionary<DecorationObjectType, string>(decorationTypes, decorationObjectNames, decorationNameToTypeDic);
            CheckConsistency(crystalObjectNames, crystalTypes, string.Format(errorMessage, "Crystal Names", "Crystal Types"));
            PopulateDictionary<CrystalType, string>(crystalTypes, crystalObjectNames, crystalNameToTypeDic);
            CheckConsistency(effectObjectNames, effectTypes, string.Format(errorMessage, "Effect Names", "Effect Types"));
            PopulateDictionary<EffectType, string>(effectTypes, effectObjectNames, effectNameToTypeDic);
            isInitialized = true;
        }
    }

    private void CheckConsistency(System.Array arrayOne, System.Array arrayTwo, string errorMessage) {
        if (arrayOne.Length != arrayTwo.Length) {
            throw new UnityException(errorMessage);
        }
    }

    private void PopulateDictionary<T, V>(T[] keys, V[] values, Dictionary<T, V> dictionary) {
        for (int i = 0; i < keys.Length; i++) {
            dictionary.Add(keys[i], values[i]);
        }
    }

    public string GetParallaxNameForType(ParallaxObjectType type) {
        return parallaxNameToTypeDic[type];
    }

    public string GetObstacleNameForType(ObstacleType type) {
        return obstacleNameToTypeDic[type];
    }

    public string GetDecorationNameForType(DecorationObjectType type) {
        return decorationNameToTypeDic[type];
    }

    public string GetCrystalNameForType(CrystalType type) {
        return crystalNameToTypeDic[type];
    }

    public string GetObjectNameForType(System.Enum type) {
        if (type is ObstacleType) {
            return obstacleNameToTypeDic[(ObstacleType)type];
        }
        else if (type is CrystalType) {
            return crystalNameToTypeDic[(CrystalType)type];
        }
        else if (type is EffectType) {
            return effectNameToTypeDic[(EffectType)type];
        }
        return "";
    }

    public enum ObstacleType
    {
        GROUND_SPINNER, AXE
    }

    public enum CrystalType
    {
        RED, GREEN
    }

    public enum EffectType
    {
        BUFF, DEBUFF // in future SLOW_EFFECT, DOUBLE_COINS ... etc
    }

    public enum ParallaxObjectType
    {
        MOUNTAIN_FAR,
        MOUNTAIN_NEAR,
        TREE
    }
    public enum DecorationObjectType
    {
        UPPER_LIGHT,
        BOTTOM_LIGHT
    }
}
