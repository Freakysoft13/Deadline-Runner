using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	private const string EXP_KEY = "exp";
	private const string MONEY_KEY = "money";
	private const string ACTIVE_SKIN_KEY = "active_skin";
    private const string SHOP_ID = "shop_id";

	private static LevelManager instance;

	public int[] levelsExp;
	public int maxPowerUpLevel = 6;

	public static LevelManager Instance {
		get { return instance;}
	}

	void Awake () {
		instance = this;
	}
	
	public void AddExp(int amt) {
		PlayerPrefs.SetInt (EXP_KEY, GetExp() + amt);
	}

	public int GetExp() {
		return PlayerPrefs.GetInt (EXP_KEY, 0);
	}

	public int GetLevel() {
		int expSum = 0;
		int currentExp = GetExp ();
		int level = 0;
		for (int i = 0; i < levelsExp.Length; i++) {
			expSum += levelsExp[i];
			if(currentExp >= expSum) {
				level = i + 1;
			} else {
				break;
			}
		}
		return level;
	}

	public void AddMoney(int amt) {
		PlayerPrefs.SetInt (MONEY_KEY, GetMoney () + amt);
	}

	public int GetMoney() {
		return PlayerPrefs.GetInt (MONEY_KEY, 0);
	}

	public void UpgradePowerUpLevel(PowerUp PowerUp) {
		int currentPowerUpLevel = GetPowerUpLevel (PowerUp);
		if (currentPowerUpLevel >= maxPowerUpLevel) {
			return;
		}
		PlayerPrefs.SetInt (PowerUp.ToString(), currentPowerUpLevel + 1);
	}

	public int GetPowerUpLevel(PowerUp powerUp) {
		return PlayerPrefs.GetInt (powerUp.ToString(), 0);
	}

	public void AquireConsumable(Consumable consumable) {
		PlayerPrefs.SetInt (consumable.ToString(), 1);
	}

	public bool IsConsumableActive(Consumable consumable) {
		return PlayerPrefs.GetInt (consumable.ToString(), 0) == 1;
	}

	public int ActivateConsumable(Consumable consumable) {
		int isActive = PlayerPrefs.GetInt (consumable.ToString(), 0);
		PlayerPrefs.SetInt (consumable.ToString(), 0);
		return isActive;
	}

	public void ActivatePassiveSkill(Passive passive) {
		PlayerPrefs.SetInt (passive.ToString (), 1);
		foreach (Passive skill in Passive.GetValues(typeof(Passive))) {
			if(skill != passive) {
				PlayerPrefs.SetInt (skill.ToString (), 0);
			}
		}
	}

	public Passive GetActivePassive() {
		Passive result = Passive.NONE;
		foreach (Passive skill in Passive.GetValues(typeof(Passive))) {
			if(PlayerPrefs.GetInt (skill.ToString (), 0) == 1) {
				result = skill;
			}
		}
		return result;
	}
	//LevelManager.Instance.BuySkin (Skin.NONE);
	public void BuySkin(Skin skin) {
		PlayerPrefs.SetInt (skin.ToString (), 1);
	}
	
	//LevelManager.Instance.HasSkin (Skin.NONE);
	public bool HasSkin(Skin skin) {
		return PlayerPrefs.GetInt (skin.ToString (), 0) == 1;
	}

	public List<Skin> GetOwnedSkins() {
		List<Skin> skins = new List<Skin>();
		foreach (Skin skin in Skin.GetValues(typeof(Skin))) {
			if(PlayerPrefs.GetInt(skin.ToString(), 0) == 1) {
				skins.Add(skin);
			}
		}
		return skins;
	}
	
	//LevelManager.Instance.EquipSkin (Skin.NONE);
	public void EquipSkin(Skin skin) {
		if (!GetOwnedSkins ().Contains (skin)) {
			return;
		}

		PlayerPrefs.SetString (ACTIVE_SKIN_KEY, skin.ToString());
	}

	public Skin GetEquippedSkin() {
		foreach (Skin skin in GetOwnedSkins()) {
			if(PlayerPrefs.GetString(ACTIVE_SKIN_KEY, Skin.NONE.ToString()).Equals(skin.ToString())) {
				return skin;
			}
		}
		return Skin.NONE;
	}

    public void SetShopID(int index)
    {
        PlayerPrefs.SetInt(SHOP_ID,GetShopID()+index);
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

    public enum PowerUp {
		SHIELD, HP_BOOST, AFTER_LIFE, TREASURE_HUNTER
	}

	public enum Consumable {
		DOUBLE_EXP, DOUBLE_CRYSTALS, POWER_START, COLLECTOR_PET
	}

	public enum Passive {
		NONE, WILLOW, DELMOR, ARDEN, CRIMSON, MAXWELL, AMORET, MORDECAI, MORGANA, ELENOR, ARCHIBALD
	}

	public enum Skin {
		NONE, WALKING_ARMOR, UR900, QUEEN_NORA, WIZARD, GUNMAN, BARBARIAN, POET
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
}
