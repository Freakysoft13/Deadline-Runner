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
    private int specialCurrency;
    private Player player;
    private bool hasRessurectedThisRun = false;

    public Effect.Shield shieldStub;
    public Effect.AfterLife afterLifeStub;
    public int expMultiplier = 1;
    public int scoreMultiplier = 1;
    public bool upgradePassives = false;


    public static GameManager Instance
    {
        get { return instance; }
    }

    public Player Player
    {
        get
        {
            return player;
        }

        set
        {
            player = value;
        }
    }

    public int Score { get; set; }
    public int SpecialCurrency
    {
        get { return specialCurrency; }
        set { specialCurrency = value; }
    }

    public bool HasRessurectedThisRun
    {
        get
        {
            return hasRessurectedThisRun;
        }

        set
        {
            hasRessurectedThisRun = value;
        }
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
        dataHolder.EffectObjectNames = effectNames;
        dataHolder.EffectTypes = effectTypes;
        dataHolder.Initialize();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        new ConsumablesManager().ApplyActiveConsumables();
        new TreePassivesManager().ApplyActivePassives();
        EventManager.Reset();
        InvokeRepeating("GrantAchievements", 1.0f, .3f);
    }

    void Start()
    {
        EventManager.OnPlayerDied += PlayerDie;
        EventManager.OnPlayerResurrected += PlayerResurrect;
        EventManager.OnBeforePlayerResurrected += BeforePlayerResurrect;
#if UNITY_ANDROID
        EventManager.OnLevelUp += () =>
        {
            if (LevelManager.Instance.IsMaxLevel())
            {
                GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_monster_slayer, 100.0f);
            }
        };
#endif
    }

    private void GrantAchievements()
    {
#if UNITY_ANDROID
        int gamesPlayed = PlayerPrefs.GetInt("games_played", 0);
        PlayerPrefs.SetInt("games_played", ++gamesPlayed);
        if (gamesPlayed > 0)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_newbie, 100.0f);
        }
        if (gamesPlayed > 24)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_addict, 100.0f);
        }
        if (gamesPlayed > 100)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_fan_club, 100.0f);
        }

        float distanceTraveled = Player.GetDistance();
        float crystalsCollected = LevelManager.Instance.GetTotalMoney();
        if (distanceTraveled > 499)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_sprinter, 100.0f);
        }
        if (distanceTraveled > 1999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_runner, 100.0f);
        }
        if (distanceTraveled > 4999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_long_distance_runner, 100.0f);
        }
        if (crystalsCollected > 99)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_something_shiny, 100.0f);
        }
        if (crystalsCollected > 4999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_gemology, 100.0f);
        }
        if (crystalsCollected > 49999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_jeweler, 100.0f);
        }
#endif
    }

    public void SpawnDecorationForObject(GameObject go, ObjectTypesDataHolder.DecorationObjectType type, bool isUpper)
    {
        objectDecorator.SpawnDecorationForObject(go, type, isUpper);
    }

    public void SpawnParallaxObjectForObject(GameObject go, ObjectTypesDataHolder.ParallaxObjectType type)
    {
        objectDecorator.SpawnParallaxObjectForObject(go, type);
    }

    public void AddCrystals(int amt)
    {
        score += amt * scoreMultiplier;
    }

    public void AddSpecialCurrency(int amt)
    {
        specialCurrency += amt;
    }

    public int GetCrystals()
    {
        return score;
    }

    public void PlayerDie()
    {
        //Time.timeScale = 0;

    }

    public void ApplyAfterLife()
    {
        afterLifeStub.PickUp();
    }

    public void PlayerResurrect()
    {
        shieldStub.PickUp();
        hasRessurectedThisRun = true;
    }

    public void BeforePlayerResurrect()
    {
        //Time.timeScale = 1;
    }

    public void HeadstartEnd()
    {
        shieldStub.PickUp();
    }

    public void SaveResult()
    {
        LevelManager.Instance.AddMoney(score);
    }

    public enum Side
    {
        UPPER, BOTTOM, NONE
    }
}
