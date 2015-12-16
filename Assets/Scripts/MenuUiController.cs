﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuUiController : MonoBehaviour
{

    public GameObject[] ancestorPortrait;
    public GameObject[] ancestorText;
    public Image soundOn;
    public Image soundOff;


    //timescale factor excluding freeze bug
    void Start()
    {
        SoundCheckOnStart();
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
        print(LevelManager.Instance.GetSoundCheck());
        print(soundOn.gameObject.activeInHierarchy);
        print(soundOff.gameObject.activeInHierarchy);
        print(AudioListener.pause);

        if (LevelManager.Instance.GetSoundCheck() == 0)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            AudioListener.pause = true;
            LevelManager.Instance.SetSoundCkeck(+1);
        }
        else if (LevelManager.Instance.GetSoundCheck() == 1)
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            AudioListener.pause = false;
            LevelManager.Instance.SetSoundCkeck(-1);
        }

    }

    void SoundCheckOnStart()
    {
        if (LevelManager.Instance.GetSoundCheck() == 1)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            AudioListener.pause = true;
        }
        else
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            AudioListener.pause = false;
        }

    }

}
