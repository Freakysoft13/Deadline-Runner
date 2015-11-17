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
        int currentExp = LevelManager.Instance.GetExp();
        int currentLevel = LevelManager.Instance.GetLevel();
        int expEarned = GameManager.Instance.Player.Exp;
        int[] levelsExp = LevelManager.Instance.levelsExp;
        int overallLevelExp = 0;
        for (int i = 0; i < currentLevel + 1; i++) {
            overallLevelExp += levelsExp[i];
        }
        expSlider.value = (currentExp - overallLevelExp) / (float)levelsExp[currentLevel + 1];
        for (int i = currentLevel; i < levelsExp.Length - 1; i++) {
            if (currentExp + expEarned > levelsExp[i + 1]) {
                levelsAcquired++;
            }
            else {
                break;
            }
        }
        if (levelsAcquired > 0) {
            expEarned -= levelsExp[levelsAcquired] - currentExp;
        }

        for (int i = 0; i < currentLevel + 1 + levelsAcquired; i++) {
            overallLevelExp += levelsExp[i];
        }
        int previousLevelExp = currentLevel > 0 ? levelsExp[currentLevel - 1] : 0;
        if (currentLevel >= 0 && currentLevel < 9) {
            targetValue = (currentExp + expEarned - overallLevelExp) / (float)(levelsExp[currentLevel + 1 + levelsAcquired] + previousLevelExp);
            LevelManager.Instance.AddExp(GameManager.Instance.Player.Exp);
            updateUI = true;
        }
    }

    void Update() {
        if (!updateUI) { return; }
        if (levelsAcquired > 0) {
            if (Mathf.Abs(expSlider.value - 1f) > fillPrecision) {
                expSlider.value += (1.0f / (int)(1 / (fillWaitTime * 1f))) * Time.deltaTime;
            }
            else {
                levelsAcquired--;
                expSlider.value = 0.0f;
                EventManager.Instance.FireLevelUp();
            }
            return;
        }
        if (Mathf.Abs(expSlider.value - targetValue) > fillPrecision) {
            expSlider.value += (1.0f / (int)(1 / (fillWaitTime * targetValue))) * Time.deltaTime;
        }
        else {
            updateUI = false;
            if (Mathf.Abs(expSlider.value - 1) < fillPrecision) {
                EventManager.Instance.FireLevelUp();
                expSlider.value = 0;
            }
        }
    }
}
