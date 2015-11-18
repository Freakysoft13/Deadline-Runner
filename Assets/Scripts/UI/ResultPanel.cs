using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultPanel : MonoBehaviour
{
    public Slider expSlider;
    public float fillWaitTime = 100;
    public float fillPrecision = 0.01f;

    private float targetValue;
    private int levelsAcquired = 0;
    private bool updateUI = false;

    public Text levelUp_txt;
    public Text level_txt;


    void Start() {
        levelUp_txt.gameObject.SetActive(false);
        level_txt.text = "Level " + (1 + LevelManager.Instance.GetLevel());
        EventManager.Instance.OnLevelUp += delegate () {
            levelUp_txt.gameObject.SetActive(true);
            level_txt.text = "Level " + (1 + LevelManager.Instance.GetLevel());
        };
    }

    void OnEnable() {
        if (LevelManager.Instance == null || GameManager.Instance == null || GameManager.Instance.Player == null) { return; }
        CalcLevelProgress();
    }

    void Update() {
        if (!updateUI) { return; }
        if (Mathf.Abs(expSlider.value - targetValue) > fillPrecision) {
            expSlider.value += (1.0f / (int)(1 / (fillWaitTime * targetValue))) * Time.deltaTime;
        }
        else {
            updateUI = false;
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
        int currentExp = LevelManager.Instance.GetExp();
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
            updateUI = true;
        }
    }
}
