using UnityEngine;
using System.Collections;
using SunCubeStudio.Localization;

public class LocalManager : MonoBehaviour
{

    void Awake()
    {
        if (Application.systemLanguage.ToString() == "Russian")
        {
            LocalizationService.Instance.Localization = "Russian";
        }
        else if (Application.systemLanguage.ToString() == "Ukrainian")
        {
            LocalizationService.Instance.Localization = "Ukrainian";
        }
        else
        {
            LocalizationService.Instance.Localization = "English";
        }
    }
}
