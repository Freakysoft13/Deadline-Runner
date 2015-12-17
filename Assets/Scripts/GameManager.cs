using UnityEngine;
using UnityEngine.UI;

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
    public string[] effectNames;
    public LevelManager.PowerUp[] effectTypes;
   

    private int score;
    private Player player;
    private bool hasRessurectedThisRun = false;

    public Effect.Shield shieldStub;
    public Effect.AfterLife afterLifeStub;
    public GameObject grayscaleCamera;
    public GameObject playerCamera;
    public GameObject mainCamera;
    public int expMultiplier = 1;
    public int scoreMultiplier = 1;
    public int target = 60;

    public static GameManager Instance {
        get { return instance; }
    }

    public Player Player {
        get {
            return player;
        }

        set {
            player = value;
        }
    }

    public bool HasRessurectedThisRun {
        get {
            return hasRessurectedThisRun;
        }

        set {
            hasRessurectedThisRun = value;
        }
    }

    void Awake() {
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
        dataHolder.EffectObjectNames = effectNames;
        dataHolder.EffectTypes = effectTypes;
        dataHolder.Initialize();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start() {
        Application.targetFrameRate = target;
        EventManager.Instance.OnPlayerDied += PlayerDie;
        EventManager.Instance.OnPlayerResurrected += PlayerResurrect;
        EventManager.Instance.OnBeforePlayerResurrected += BeforePlayerResurrect;
    }

    void Update() {
        if (target != Application.targetFrameRate) {
            Application.targetFrameRate = target;
        }
    }

    public void SpawnDecorationForObject(GameObject go, ObjectTypesDataHolder.DecorationObjectType type, bool isUpper) {
        objectDecorator.SpawnDecorationForObject(go, type, isUpper);
    }

    public void SpawnParallaxObjectForObject(GameObject go, ObjectTypesDataHolder.ParallaxObjectType type) {
        objectDecorator.SpawnParallaxObjectForObject(go, type);
    }

    public void AddScore(int amt) {
        score += amt * scoreMultiplier;
    }

    public int GetScore() {
        return score;
    }

    public void PlayerDie() {
        //Time.timeScale = 0;
        mainCamera.SetActive(true);
        playerCamera.SetActive(false);
        grayscaleCamera.SetActive(false);
    }

    public void ApplyAfterLife() {
        playerCamera.SetActive(true);
        grayscaleCamera.SetActive(true);
        mainCamera.SetActive(false);
        afterLifeStub.PickUp();
    }

    public void PlayerResurrect() {
        shieldStub.PickUp();
        hasRessurectedThisRun = true;
    }

    public void BeforePlayerResurrect() {
        //Time.timeScale = 1;
    }

    public void HeadstartEnd() {
        shieldStub.PickUp();
    }

    public void SaveResult() {
        LevelManager.Instance.AddMoney(score);
    }
    
    public enum Side
    {
        UPPER, BOTTOM
    }
}
