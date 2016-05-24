using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.Advertisements;

public class ResultPanel : MonoBehaviour
{
    public Slider expSlider;
    public float fillWaitTime = 100;
    public float fillPrecision = 0.01f;
    public float updateDelay = 0.001f;

    public GameObject rateUsPanel;

    private float targetValue;
    private int levelsAcquired = 0;
    private bool updateExpBar = false;
    private float expEarnedCounter;
    private float crystalsCollectedCounter;
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

    public GameObject[] ancestorsImg;
    public GameObject lockancestors;
    public GameObject ancesorButton;
    public GameObject adButton;
    public GameObject adImage;
    public Animator lockImg;
    public Animator unlockImg;

    private String LevelLoc;
    private String bestscoreLoc;
    private String collectedCryLoc;
    private String scoreLoc;
    private String unlockLoc;
    private AudioSource expGain;
    private AudioSource levelUp;

    private bool hasGrantedAchievements = false;

    void Awake()
    {
        if (Application.systemLanguage.ToString() == "Russian")
        {
            LevelLoc = "Уровень ";
            bestscoreLoc = "Лучший результат: ";
            collectedCryLoc = "Собрано Осколков: ";
            scoreLoc = "Очки: ";
            unlockLoc = "Открывается на уровне ";
        }
        else if (Application.systemLanguage.ToString() == "Ukrainian")
        {
            LevelLoc = "Рівень ";
            bestscoreLoc = "Кращий результат: ";
            collectedCryLoc = "Зібрано Кристалів: ";
            scoreLoc = "Очки: ";
            unlockLoc = "Відкривається на рівні ";
        }
        else
        {
            LevelLoc = "Level ";
            bestscoreLoc = "Best Score: ";
            collectedCryLoc = "Crystals collected: ";
            scoreLoc = "Score: ";
            unlockLoc = "Unlock on Level ";
        }
    }
    void Start()
    {
        EventManager.OnLevelUp += delegate ()
        {
            levelUp_txt.gameObject.SetActive(true);
            level_txt.text = LevelLoc + (1 + LevelManager.Instance.GetLevel());
            lockancestors.SetActive(false);
            ancesorButton.SetActive(true);
            unlockText.gameObject.SetActive(false);
            lockImg.Play("LockedIcon");
            unlockImg.gameObject.SetActive(true);
            unlockImg.Play("UnlockedIcon");
            //отут левел ап
        };
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length == 2)
        {
            expGain = sources[0];
            levelUp = sources[1];
        }
#if UNITY_WSA
        if (!AdsManager.Instance.IsAdReady)
        {
            adButton.SetActive(false);
            adImage.SetActive(false);
        }
#endif
#if UNITY_ANDROID
        if (!Advertisement.IsReady())
        {
            adButton.SetActive(false);
            adImage.SetActive(false);
        }

        int alreadyRated = PlayerPrefs.GetInt("already_rated", 0);
        if (alreadyRated == 0)
        {
            Invoke("TryShowRateUs", 0.5f);
        }
#endif
    }
#if UNITY_ANDROID
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    public int[] rateUsPopTimes;

    private void TryShowRateUs()
    {
        int triesAmt = PlayerPrefs.GetInt("tries_amt", 0);
        int rateUsPopAmt = PlayerPrefs.GetInt("rate_us_pop_amt", 0);
        PlayerPrefs.SetInt("tries_amt", ++triesAmt);
        if (triesAmt >= rateUsPopTimes[rateUsPopAmt])
        {
            PlayerPrefs.SetInt("rate_us_pop_amt", Mathf.Clamp(++rateUsPopAmt, 0, rateUsPopTimes.Length - 1));
            rateUsPanel.SetActive(true);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                GameManager.Instance.AddCrystals((GameManager.Instance.GetCrystals() * 2));
                collectedCrystals.text = collectedCryLoc + crystalsCollectedCounter * 2;
                LevelManager.Instance.AddMoney(crystalsCollected);
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
#endif

    void OnEnable()
    {
        bool isMaxLevel = LevelManager.Instance.IsMaxLevel();
        lockancestors.SetActive(!isMaxLevel);
        ancesorButton.SetActive(false);
        unlockText.gameObject.SetActive(true);
        unlockText.text = isMaxLevel ? "" : unlockLoc + (LevelManager.Instance.GetLevel() + 2);
        ancestorsImg[Mathf.Clamp(LevelManager.Instance.GetLevel(), 0, 10)].SetActive(true);
        if (LevelManager.Instance == null || GameManager.Instance == null || GameManager.Instance.Player == null) { return; }
        levelUp_txt.gameObject.SetActive(false);
        level_txt.text = LevelLoc + (1 + LevelManager.Instance.GetLevel());
        expEarned = GameManager.Instance.Player.Exp;
        crystalsCollected = GameManager.Instance.GetCrystals();
        int bestScore = LevelManager.Instance.GetBestScore();
        if (expEarned > bestScore)
        {
            bestScore = expEarned;
            LevelManager.Instance.SaveBestScore(bestScore);
#if UNITY_ANDROID
            GooglePlayServices.Instance.ReportScore(GPGIds.leaderboard_best_score, bestScore);
#endif
        }
#if UNITY_ANDROID
        GooglePlayServices.Instance.ReportScore(GPGIds.leaderboard_distance, GameManager.Instance.Player.GetDistance());
#endif
        bestscore.text = bestscoreLoc + bestScore;
        CalcLevelProgress();
        LevelManager.Instance.AddMoney(crystalsCollected);
    }

    void Update()
    {
        UpdateExp();
        UpdateCounters();
    }

    public void GrantAchievements()
    {
#if UNITY_ANDROID
        float distanceTraveled = GameManager.Instance.Player.GetDistance();
        float crystalsCollected = LevelManager.Instance.GetTotalMoney();
        if (distanceTraveled > 1999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_runner, 100.0f);
        }
        if (distanceTraveled > 4999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_long_distance_runner, 100.0f);
        }
        if (crystalsCollected > 99)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_something_shiny, 100.0f);
        }
        if (crystalsCollected > 4999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_gemology, 100.0f);
        }
        if (crystalsCollected > 49999)
        {
            GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_jeweler, 100.0f);
        }
