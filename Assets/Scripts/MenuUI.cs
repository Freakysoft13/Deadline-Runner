using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject PowerUpMenu;
    public GameObject CharacterMenu;
    public GameObject ConsumablesMenu;
    public GameObject SpecialMenu;

    public Text PowerUpText1;
    public Text PowerUpText2;
    public Text CharactersText1;
    public Text CharactersText2;
    public Text ConsumablesText1;
    public Text ConsumablesText2;
    public Text SpecialText1;
    public Text SpecialText2;

    public Image DefaultPwUp;
    public Image PressedPwUP;
    public Image DefaultChar;
    public Image PressedChar;
    public Image DefaultConsum;
    public Image PressedConsum;
    public Image DefaultSpecial;
    public Image PressedSpecial;

    public int[] powerUpLvlPrices;

    public Image[] ShieldPowerUPImage;
    public Text[] ShieldText;
    public Image[] HpBoostPowerUPImage;
    public Text[] HpBoostText;
    public Image[] AfterlifePowerUPImage;
    public Text[] AfterlifeText;
    public Image[] CrystalPowerUPImage;
    public Text[] CrystalText;

    public RectTransform BuyShieldBtn;
    public RectTransform ShieldMaxedBtn;
    public RectTransform BuyHpBoostBtn;
    public RectTransform HpBoostMaxedBtn;
    public RectTransform BuyAfterlifeBtn;
    public RectTransform AfterlifeMaxedBtn;
    public RectTransform BuyCrystalBtn;
    public RectTransform CrystalMaxedBtn;

    public Text ShieldPrice;
    public Text HpBoostPrice;
    public Text AfterlifePrice;
    public Text CrystalPrice;

    private int DoubleExpPrice = 500;
    private int DoubleShardsPrice = 500;
    private int PowerDashPrice = 250;
    private int MagnetPrice = 250;

    public RectTransform BuyExpBtn;
    public RectTransform ExpActiveBtn;
    public RectTransform BuyShardsBtn;
    public RectTransform ShardsActiveBtn;
    public RectTransform BuyPowerDashBtn;
    public RectTransform PowerDashActiveBtn;
    public RectTransform BuyMagnetBtn;
    public RectTransform MagnetActiveBtn;

    public GameObject BlockingMask;
    public GameObject[] characterblock;

    public int[] shieldValue;
    public int[] machineValue;
    public int[] afterlifeValue;
    public int[] bigCryValue;
    public Text shieldvaluetxt;
    public Text mashinevaluetxt;
    public Text afterlifevaluetxt;
    public Text bigcrystaltxt;


    void Start()
    {
        if (LevelManager.Instance.GetShopID() == 1)
        {
            Special();
            PowerUpText2.gameObject.SetActive(false);
            PowerUpText1.gameObject.SetActive(true);
            CharactersText2.gameObject.SetActive(false);
            CharactersText1.gameObject.SetActive(true);
            ConsumablesText2.gameObject.SetActive(false);
            ConsumablesText1.gameObject.SetActive(true);
            SpecialText2.gameObject.SetActive(true);
            SpecialText1.gameObject.SetActive(false);

            DefaultPwUp.gameObject.SetActive(true);
            PressedPwUP.gameObject.SetActive(false);
            DefaultChar.gameObject.SetActive(true);
            PressedChar.gameObject.SetActive(false);
            DefaultConsum.gameObject.SetActive(true);
            PressedConsum.gameObject.SetActive(false);
            DefaultSpecial.gameObject.SetActive(false);
            PressedSpecial.gameObject.SetActive(true);
            LevelManager.Instance.SetShopID(-1);
            shieldvaluetxt.gameObject.SetActive(false);
            mashinevaluetxt.gameObject.SetActive(false);
            afterlifevaluetxt.gameObject.SetActive(false);
            bigcrystaltxt.gameObject.SetActive(false);
        }

        else
        {
            PowerUpMenu.SetActive(true);
            CharacterMenu.SetActive(false);
            ConsumablesMenu.SetActive(false);
            SpecialMenu.SetActive(false);

            PowerUpText2.gameObject.SetActive(true);
            PowerUpText1.gameObject.SetActive(false);
            CharactersText2.gameObject.SetActive(false);
            CharactersText1.gameObject.SetActive(true);
            ConsumablesText2.gameObject.SetActive(false);
            ConsumablesText1.gameObject.SetActive(true);
            SpecialText2.gameObject.SetActive(false);
            SpecialText1.gameObject.SetActive(true);

            DefaultPwUp.gameObject.SetActive(false);
            PressedPwUP.gameObject.SetActive(true);
            DefaultChar.gameObject.SetActive(true);
            PressedChar.gameObject.SetActive(false);
            DefaultConsum.gameObject.SetActive(true);
            PressedConsum.gameObject.SetActive(false);
            DefaultSpecial.gameObject.SetActive(true);
            PressedSpecial.gameObject.SetActive(false);

            BlockingMask.SetActive(false);
        }

        Checker();
    }

    public void PowerUp()
    {
        if (PowerUpMenu.activeInHierarchy)
        {
            PowerUpMenu.SetActive(true);
        }
        else
        {
            PowerUpMenu.SetActive(true);
        }
        if (CharacterMenu.activeInHierarchy)
        {
            CharacterMenu.SetActive(false);
        }
        if (ConsumablesMenu.activeInHierarchy)
        {
            ConsumablesMenu.SetActive(false);
        }
        if (SpecialMenu.activeInHierarchy)
        {
            SpecialMenu.SetActive(false);
        }

    }
    public void Character()
    {
        if (CharacterMenu.activeInHierarchy)
        {
            CharacterMenu.SetActive(true);
        }
        else
        {
            CharacterMenu.SetActive(true);
        }
        if (PowerUpMenu.activeInHierarchy)
        {
            PowerUpMenu.SetActive(false);
        }
        if (ConsumablesMenu.activeInHierarchy)
        {
            ConsumablesMenu.SetActive(false);
        }
        if (SpecialMenu.activeInHierarchy)
        {
            SpecialMenu.SetActive(false);
        }

    }
    public void Consumables()
    {
        if (ConsumablesMenu.activeInHierarchy)
        {
            ConsumablesMenu.SetActive(true);
        }
        else
        {
            ConsumablesMenu.SetActive(true);
        }
        if (PowerUpMenu.activeInHierarchy)
        {
            PowerUpMenu.SetActive(false);
        }
        if (CharacterMenu.activeInHierarchy)
        {
            CharacterMenu.SetActive(false);
        }
        if (SpecialMenu.activeInHierarchy)
        {
            SpecialMenu.SetActive(false);
        }
    }
    public void Special()
    {
        if (SpecialMenu.activeInHierarchy)
        {
            SpecialMenu.SetActive(true);
        }
        else
        {
            SpecialMenu.SetActive(true);
        }
        if (PowerUpMenu.activeInHierarchy)
        {
            PowerUpMenu.SetActive(false);
        }
        if (CharacterMenu.activeInHierarchy)
        {
            CharacterMenu.SetActive(false);
        }
        if (ConsumablesMenu.activeInHierarchy)
        {
            ConsumablesMenu.SetActive(false);
        }
    }

    public void PwUpTextSwap()
    {
        PowerUpText2.gameObject.SetActive(true);
        PowerUpText1.gameObject.SetActive(false);
        CharactersText2.gameObject.SetActive(false);
        CharactersText1.gameObject.SetActive(true);
        ConsumablesText2.gameObject.SetActive(false);
        ConsumablesText1.gameObject.SetActive(true);
        SpecialText2.gameObject.SetActive(false);
        SpecialText1.gameObject.SetActive(true);
    }

    public void CharTextSwap()
    {
        PowerUpText2.gameObject.SetActive(false);
        PowerUpText1.gameObject.SetActive(true);
        CharactersText2.gameObject.SetActive(true);
        CharactersText1.gameObject.SetActive(false);
        ConsumablesText2.gameObject.SetActive(false);
        ConsumablesText1.gameObject.SetActive(true);
        SpecialText2.gameObject.SetActive(false);
        SpecialText1.gameObject.SetActive(true);
    }
    public void ConsumTextSwap()
    {
        PowerUpText2.gameObject.SetActive(false);
        PowerUpText1.gameObject.SetActive(true);
        CharactersText2.gameObject.SetActive(false);
        CharactersText1.gameObject.SetActive(true);
        ConsumablesText2.gameObject.SetActive(true);
        ConsumablesText1.gameObject.SetActive(false);
        SpecialText2.gameObject.SetActive(false);
        SpecialText1.gameObject.SetActive(true);
    }
    public void SpecialTextSwap()
    {
        PowerUpText2.gameObject.SetActive(false);
        PowerUpText1.gameObject.SetActive(true);
        CharactersText2.gameObject.SetActive(false);
        CharactersText1.gameObject.SetActive(true);
        ConsumablesText2.gameObject.SetActive(false);
        ConsumablesText1.gameObject.SetActive(true);
        SpecialText2.gameObject.SetActive(true);
        SpecialText1.gameObject.SetActive(false);
    }

    public void PowerUpButtonSwap()
    {
        DefaultPwUp.gameObject.SetActive(false);
        PressedPwUP.gameObject.SetActive(true);
        DefaultChar.gameObject.SetActive(true);
        PressedChar.gameObject.SetActive(false);
        DefaultConsum.gameObject.SetActive(true);
        PressedConsum.gameObject.SetActive(false);
        DefaultSpecial.gameObject.SetActive(true);
        PressedSpecial.gameObject.SetActive(false);
    }
    public void CharactersButtonSwap()
    {
        DefaultPwUp.gameObject.SetActive(true);
        PressedPwUP.gameObject.SetActive(false);
        DefaultChar.gameObject.SetActive(false);
        PressedChar.gameObject.SetActive(true);
        DefaultConsum.gameObject.SetActive(true);
        PressedConsum.gameObject.SetActive(false);
        DefaultSpecial.gameObject.SetActive(true);
        PressedSpecial.gameObject.SetActive(false);
    }
    public void ConsumablesButtonSwap()
    {
        DefaultPwUp.gameObject.SetActive(true);
        PressedPwUP.gameObject.SetActive(false);
        DefaultChar.gameObject.SetActive(true);
        PressedChar.gameObject.SetActive(false);
        DefaultConsum.gameObject.SetActive(false);
        PressedConsum.gameObject.SetActive(true);
        DefaultSpecial.gameObject.SetActive(true);
        PressedSpecial.gameObject.SetActive(false);
    }
    public void SpecialButtonSwap()
    {
        DefaultPwUp.gameObject.SetActive(true);
        PressedPwUP.gameObject.SetActive(false);
        DefaultChar.gameObject.SetActive(true);
        PressedChar.gameObject.SetActive(false);
        DefaultConsum.gameObject.SetActive(true);
        PressedConsum.gameObject.SetActive(false);
        DefaultSpecial.gameObject.SetActive(false);
        PressedSpecial.gameObject.SetActive(true);
    }

    public void BuyUpgradeShield()
    {
        print(string.Format("Shield lasts for {0} seconds {1}", 5, ""));
        print(string.Format("Shield lasts for {0} seconds {1}", 5, "and spawns more often"));
        BuyItem(LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD), LevelManager.PowerUp.SHIELD, ShieldPowerUPImage, ShieldText, ShieldPrice,
            BuyShieldBtn, ShieldMaxedBtn,shieldvaluetxt,shieldValue);
    }

    public void BuyHpBoost()
    {
        BuyItem(LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST), LevelManager.PowerUp.HP_BOOST, HpBoostPowerUPImage, HpBoostText, HpBoostPrice,
            BuyHpBoostBtn, HpBoostMaxedBtn,mashinevaluetxt,machineValue);
    }

    public void BuyAfterlife()
    {
        BuyItem(LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.AFTER_LIFE), LevelManager.PowerUp.AFTER_LIFE, AfterlifePowerUPImage, AfterlifeText, AfterlifePrice,
            BuyAfterlifeBtn, AfterlifeMaxedBtn,afterlifevaluetxt,afterlifeValue);
    }

    public void BuyBigCrystal()
    {
        BuyItem(LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.TREASURE_HUNTER), LevelManager.PowerUp.TREASURE_HUNTER,CrystalPowerUPImage, CrystalText, CrystalPrice,
           BuyCrystalBtn,CrystalMaxedBtn,bigcrystaltxt,bigCryValue);
    }

    private readonly object lock_ = new object();

    private void BuyItem(int powerUpLevelIndex, LevelManager.PowerUp powerUp, Image[] images, Text[] texts, Text priceText, RectTransform buyButton, RectTransform maxedButton,Text UpgradeValue, int[] value)
    {
        lock(lock_)
        {
            int prevPowerUpLevelIndex = powerUpLevelIndex - 1;
            int priceIndex = powerUpLevelIndex + 1;
            bool isLevelValid = (powerUpLevelIndex >= 0 && powerUpLevelIndex < 6);
            if (isLevelValid && (LevelManager.Instance.GetMoney() >= powerUpLvlPrices[powerUpLevelIndex]))
            {
                LevelManager.Instance.UpgradePowerUpLevel(powerUp);
                print("Upgrade " + powerUp + " : " + powerUpLevelIndex);
                LevelManager.Instance.AddMoney(-powerUpLvlPrices[powerUpLevelIndex]);
                images[powerUpLevelIndex].gameObject.SetActive(true);
                texts[powerUpLevelIndex].gameObject.SetActive(true);



                if (prevPowerUpLevelIndex >= 0)
                {
                    images[prevPowerUpLevelIndex].gameObject.SetActive(false);
                    texts[prevPowerUpLevelIndex].gameObject.SetActive(false);
                   
                }
                if (powerUpLevelIndex >= 0 && powerUpLevelIndex <= 5)
                {
                    if (UpgradeValue.gameObject.activeInHierarchy)

                    {
                        UpgradeValue.gameObject.SetActive(false);
                        UpgradeValue.gameObject.SetActive(true);
                        UpgradeValue.text = "+" + value[powerUpLevelIndex].ToString();
                    }
                    else
                    {
                        UpgradeValue.gameObject.SetActive(true);
                        UpgradeValue.text = "+" + value[powerUpLevelIndex].ToString();

                    }
                }
                if (powerUpLevelIndex < 5)
                {
                    priceText.text = powerUpLvlPrices[priceIndex].ToString();
                   
                }
            

                else
                {
                    buyButton.gameObject.SetActive(false);
                    maxedButton.gameObject.SetActive(true);
                    priceText.text = powerUpLvlPrices[prevPowerUpLevelIndex].ToString();
                    return;
                }
               
            }
            else if (isLevelValid && LevelManager.Instance.GetMoney() < powerUpLvlPrices[powerUpLevelIndex])
            {
                BlockingMask.SetActive(true);
            }
        }
    }

   

    void Checker()
    {
        int ShieldPlvl = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD);
        int ShieldDesc = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD) - 1;
        int HpBoostPlvl = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST);
        int HpBoostDesc = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST) - 1;
        int AfterlifePlvl = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.AFTER_LIFE);
        int AfterlifeDesc = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.AFTER_LIFE) - 1;
        int CrystalsPlvl = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.TREASURE_HUNTER);
        int CrystalsDesc = LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.TREASURE_HUNTER) - 1;


        if (ShieldPlvl == 0)
        {
            ShieldPowerUPImage[ShieldPlvl].gameObject.SetActive(false);
            ShieldText[ShieldPlvl].gameObject.SetActive(true);
        }
        else
        {
            ShieldPowerUPImage[ShieldDesc].gameObject.SetActive(true);
            ShieldText[ShieldDesc].gameObject.SetActive(true);
        }

        if (HpBoostPlvl == 0)
        {
            HpBoostPowerUPImage[HpBoostPlvl].gameObject.SetActive(false);
            HpBoostText[HpBoostPlvl].gameObject.SetActive(true);
        }
        else
        {
            HpBoostPowerUPImage[HpBoostDesc].gameObject.SetActive(true);
            HpBoostText[HpBoostDesc].gameObject.SetActive(true);
        }
        if (AfterlifePlvl == 0)
        {
            AfterlifePowerUPImage[AfterlifePlvl].gameObject.SetActive(false);
            AfterlifeText[AfterlifePlvl].gameObject.SetActive(true);
        }
        else
        {
            AfterlifePowerUPImage[AfterlifeDesc].gameObject.SetActive(true);
            AfterlifeText[AfterlifeDesc].gameObject.SetActive(true);
        }
        if (CrystalsPlvl == 0)
        {
            CrystalPowerUPImage[CrystalsPlvl].gameObject.SetActive(false);
            CrystalText[CrystalsPlvl].gameObject.SetActive(true);
        }
        else
        {
            CrystalPowerUPImage[CrystalsDesc].gameObject.SetActive(true);
            CrystalText[CrystalsDesc].gameObject.SetActive(true);
        }
        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD) < 6)
        {
            BuyShieldBtn.gameObject.SetActive(true);
            ShieldMaxedBtn.gameObject.SetActive(false);
        }
        else
        {
            BuyShieldBtn.gameObject.SetActive(false);
            ShieldMaxedBtn.gameObject.SetActive(true);
        }
        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST) < 6)
        {
            BuyHpBoostBtn.gameObject.SetActive(true);
            HpBoostMaxedBtn.gameObject.SetActive(false);
        }
        else
        {
            BuyHpBoostBtn.gameObject.SetActive(false);
            HpBoostMaxedBtn.gameObject.SetActive(true);
        }
        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.AFTER_LIFE) < 6)
        {
            BuyAfterlifeBtn.gameObject.SetActive(true);
            AfterlifeMaxedBtn.gameObject.SetActive(false);
        }
        else
        {
            BuyAfterlifeBtn.gameObject.SetActive(false);
            AfterlifeMaxedBtn.gameObject.SetActive(true);
        }
        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.TREASURE_HUNTER) < 6)
        {
            BuyCrystalBtn.gameObject.SetActive(true);
            CrystalMaxedBtn.gameObject.SetActive(false);
        }
        else
        {
            BuyCrystalBtn.gameObject.SetActive(false);
            CrystalMaxedBtn.gameObject.SetActive(true);
        }
        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD) == 0)
        {
            ShieldPrice.text = powerUpLvlPrices[0].ToString();
        }
        else if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD) < 6)
            ShieldPrice.text = powerUpLvlPrices[ShieldPlvl].ToString();

        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST) == 0)
        {
            HpBoostPrice.text = powerUpLvlPrices[0].ToString();
        }
        else if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST) < 6)
            HpBoostPrice.text = powerUpLvlPrices[HpBoostPlvl].ToString();

        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.AFTER_LIFE) == 0)
        {
            AfterlifePrice.text = powerUpLvlPrices[0].ToString();
        }
        else if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.AFTER_LIFE) < 6)
            AfterlifePrice.text = powerUpLvlPrices[AfterlifePlvl].ToString();
        if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.TREASURE_HUNTER) == 0)
        {
            CrystalPrice.text = powerUpLvlPrices[0].ToString();
        }
        else if (LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.TREASURE_HUNTER) < 6)
            CrystalPrice.text = powerUpLvlPrices[CrystalsPlvl].ToString();

        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.DOUBLE_EXP)
            || LevelManager.Instance.GetActivePassive() == LevelManager.Passive.MAXWELL)
        {
            ExpActiveBtn.gameObject.SetActive(true);
            BuyExpBtn.gameObject.SetActive(false);
        }

        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.COLLECTOR_PET))
        {
            MagnetActiveBtn.gameObject.SetActive(true);
            BuyMagnetBtn.gameObject.SetActive(false);
        }
        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.DOUBLE_CRYSTALS)
            || LevelManager.Instance.GetActivePassive() == LevelManager.Passive.AMORET)
        {
            ShardsActiveBtn.gameObject.SetActive(true);
            BuyShardsBtn.gameObject.SetActive(false);
        }
        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.POWER_START))
        {
            PowerDashActiveBtn.gameObject.SetActive(true);
            BuyPowerDashBtn.gameObject.SetActive(false);
        }
    }

	public enum Chest
	{
		SMALL,
		MEDIUM,
		BIG,
		XXL
	}

    public void GetMoneyChest()
    {
		LevelManager.Instance.AquireInGameMoney (Chest.XXL);
    }
    public void GetMoneyShards()
	{
		LevelManager.Instance.AquireInGameMoney (Chest.SMALL);
    }
    public void GetMoneyBag()
	{
		LevelManager.Instance.AquireInGameMoney (Chest.MEDIUM);
    }
    public void GetMoneyBigBag()
	{
		LevelManager.Instance.AquireInGameMoney (Chest.BIG);
    }


    public void BackButton()
    {
        Application.LoadLevel("StartMenu");
    }

    //activate DoubleExp
    public void ActivateDoubleExp()
    {

        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.DOUBLE_EXP) == false && LevelManager.Instance.GetMoney() >= DoubleExpPrice)
        {
            LevelManager.Instance.AquireConsumable(LevelManager.Consumable.DOUBLE_EXP);
            ExpActiveBtn.gameObject.SetActive(true);
            BuyExpBtn.gameObject.SetActive(false);
            LevelManager.Instance.AddMoney(-DoubleExpPrice);
            //print(LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.DOUBLE_EXP));
        }
        else if (LevelManager.Instance.GetMoney() < DoubleExpPrice)
        {
            BlockingMask.SetActive(true);
        }
    }
    //activate DoubleCry
    public void ActivateDoubleCry()
    {

        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.DOUBLE_CRYSTALS) == false && LevelManager.Instance.GetMoney() >= DoubleShardsPrice)
        {
            LevelManager.Instance.AquireConsumable(LevelManager.Consumable.DOUBLE_CRYSTALS);
            ShardsActiveBtn.gameObject.SetActive(true);
            BuyShardsBtn.gameObject.SetActive(false);
            LevelManager.Instance.AddMoney(-DoubleShardsPrice);
            //print(LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.DOUBLE_CRYSTALS));
        }
        else if (LevelManager.Instance.GetMoney() < DoubleShardsPrice)
        {
            BlockingMask.SetActive(true);
        }

    }

    //activate PowerStart
    public void ActivatePowerStart()
    {

        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.POWER_START) == false && LevelManager.Instance.GetMoney() >= PowerDashPrice)
        {
            LevelManager.Instance.AquireConsumable(LevelManager.Consumable.POWER_START);
            PowerDashActiveBtn.gameObject.SetActive(true);
            BuyPowerDashBtn.gameObject.SetActive(false);
            LevelManager.Instance.AddMoney(-PowerDashPrice);
        }
        else if (LevelManager.Instance.GetMoney() < PowerDashPrice)
        {
            BlockingMask.gameObject.SetActive(true);
        }
    }
    //activate CollectorPet
    public void ActivateCollectorPet()
    {

        if (LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.COLLECTOR_PET) == false && LevelManager.Instance.GetMoney() >= MagnetPrice)
        {
            LevelManager.Instance.AquireConsumable(LevelManager.Consumable.COLLECTOR_PET);
            MagnetActiveBtn.gameObject.SetActive(true);
            BuyMagnetBtn.gameObject.SetActive(false);
            LevelManager.Instance.AddMoney(-MagnetPrice);
            print(LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.COLLECTOR_PET));
        }
        else if (LevelManager.Instance.GetMoney() < MagnetPrice)
        {
            BlockingMask.gameObject.SetActive(true);
        }
    }

    public void DeactivateBlockingMask()
    {
        BlockingMask.SetActive(false);
    }

   /* private void GetOwnedSkin()
    {
        int index = LevelManager.Instance.GetItemIndex(LevelManager.Instance.);
        LevelManager.Instance.GetOwnedSkins();
    }
    */
    public void AnimationTextChecker()
    {
        if(shieldvaluetxt.gameObject.activeInHierarchy || afterlifevaluetxt.gameObject.activeInHierarchy || mashinevaluetxt.gameObject.activeInHierarchy || bigcrystaltxt.gameObject.activeInHierarchy)
        {
            shieldvaluetxt.gameObject.SetActive(false);
            afterlifevaluetxt.gameObject.SetActive(false);
            mashinevaluetxt.gameObject.SetActive(false);
            bigcrystaltxt.gameObject.SetActive(false);
        }
    }

    public void StartFromBlockingMask()
    {
        Application.LoadLevel("Main");
    }
  
}


