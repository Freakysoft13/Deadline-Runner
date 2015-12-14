using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuUiController : MonoBehaviour
{

    public GameObject[] ancestorPortrait;
    public GameObject[] ancestorText;
    public Image soundOn;
    public Image soundOff;
    private int index;


    //timescale factor excluding freeze bug
    void Start()
    {
        index = LevelManager.Instance.GetSoundCheck();
        SoundBtnRoute();
        IndexChecker();
        Time.timeScale = 1;
    }
    IEnumerator Fading()
    {
        float fadeTime = GameObject.Find("LevelManager").GetComponent<FadeScene>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
    //Load Game Scene (not active yet)
    public void StartGame()
    {
        StartCoroutine(Fading());
        Application.LoadLevel("Main");
        StartCoroutine(Fading());
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
