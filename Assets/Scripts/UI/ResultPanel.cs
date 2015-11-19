using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultPanel : MonoBehaviour
{
    public Slider expSlider;
    public float fillWaitTime = 100;
    public float fillPrecision = 0.01f;
    public float updateDelay = 0.001f;

    private float targetValue;
    private int levelsAcquired = 0;
    private bool updateExpBar = false;
    private int expEarnedCounter;
    private int crystalsCollectedCounter;
    private int expEarned;
    private int crystalsCollected;
    private float lastExpUpdateTime;
    private float lastCryUpdateTime;

    public Text levelUp_txt;
    public Text level_txt;

    public Text bestscore;
    public Text collectedCrystals;
    public Text unlockText;
    public Text score;


    void Start() {
        EventManager.Instance.OnLevelUp += delegate () {
            levelUp_txt.gameObject.SetActive(true);
            level_txt.text = "Level " + (1 + LevelManager.Instance.GetLevel());
        };
    }

    void OnEnable() {
        if (LevelManager.Instance == null || GameManager.Instance == null || GameManager.Instance.Player == null) { return; }
        levelUp_txt.gameObject.SetActive(false);
        level_txt.text = "Level " + (1 + LevelManager.Instance.GetLevel());
        expEarned = GameManager.Instance.Player.Exp;
        crystalsCollected = GameManager.Instance.GetScore();
        int bestScore = LevelManager.Instance.GetBestScore();
        if (expEarned > bestScore) {
            bestScore = expEarned;
            LevelManager.Instance.SaveBestScore(bestScore);
        }
        bestscore.text = "Best score: " + bestScore;
        CalcLevelProgress();
    }

    void Update() {
        UpdateExp();
        UpdateCounters();
    }

    private void UpdateCounters() {
        if(expEarnedCounter < expEarned && Mathf.Abs(Time.time - lastExpUpdateTime) > updateDelay) {
            expEarnedCounter++;
            score.text = "Score: " + expEarnedCounter;
            lastExpUpdateTime = Time.time;
        }
        if (crystalsCollectedCounter < crystalsCollected && Mathf.Abs(Time.time - lastCryUpdateTime) > updateDelay) {
            crystalsCollectedCounter++;
            collectedCrystals.text = "Crystals collected: " + crystalsCollectedCounter;
            lastCryUpdateTime = Time.time;
        }
    }

    private void UpdateExp() {
        if (!updateExpBar) { return; }
        if (Mathf.Abs(expSlider.value - targetValue) > fillPrecision) {
            expSlider.value += (1.0f / (int)(1 / (fillWaitTime * targetValue))) * Time.deltaTime;
        }
        else {
            updateExpBar = false;
            if (Mathf.Abs(expSlider.value - 1) < fillPrecision) {
                EventManager.Instance.FireLevelUp();
                expSlider.value = 0;
            }
            if (overhead > 0) {
                CalcLevelProgress();
            }
        }
    }

    private int overhead = 0;

    private void CalcLevelProgress() {
        int currentExp = LevelManager.Instance.GetExpThisLevel();
        int currentLevel = LevelManager.Instance.GetLevel();
        int expEarned = overhead > 0 ? overhead : GameManager.Instance.Player.Exp;
        int[] levelsExp = LevelManager.Instance.levelsExp;
        if (true) { // change to current level < MAX_LEVEL
            int expToNextLevel = levelsExp[currentLevel + 1];
            if (overhead == 0) {
                expSlider.value = currentExp / (float)expToNextLevel;
            }
            if (expEarned + currentExp > expToNextLevel) {
                overhead = expEarned - (expToNextLevel - currentExp);
            }
            else {
                overhead = 0;
            }
            int remainder = expEarned - overhead;
            targetValue = (remainder + currentExp) / (float)expToNextLevel;
            LevelManager.Instance.AddExp(remainder);
            updateExpBar = true;
        }
    }
}
