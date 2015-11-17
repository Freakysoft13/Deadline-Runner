using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public GameObject[] ancestors;
    public Animator[] gearAnimators;
    private const string last_level = "last_level";
    private const string ancestor_level = "ancestor_level";
    public Image[] canalImages;
    public GameObject[] unlocked_ancestor_btn;
    public int[] ancestorLevels;


    // що це робить?(і що таке const string)
    private static AnimationManager instance;
    public static AnimationManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;

        int ancestorLevel = GetLastOpenedAncestor();
        for (int i = 0; i < ancestorLevel + 1; i++)
        {
            //як замутити, шоб не вирубати ан1матор?
            canalImages[i].GetComponent<Animator>().enabled = false;
            canalImages[i].fillAmount = 1.0f;
            unlocked_ancestor_btn[i].SetActive(true);
            LockAncestor(i);
        }
    }

    void Start()
    {
        InitiateAncestorLevelCheck();
    }

    private void InitiateAncestorLevelCheck()
    {
        if (ShouldPlayNewLevelAnimation())
        {
            TriggerStart();
            int level = LevelManager.Instance.GetLevel();
            int ancestorLevel = GetAncestorLevel();
            if (ancestorLevel <= level)
            {
                FillCanal(ancestorLevel);
            } else
            {
                SaveLastEntryLevel(LevelManager.Instance.GetLevel());
            }
        }
    }

    public void TriggerStart()
    {
        for (int i = 0; i < gearAnimators.Length; i++)
        {
            gearAnimators[i].SetTrigger("Spin");
        }
    }


    public void SpinGear(int index)
    {
        gearAnimators[index].SetTrigger("Spin");
    }

    private bool ShouldPlayNewLevelAnimation()
    {
        int currentLevel = GetAncestorLevel();
        if (currentLevel > GetLastOpenedAncestor())
        {
            return true;
        }
        return false;
    }

    private int GetLastEntryLevel()
    {
        return PlayerPrefs.GetInt(last_level, -1);
    }

    private void SaveLastEntryLevel(int level)
    {
        PlayerPrefs.SetInt(last_level, level);
    }

    private void SaveOpenedAncestor(int level)
    {
        PlayerPrefs.SetInt(ancestor_level, level);
    }

    private int GetLastOpenedAncestor()
    {
        return PlayerPrefs.GetInt(ancestor_level, -1);
    }

    private int GetAncestorLevel()
    {
        int level = LevelManager.Instance.GetLevel();
        int ancestorLevel = 0;
        for (int i = 0; i < ancestorLevels.Length; i++)
        {
            if(level >= ancestorLevels[i])
            {
                ancestorLevel = i;
                if(ancestorLevel + 1 > GetLastOpenedAncestor())
                {
                    break;
                }
            } else
            {
                break;
            }
        }
        return ancestorLevel;
    }

    public void FillCanal(int index)
    {
        canalImages[index].GetComponent<Animator>().SetTrigger("Fill");
    }

    public void OpenAncestor()
    {       
        int index = GetAncestorLevel();
        Animator[] topBottomPanels = ancestors[index].GetComponentsInChildren<Animator>();
        topBottomPanels[0].SetTrigger("Open");
        topBottomPanels[1].SetTrigger("Open");
        SkeletonAnimation skelAnim = ancestors[index].GetComponentInChildren<SkeletonAnimation>();
        skelAnim.AnimationName = "animation";
        unlocked_ancestor_btn[index].SetActive(true);
        SaveOpenedAncestor(index);
        StopAllGears();
        SaveLastEntryLevel(index);
        InitiateAncestorLevelCheck();
    }

    private void StopAllGears()
    {
        foreach (Animator a in gearAnimators)
        {
            a.SetTrigger("Stop");
        }
    }

    private void LockAncestor(int index)
    {
        int i = 0;
        foreach(Transform t in ancestors[index].transform.GetChild(0))
        {
            if (i == 1 || i == 2)
            {
                t.gameObject.SetActive(false);
            }
            i++;
        }
    }

    private int GetAncestorIndex()
    {
        int index = LevelManager.Instance.GetItemIndex(LevelManager.Instance.GetActivePassive());
        return index;
    }

    
}