#endif
    }

    public void RateUs()
    {
        PlayerPrefs.SetInt("already_rated", 1);
        rateUsPanel.SetActive(false);
        Application.OpenURL("market://details?id=com.freakysoft.deadlinerunner/");
    }

    public void Cancel()
    {
        rateUsPanel.SetActive(false);
    }
    private void UpdateCounters()
    {
        if (expEarnedCounter < expEarned && Mathf.Abs(Time.time - lastExpUpdateTime) > updateDelay)
        {
            float divisor = expEarned / 10.0f;
            if ((expEarnedCounter + (expEarned / divisor)) <= expEarned)
            {
                expEarnedCounter += expEarned / divisor;
            }
            else
            {
                expEarnedCounter = expEarned;
            }
            score.text = scoreLoc + (int)expEarnedCounter;
            lastExpUpdateTime = Time.time;
        }
        if (crystalsCollectedCounter < crystalsCollected && Mathf.Abs(Time.time - lastCryUpdateTime) > updateDelay)
        {
            float divisor = crystalsCollected / 10.0f;
            if ((crystalsCollectedCounter + (crystalsCollected / divisor)) <= crystalsCollected)
            {
                crystalsCollectedCounter += crystalsCollected / divisor;
            }
            else
            {
                crystalsCollectedCounter = crystalsCollected;
            }
            collectedCrystals.text = collectedCryLoc + crystalsCollectedCounter;
            lastCryUpdateTime = Time.time;
        }
    }

    private void UpdateExp()
    {
        if (!updateExpBar) { return; }
        if (Mathf.Abs(expSlider.value - targetValue) > fillPrecision)
        {
            expSlider.value += (1.0f / (int)(1 / (fillWaitTime * targetValue))) * Time.deltaTime;
            if (!expGain.isPlaying)
            {
                expGain.Play();
            }
        }
        else
        {
            updateExpBar = false;
            if (Mathf.Abs(expSlider.value - 1) < fillPrecision && !LevelManager.Instance.IsMaxLevel())
            {
                EventManager.FireLevelUp();
                levelUp.Play();
                expSlider.value = 0;
            }
            if (overhead > 0)
            {
                CalcLevelProgress();
            }
            else
            {
                if (!hasGrantedAchievements)
                {
                    hasGrantedAchievements = true;
                    GrantAchievements();
                }
            }

            if (expGain.isPlaying)
            {
                expGain.Stop();
            }
        }
    }

    private int overhead = 0;

    private void CalcLevelProgress()
    {
        int currentExp = LevelManager.Instance.GetExpThisLevel();
        int currentLevel = LevelManager.Instance.GetLevel();
        int expEarned = overhead > 0 ? overhead : GameManager.Instance.Player.Exp;
        int[] levelsExp = LevelManager.Instance.levelsExp;
        if (currentLevel < 10)
        {
            int expToNextLevel = levelsExp[currentLevel + 1];
            if (overhead == 0)
            {
                expSlider.value = currentExp / (float)expToNextLevel;
            }
            if (expEarned + currentExp > expToNextLevel)
            {
                overhead = expEarned - (expToNextLevel - currentExp);
            }
            else
            {
                overhead = 0;
            }
            int remainder = expEarned - overhead;
            targetValue = (remainder + currentExp) / (float)expToNextLevel;
            LevelManager.Instance.AddExp(remainder);
            updateExpBar = true;
        }
    }
    //Video Ads for crystals
    public void CrystalsVideoBtn()
    {
#if UNITY_WSA
        Action<object> onAdCompleted = (arg) =>
        {
            if (arg == null || (arg != null && (arg is AdFinishedEventArgs) && ((AdFinishedEventArgs)arg).IsCompletedView))
            {
                GameManager.Instance.AddCrystals((GameManager.Instance.GetCrystals() * 2));
                collectedCrystals.text = collectedCryLoc + crystalsCollectedCounter * 2;
                LevelManager.Instance.AddMoney(crystalsCollected);
            }
        };
        AdsManager.Instance.ShowVideo(onAdCompleted);
#endif
        //ADS
#if UNITY_ANDROID
        ShowRewardedAd();
#endif
        adImage.SetActive(false);
        adButton.SetActive(false);

    }
    public void GPGRating()
    {
        Application.OpenURL("http://play.google.com/store/apps/details?id=" + Application.bundleIdentifier);
    }
}
