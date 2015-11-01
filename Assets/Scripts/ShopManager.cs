using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopManager : MonoBehaviour
{

    public Text money;
    public ScrollRect pwUpPnl;
    public ScrollRect ConsumPnl;
    public ScrollRect SpecialPnl;
    public GameObject rightgm;
    public GameObject centregm;
    public GameObject leftgm;
    public int charCells = 2;
    LevelManager.Skin skin;

    Vector3 startRightPosition;
    Vector3 startCentrePosition;
    Vector3 startLeftPosition;
    Vector3 bottomPos = new Vector3(0, -25);

    public GameObject[] charlist;
    public GameObject[] lockmasklist;
    public int[] charprice;
    private int index = 0;
    public GameObject[] activateBtn;
    public GameObject[] activatedBtn;
    public GameObject[] buyBtn;
    public GameObject blockingMask;

    void Start() {
        money.text = LevelManager.Instance.GetMoney().ToString();
        startRightPosition = rightgm.transform.position;
        startCentrePosition = centregm.transform.position;
        startLeftPosition = leftgm.transform.position;
        index = GetSkinIndex();
        UpdateCharPos();
        SkinChecker();
    }
    void Update() {
        money.text = LevelManager.Instance.GetMoney().ToString();
    }

    public void PositionReset() {
        pwUpPnl.normalizedPosition = Vector2.zero;
        ConsumPnl.normalizedPosition = Vector2.zero;
        SpecialPnl.normalizedPosition = Vector2.zero;
    }

    private void UpdateCharPos() {
        foreach (GameObject go in charlist) {
            go.transform.position = bottomPos;
        }
        charlist[index].transform.position = startLeftPosition;
        charlist[index + 1].transform.position = startCentrePosition;
        charlist[index + 2].transform.position = startRightPosition;
    }

    public void next_char() {
        if (!((index + charCells + 1) >= charlist.Length)) {
            index++;
        }
        UpdateCharPos();
    }

    public void prevChar() {
        if (!(index == 0)) {
            index--;
        }
        UpdateCharPos();
    }

    private int GetSkinIndex() {
        int index = LevelManager.Instance.GetItemIndex(LevelManager.Instance.GetEquippedSkin());
        if (index > charlist.Length - charCells) {
            index -= charCells + ((charlist.Length - 1) - index);
        }
        return index;
    }
    public void BuyChar(int index) {
        //Add check for owned skins.
        if (LevelManager.Instance.GetMoney() >= charprice[index]) {
            LevelManager.Instance.AddMoney(-charprice[index]);
            LevelManager.Skin skin = (LevelManager.Skin)System.Enum.GetValues(typeof(LevelManager.Skin)).GetValue(index);

            LevelManager.Instance.BuySkin(skin);
            print(skin);
            SkinChecker();
        }
        else {
            lockmasklist[index].SetActive(false);
            blockingMask.SetActive(true);
        }
    }
    public void LockedChar(int index) {
        lockmasklist[index].SetActive(true);
    }

    public void DeactivateCharMask(int index) {
        lockmasklist[index].SetActive(false);
    }


    public void ActivateSkin(int index) {
        LevelManager.Instance.EquipSkin(index);
        SkinChecker();
    }

    //call on start and on char button press, check what skins you own and replace buy button with activate button
    public void SkinChecker() {
        LevelManager lm = LevelManager.Instance;
        for(int i = 0; i < buyBtn.Length; i++)
        {
            buyBtn[i].SetActive(false);
            if (!lm.HasSkin(i)) {
               buyBtn[i].SetActive(true);
            }
        }
        for (int i = 0; i < activatedBtn.Length; i++)
        {
            activatedBtn[i].SetActive(false);
            if (i == lm.GetItemIndex(lm.GetEquippedSkin())) {
                activatedBtn[i].SetActive(true);
            }
        }
        for (int i = 0; i < activateBtn.Length; i++)
        {
            activateBtn[i].SetActive(false);
            if (!(i == lm.GetItemIndex(lm.GetEquippedSkin())) && lm.HasSkin(i)) {
                activateBtn[i].SetActive(true);
            }
        }
    }

}
