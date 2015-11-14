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

    void OnEnable() {
        if(LevelManager.Instance == null || GameManager.Instance == null || GameManager.Instance.Player == null) { return; }
        int currentExp = LevelManager.Instance.GetExp();
        int currentLevel = LevelManager.Instance.GetLevel();
        int expEarned = GameManager.Instance.Player.Exp;
        int[] levelsExp = LevelManager.Instance.levelsExp;
        int overallLevelExp = 0;
        for(int i = 0; i < currentLevel + 1; i++) {
            overallLevelExp += levelsExp[i];
        }
        expSlider.value = (currentExp - overallLevelExp) / (float)levelsExp[currentLevel + 1];
        for (int i = currentLevel; i < levelsExp.Length - 1; i++) {
            if (expEarned > levelsExp[currentLevel + 1]) {
                levelsAcquired++;
            } else {
                break;
            }
        }
        if (currentLevel >= 0 && currentLevel < 9) {
            targetValue = (currentExp + expEarned - overallLevelExp) / (float)levelsExp[currentLevel + 1];
            updateUI = true;
        }
    }

    void Update() {
        if(!updateUI) { return; }
        if(levelsAcquired > 0) {
            if (Mathf.Abs(expSlider.value - 1f) > fillPrecision) {
                expSlider.value += (1.0f / (int)(1 / (fillWaitTime * 1f))) * Time.deltaTime;
            }
            else {
                levelsAcquired--;
                expSlider.value = 0.0f;
            }
            return;
        }
        if (Mathf.Abs(expSlider.value - targetValue) > fillPrecision) {
            expSlider.value += (1.0f / (int)( 1 / (fillWaitTime* targetValue))) * Time.deltaTime;
        }
        else {
            if(Mathf.Abs(expSlider.value - 1) < fillPrecision) {
                //level up
                expSlider.value = 0;
            }
            updateUI = false;
            LevelManager.Instance.AddExp(GameManager.Instance.Player.Exp);
        }
    }
}
