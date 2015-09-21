using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    public Image HideMask;
    public GameObject[] ancestors;

    void Start()
    {
        DeactivateAll();
    }

    private void DeactivateAll()
    {
        HideMask.gameObject.SetActive(false);
        for (int i = 0; i < ancestors.Length; i++)
        {
            ancestors[i].SetActive(false);
        }
    }

    public void AncestorActivation(int index)
    {
        HideMask.gameObject.SetActive(true);
        ancestors[index].SetActive(true);
    }


    public void Close_mask_btn()
    {
        DeactivateAll();
    }

    public void Back_Btn()
    {
        Application.LoadLevel("StartMenu");
    }
    //test button, loading UIScene
    public void LoadTestUI()
    {
        Application.LoadLevel("Ui_sc");
    }

    public void ActivateAncestorWillow()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.WILLOW);
        DeactivateAll();
    }
    public void ActivateAncestorDelmor()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.DELMOR);
        DeactivateAll();
    }
    public void ActivateAncestorArden()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.ARDEN);
        DeactivateAll();
    }
    public void ActivateAncestorCrimson()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.CRIMSON);
        DeactivateAll();
    }
    public void ActivateAncestorMaxwell()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.MAXWELL);
        DeactivateAll();
    }
    public void ActivateAncestorAmoret()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.AMORET);
        DeactivateAll();
    }
    public void ActivateAncestorMordecai()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.MORDECAI);
        DeactivateAll();
    }
    public void ActivateAncestorMorgana()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.MORGANA);
        DeactivateAll();
    }
    public void ActivateAncestorElenor()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.ELENOR);
        DeactivateAll();
    }
    public void ActivateAncestorArchibald()
    {
        LevelManager.Instance.ActivatePassiveSkill(LevelManager.Passive.ARCHIBALD);
        DeactivateAll();
    }
}
