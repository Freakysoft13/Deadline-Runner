using UnityEngine;
using UnityEngine.UI;
using System;

public class RewardManager : MonoBehaviour
{
    private const string LAST_OPENED_CHEST_ID = "chest_id";
    private const string TIME_OF_OPENING = "time";
    private const string TIME_FORMAT = "HH:mm:ss";
    private const int COOLDOWN_HOURS = 24;


    public GameObject[] buttons;
    public float[] timers;
    public int[] rewards;
    public GameObject[] lockedImg;
    public GameObject[] unlockedImg;
    public Text timer;
    public Text timer2;
    public Text rewardText;
    public GameObject rewardPanel;

    private int lastOpenedChestId;
    private DateTime timeOfOpening;
    private DateTime timeOfNextOpening;
    private bool shouldPerform = true;

    void Start()
    {
        Reinitialize();
    }
    private void Reinitialize()
    {

        lastOpenedChestId = PlayerPrefs.GetInt(LAST_OPENED_CHEST_ID, -1);
        if (lastOpenedChestId == -1)
        {
            shouldPerform = false;
        }
        if (!DateTime.TryParseExact(PlayerPrefs.GetString(TIME_OF_OPENING, ""), TIME_FORMAT,
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None,
                                       out timeOfOpening))
        {
            shouldPerform = false;
        }
        timer.gameObject.SetActive(shouldPerform);
        timer2.gameObject.SetActive(shouldPerform);
        if (!shouldPerform)
        {
            return;
        }
        for (int i = lastOpenedChestId + 1; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            //тут закриває лєві іконки
        }
        //тут врубається чест який ти відрив вже.
        buttons[lastOpenedChestId].SetActive(true);
        timeOfNextOpening = IsOnCooldown() ? timeOfOpening.AddHours(COOLDOWN_HOURS) : timeOfOpening.AddMinutes(timers[lastOpenedChestId + 1]);
    }

    private bool IsOnCooldown()
    {
        return lastOpenedChestId == buttons.Length - 1;
    }

    void Update()
    {
        if (!shouldPerform) { return; }
        TimeSpan timeSinceLastOpening = (DateTime.Now - timeOfOpening);
        if (IsOnCooldown() || timeSinceLastOpening.TotalMinutes < timers[lastOpenedChestId + 1])
        {
            TimeSpan timeUntilNextOpening = (timeOfNextOpening - DateTime.Now);
            timer.text = " " + DateTime.MinValue.Add(timeUntilNextOpening).ToString(TIME_FORMAT);
            timer2.text = "Next Reward In : " + DateTime.MinValue.Add(timeUntilNextOpening).ToString(TIME_FORMAT);
        }
        if (IsOnCooldown() ||timeSinceLastOpening.TotalMinutes > timers[lastOpenedChestId + 1]
            && !buttons[lastOpenedChestId + 1].activeSelf)
        {
            if (IsOnCooldown())
            {
                PlayerPrefs.SetInt(LAST_OPENED_CHEST_ID, -1);
                Reinitialize();
            }
            else
            {
                buttons[lastOpenedChestId + 1].SetActive(true);
            }
            //тут врубається коли закінчився таймер
        }
    }

    public void Click(int btnId)
    {
        if (btnId <= lastOpenedChestId)
        {
            Debug.Log("Cheat!");
            return;
        }

        int rewardedAmt = rewards[btnId];
        LevelManager.Instance.AddMoney(rewardedAmt);
        Debug.Log("You are rewarded with " + rewardedAmt + " crystals!");
        PlayerPrefs.SetInt(LAST_OPENED_CHEST_ID, btnId);
        PlayerPrefs.SetString(TIME_OF_OPENING, DateTime.Now.ToString(TIME_FORMAT));
        shouldPerform = true;
        //rewardText.text = "Earned " + rewards[btnId] + " Crystal Shards";
        //rewardPanel.SetActive(true);
        Reinitialize();
    }
}
