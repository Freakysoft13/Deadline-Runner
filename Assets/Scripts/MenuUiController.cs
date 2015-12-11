using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUiController : MonoBehaviour
{

    public GameObject[] ancestorPortrait;
    public GameObject[] ancestorText;
    public Image soundOn;
    public Image soundOff;


    //timescale factor excluding freeze bug
    void Start()
    {
        SoundBtnRoute();
        IndexChecker();
        Time.timeScale = 1;
    }
    //Load Game Scene (not active yet)
    public void StartGame()
    {
        Application.LoadLevel("Main");
    }

    //Load Shop Scene btn
    public void Shop()
    {
        Application.LoadLevel("Shop");
    }

    //Quit btn
    public void EndGame()
    {
        Application.Quit();
    }

    //load Tree_Scene btn
    public void tree()
    {
        Application.LoadLevel("TreeRevorked");
    }

    //invisible delete prefs btn
    public void DeletePresets()
    {
        PlayerPrefs.DeleteAll();
    }

    //invisible exp btn
    public void GetExp()
    {
        print(System.DateTime.Now.Ticks);

        LevelManager.Instance.AddExp(10000);
    }

    private int GetActiveAncestor()
    {
        int index = LevelManager.Instance.GetItemIndex(LevelManager.Instance.GetActivePassive());

        return index;

    }
    private void IndexChecker()
    {
        int index = GetActiveAncestor();
        ancestorPortrait[index].SetActive(true);
        ancestorText[index].SetActive(true);
    }
    public void SoundBtnRoute()
    {
        int index = 0;
        if (LevelManager.Instance.GetSoundCheck() == index)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            AudioListener.pause = true;
            LevelManager.Instance.SetSoundCkeck(index + 1);
            print(LevelManager.Instance.GetSoundCheck());
        }
        else 
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            AudioListener.pause = false;
            LevelManager.Instance.SetSoundCkeck(index -1);
            print(LevelManager.Instance.GetSoundCheck());
        }

    }
}
