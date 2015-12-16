using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject SFXOn;
    public GameObject SFXOff;

    void Start()
    {
        if (LevelManager.Instance.GetSoundCheck() == 0)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = false;
        }
    }
    void OnEnable()
    {
        if (LevelManager.Instance.GetSoundCheck() == 0)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }
    public void SoundBtn()
    {
        if (LevelManager.Instance.GetSoundCheck() == 0)
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            LevelManager.Instance.SetSoundCkeck(+1);
            AudioListener.pause = true;
        }
        else
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            LevelManager.Instance.SetSoundCkeck(-1);
            AudioListener.pause = false;
        }
    }
}
