﻿using UnityEngine;
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
    public GameObject rewardAnimator;

    private int lastOpenedChestId;
    private int nextChestId;
    private DateTime timeOfNextOpening;
    private bool shouldPerform = true;

    void Start() {
        Initialize();
    }

    private bool IsOnCooldown(int lastOpenedChestId) {
        return lastOpenedChestId == buttons.Length - 1;
    }

    private void Initialize() {
        int lastOpenedChestId = PlayerPrefs.GetInt(LAST_OPENED_CHEST_ID, -1);
        for(int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(false);
            lockedImg[i].SetActive(true);
            unlockedImg[i].SetActive(false);
        }
        DateTime timeOfOpening;
        if (!DateTime.TryParseExact(PlayerPrefs.GetString(TIME_OF_OPENING, ""), TIME_FORMAT,
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None,
                                       out timeOfOpening)) {
            shouldPerform = false;
        }
        if (IsOnCooldown(lastOpenedChestId)) {
            timeOfNextOpening = timeOfOpening.AddHours(COOLDOWN_HOURS);
            nextChestId = 0;
            return;
        }
        nextChestId = lastOpenedChestId + 1;
        if (lastOpenedChestId == -1) {
            ActivateChest(0);
            return;
        }
        timeOfNextOpening = timeOfOpening.AddMinutes(timers[nextChestId]);
        for (int i = 0; i < lastOpenedChestId + 1; i++) {
            lockedImg[i].SetActive(false);
            unlockedImg[i].SetActive(true);
        }
    }

    private void ActivateChest(int id) {
        buttons[id].SetActive(true);
        timer.text = "";
        timer2.text = "";
    }

    void Update() {
        if (!shouldPerform) return;
        if (timeOfNextOpening > DateTime.Now) {
            TimeSpan timeUntilNextOpening = (timeOfNextOpening - DateTime.Now);
            timer.text = " " + DateTime.MinValue.Add(timeUntilNextOpening).ToString(TIME_FORMAT);
            timer2.text = "Next Reward In : " + DateTime.MinValue.Add(timeUntilNextOpening).ToString(TIME_FORMAT);
            if (rewardAnimator.activeInHierarchy) {
                rewardAnimator.SetActive(false);
            }
        }
        else {
            ActivateChest(nextChestId);
            if (!rewardAnimator.activeInHierarchy) {
                rewardAnimator.SetActive(true);
            }
        }
    }

    public void Click(int btnId) {
        int lastChestId = lastOpenedChestId == -1 ? 0 : lastOpenedChestId;
        if (btnId < lastOpenedChestId) {
            Debug.Log("Cheat!");
            return;
        }

        int rewardedAmt = rewards[btnId];
        LevelManager.Instance.AddMoney(rewardedAmt);
        Debug.Log("You are rewarded with " + rewardedAmt + " crystals!");
        PlayerPrefs.SetInt(LAST_OPENED_CHEST_ID, btnId);
        PlayerPrefs.SetString(TIME_OF_OPENING, DateTime.Now.ToString(TIME_FORMAT));
        shouldPerform = true;
        lockedImg[btnId].SetActive(false);
        unlockedImg[btnId].SetActive(true);
        rewardText.text = "Earned " + rewards[btnId] + " Crystal Shards";
        rewardPanel.SetActive(true);
        Initialize();
    }
}
