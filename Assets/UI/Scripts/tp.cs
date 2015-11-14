using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tp : MonoBehaviour
{
    public GameObject pause_pnl;
    public GameObject blockingmask;
    public GameObject dailyreward;
    public GameObject scoreboard;
    public GameObject fortunewheel;
    public GameObject deathpnl;
    public GameObject result_pnl;

    public Text Money_txt;
    public Text Distance_txt;
    //player object for distance meeter
    public GameObject Player;
    public int distance = 8;
    public Text CrystalSumm;

    AudioSource audios;
    public Image audioOn_img;
    public Image audioOff_img;

    public GameObject money_mask;
    public int[] ress_price;
    public Text ress_txt;
    private int priceindex = 0;
    private bool ressurected_with_ads;
    public GameObject ress_ads_btn;

    public GameObject[] unlockedAncestors;

    void Start() {
        // ress_ads_btn.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
        //ressurected_with_ads = false;

        audios = GetComponent<AudioSource>();

        pause_pnl.SetActive(false);
        blockingmask.SetActive(false);
        dailyreward.SetActive(false);
        scoreboard.SetActive(true);
        fortunewheel.SetActive(false);
        deathpnl.SetActive(false);
        result_pnl.SetActive(false);

        Distance_txt.text = distance.ToString();

        audioOff_img.gameObject.SetActive(false);
        audioOn_img.gameObject.SetActive(true);
        money_mask.gameObject.SetActive(false);
        CrystalSumm.text = LevelManager.Instance.GetMoney().ToString();
    }

    void Update() {
        distance_counter();
        CrystalSumm.text = GameManager.Instance.GetScore().ToString();
    }

    //pause_btn need to be cleaned
    public void Pause() {
        deathpnl.SetActive(false);
        pause_pnl.SetActive(true);
        blockingmask.SetActive(false);
        dailyreward.SetActive(false);
        scoreboard.SetActive(true);
        fortunewheel.SetActive(false);

        Time.timeScale = 0;
    }
    // turn of sound_btn
    public void AudioMute() {
        if (audios.mute == true) {
            audios.mute = false;
            audioOn_img.gameObject.SetActive(true);
            audioOff_img.gameObject.SetActive(false);
        }
        else {
            audios.mute = true;
            audioOff_img.gameObject.SetActive(true);
            audioOn_img.gameObject.SetActive(false);
        }
    }
    // resume after pause need to be cleaned
    public void Resume() {
        deathpnl.SetActive(false);
        pause_pnl.SetActive(false);
        blockingmask.SetActive(false);
        dailyreward.SetActive(false);
        scoreboard.SetActive(true);
        fortunewheel.SetActive(false);

        Time.timeScale = 1;
    }
    //redirecting to main menu
    public void Restart_to_menu() {
        Time.timeScale = 1;
        Application.LoadLevel("StartMenu");
    }
    //test btn fortune wheel need to be cleaned
    public void Fortune() {
        deathpnl.SetActive(false);
        pause_pnl.SetActive(false);
        blockingmask.SetActive(true);
        dailyreward.SetActive(false);
        scoreboard.SetActive(false);
        fortunewheel.SetActive(true);
    }

    // counting distanve for gameobject, gameobject need to be seted in menu
    private void distance_counter() {
        int newdistance;
        newdistance = distance + 1 * ((int)Player.transform.position.x);
        Distance_txt.text = newdistance.ToString() + " m";
    }

    // test_money mask can be replaced with prefab from shop
    void Money_mask() {
        money_mask.gameObject.SetActive(true);
    }
    //redirect link leads to special pnl in shop(if using other mask prefab add this to mask GetCrystal btn)
    public void get_crystals() {
        LevelManager.Instance.SetShopID(1);
        Application.LoadLevel("Shop");
    }
    //closes money mask(if maske replaced add this to new mask close btn)
    public void close_moneymask() {
        money_mask.gameObject.SetActive(false);
    }
    // ress button for money ress no actuall ress is added only money part
    public void ressurect() {
        ress_txt.text = ress_price.ToString();

        if (LevelManager.Instance.GetMoney() >= ress_price[priceindex]) {
            LevelManager.Instance.AddMoney(-ress_price[priceindex]);

            if (priceindex <= 1) {
                priceindex++;
            }
            print("a");
            if (priceindex <= 2) {
                ress_txt.text = ress_price[priceindex].ToString();
                print("b");
            }
            else {
                ress_txt.text = ress_price[2].ToString();
            }
            print(priceindex);
            Player.GetComponent<Player>().Ressurect();
            deathpnl.SetActive(false);
            scoreboard.SetActive(true);
        }
        else {
            ress_txt.text = ress_price[priceindex].ToString();
            Money_mask();
        }

    }
    // not currently used need to be added to unity ads script
    public void ressurect_video() {
        if (ressurected_with_ads == false) {
            //resscode
            ressurected_with_ads = true;
        }
        else {
            ress_ads_btn.gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
            print("Hi");
        }
    }
    public void RestartGameOnDeath() {
        Application.LoadLevel("Main");
    }
    public void RessurectWithAds() {
        Player.GetComponent<Player>().Ressurect();
        deathpnl.SetActive(false);
        scoreboard.SetActive(true);
    }
    public void closeRessWindow()
    {
        Result();
    }

    void Result ()
    {
        scoreboard.SetActive(false);
        result_pnl.SetActive(true);
    }

}
