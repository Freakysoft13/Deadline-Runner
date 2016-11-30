using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    private const string EXP_KEY = "exp";
    private const string MONEY_KEY = "money";
    private const string TOTAL_MONEY_KEY = "total_money";
    private const string SPECIAL_MONEY_KEY = "special_money";
    private const string SPECIAL_TOTAL_MONEY_KEY = "special_total_money";
    private const string ACTIVE_SKIN_KEY = "active_skin";
    private const string SHOP_ID = "shop_id";
    private const string ACTIVE_CONSUMABLE = "active";
    private const string BEST_SCORE = "best_score";
    private const string SOUND_CHECK = "sound_check";
    private const string SFX_CHECK = "sfx_check";

    private static LevelManager instance;

    public int[] levelsExp;
    public int maxPowerUpLevel = 6;

    public int[] shieldTimers;
    public int[] crystalValues;
    public int[] afterLifeTimers;
    public int[] strangeMachineTimers;
    public SkeletonDataAsset[] skins;


    public delegate void OnPurchase(object arg);

    public OnPurchase InGameMoneyPurchase;

    public static LevelManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
        List<Skin> ownedSkins = GetOwnedSkins();
        if (!ownedSkins.Contains(Skin.NONE))
        {
            BuySkin(Skin.NONE);
        }
        Screen.fullScreen = true;
    }

    public void AddExp(int amt)
    {
        PlayerPrefs.SetInt(EXP_KEY, GetExp() + amt);
    }

    public int GetExp()
    {
        return PlayerPrefs.GetInt(EXP_KEY, 0);
    }

    public int GetExpThisLevel()
    {
        int overallExp = GetExp();
        int expForPrevLevels = 0;
        for (int i = 0; i < GetLevel() + 1; i++)
        {
            expForPrevLevels += levelsExp[i];
        }
        return overallExp - expForPrevLevels;
    }

    public bool IsMaxLevel()
    {
        return GetLevel() == 10;
    }

    public int GetLevel()
    {
        int currentExp = GetExp();
        int level = 0;
        for (int i = 1; i < levelsExp.Length; i++)
        {
            if (currentExp >= levelsExp[i])
            {
                level = i;
                currentExp -= levelsExp[i];
            }
            else
            {
                break;
            }
        }
        return level;
    }

    public void AddMoney(int amt)
    {
        PlayerPrefs.SetInt(MONEY_KEY, GetMoney() + amt);
        PlayerPrefs.SetInt(TOTAL_MONEY_KEY, GetTotalMoney() + amt);
    }

    public void AddSpecialMoney(int amt)
    {
        PlayerPrefs.SetInt(SPECIAL_MONEY_KEY, GetSpecialMoney() + amt);
        PlayerPrefs.SetInt(SPECIAL_TOTAL_MONEY_KEY, GetSpecialTotalMoney() + amt);
    }

    public int GetSpecialMoney()
    {
        return PlayerPrefs.GetInt(SPECIAL_MONEY_KEY, 0);
    }
    public int GetSpecialTotalMoney()
    {
        return PlayerPrefs.GetInt(SPECIAL_TOTAL_MONEY_KEY, 0);
    }

    public int GetMoney()
    {
        return PlayerPrefs.GetInt(MONEY_KEY, 0);
    }
    public int GetTotalMoney()
    {
        return PlayerPrefs.GetInt(TOTAL_MONEY_KEY, 0);
    }
    public void UpgradePowerUpLevel(PowerUp PowerUp)
    {
        int currentPowerUpLevel = GetPowerUpLevel(PowerUp);
        if (currentPowerUpLevel >= maxPowerUpLevel)
        {
            return;
        }
        PlayerPrefs.SetInt(PowerUp.ToString(), currentPowerUpLevel + 1);
    }

    public int GetPowerUpLevel(PowerUp powerUp)
    {
        return PlayerPrefs.GetInt(powerUp.ToString(), 0);
    }

    public void AquireConsumable(Consumable consumable)
    {
        PlayerPrefs.SetInt(consumable.ToString(), 1);
    }

    public bool IsConsumableActive(Consumable consumable)
    {
        return PlayerPrefs.GetInt(consumable.ToString(), 0) == 1;
    }

    public void DeactivateConsumable(Consumable consumable)
    {
        PlayerPrefs.SetInt(consumable.ToString(), 0);
    }

    /*public int ActivateConsumable(Consumable consumable)
    {
        int amt = PlayerPrefs.GetInt(consumable.ToString(), 0);
        if (amt > 0)
        {
            PlayerPrefs.SetInt(consumable.ToString(), --amt);
            PlayerPrefs.SetInt(consumable.ToString() + ACTIVE_CONSUMABLE, 1);
            return 1;
        }
        return 0;
    }*/

    public void AquireInGameMoney(MenuUI.Chest chestName)
    {
        InGameMoneyPurchase(chestName);
    }

    public void AddMoneyFromChest(MenuUI.Chest chestName)
    {
        switch (chestName)
        {
            case MenuUI.Chest.SMALL: AddMoney(5500); break;
            case MenuUI.Chest.MEDIUM: AddMoney(12000); break;
            case MenuUI.Chest.BIG: AddMoney(31000); break;
            case MenuUI.Chest.XXL: AddMoney(65000); break;
        }
    }

    public void AddMoneyFromChest(string chestName)
    {
        if (chestName.Equals(Purchaser.GP_SMALL))
        {
            AddMoney(5500);
        }
        else if (chestName.Equals(Purchaser.GP_MEDIUM))
        {
            AddMoney(12000);
        }
        else if (chestName.Equals(Purchaser.GP_BIG))
        {
            AddMoney(31000);
        }
        else if (chestName.Equals(Purchaser.GP_XXL))
        {
            AddMoney(65000);
        }
    }

    public void ActivatePassiveSkill(Passive passive)
    {
        PlayerPrefs.SetInt(passive.ToString(), 1);
        foreach (Passive skill in Passive.GetValues(typeof(Passive)))
        {
            if (skill != passive)
            {
                PlayerPrefs.SetInt(skill.ToString(), 0);
            }
        }
    }

    public List<Consumable> GetActiveConsumables()
    {
        List<Consumable> activeConsumables = new List<Consumable>();
        foreach (Consumable c in System.Enum.GetValues(typeof(Consumable)))
        {
            if (IsConsumableActive(c))
            {
                activeConsumables.Add(c);
            }
        }
        return activeConsumables;
    }

    public Passive GetActivePassive()
    {
        Passive result = Passive.NONE;
        foreach (Passive skill in Passive.GetValues(typeof(Passive)))
        {
            if (PlayerPrefs.GetInt(skill.ToString(), 0) == 1)
            {
                result = skill;
            }
        }
        return result;
    }
    //LevelManager.Instance.BuySkin (Skin.NONE);
    public void BuySkin(Skin skin)
    {
        PlayerPrefs.SetInt(skin.ToString(), 1);
    }

    //LevelManager.Instance.HasSkin (Skin.NONE);
    public bool HasSkin(Skin skin)
    {
        return PlayerPrefs.GetInt(skin.ToString(), 0) == 1;
    }

    public bool HasSkin(int index)
    {
        return HasSkin(GetSkinByIndex(index));
    }

    public List<Skin> GetOwnedSkins()
    {
        List<Skin> skins = new List<Skin>();
        foreach (Skin skin in Skin.GetValues(typeof(Skin)))
        {
            if (PlayerPrefs.GetInt(skin.ToString(), 0) == 1)
            {
                skins.Add(skin);
            }
        }
        return skins;
    }

    //LevelManager.Instance.EquipSkin (Skin.NONE);
    public void EquipSkin(Skin skin)
    {
        if (!GetOwnedSkins().Contains(skin))
        {
            return;
        }

        PlayerPrefs.SetString(ACTIVE_SKIN_KEY, skin.ToString());
    }

    public void EquipSkin(int index)
    {
        EquipSkin(GetSkinByIndex(index));
    }

    private Skin GetSkinByIndex(int index)
    {
        return (Skin)System.Enum.GetValues(typeof(Skin)).GetValue(index);
    }

    public Skin GetEquippedSkin()
    {
        foreach (Skin skin in GetOwnedSkins())
        {
            if (PlayerPrefs.GetString(ACTIVE_SKIN_KEY, Skin.NONE.ToString()).Equals(skin.ToString()))
            {
                return skin;
            }
        }
        return Skin.NONE;
    }
    //Int to load directly special shop
    public void SetShopID(int index)
    {
        PlayerPrefs.SetInt(SHOP_ID, GetShopID() + index);
    }
    public int GetShopID()
    {
        return PlayerPrefs.GetInt(SHOP_ID, 0);
    }

    public void SaveAchievementProgress(Achievement achievement, int amt)
    {
        PlayerPrefs.SetInt(achievement.ToString(), amt);
    }

    public int GetAchievementProgress(Achievement achievement)
    {
        return PlayerPrefs.GetInt(achievement.ToString(), 0);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(BEST_SCORE, 0);
    }

    public void SaveBestScore(int score)
    {
        PlayerPrefs.SetInt(BEST_SCORE, score);
    }
    //Sound check
    public void SetSoundCkeck(int index)
    {
        PlayerPrefs.SetInt(SOUND_CHECK, GetSoundCheck() + index);
    }
    public int GetSoundCheck()
    {
        return PlayerPrefs.GetInt(SOUND_CHECK, 0);
    }


    public enum PowerUp
    {
        SHIELD, HP_BOOST, AFTER_LIFE, TREASURE_HUNTER, WIRE, MACHINE, SPECIAL
    }

    public enum Consumable
    {
        DOUBLE_EXP, DOUBLE_CRYSTALS, POWER_START, COLLECTOR_PET
    }

    public enum Passive
    {
        NONE, WILLOW, DELMOR, ARDEN, CRIMSON, MAXWELL, AMORET, MORDECAI, MORGANA, ELENOR, ARCHIBALD
    }

    public enum Skin
    {
        NONE, WALKING_ARMOR, UR900, WIZARD, QUEEN_NORA, GUNMAN, BARBARIAN, POET, GHOST
    }

    public enum Achievement
    {
        NONE
    }

    public int GetItemIndex<T>(T item)
    {
        System.Array itemArray = System.Enum.GetValues(typeof(T));
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (item.Equals(itemArray.GetValue(i)))
            {
                return i;
            }
        }
        return 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }
}
