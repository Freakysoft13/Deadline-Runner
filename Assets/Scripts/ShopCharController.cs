using UnityEngine;
using System.Collections;

public class ShopCharController : MonoBehaviour {
    public GameObject charainlist;

    void Start()
    {
        Flying();
    }
 
    void CharAnimaSwitch()
    {
        Jumping();
        //Flying();
    }
    void Flying ()
    {
        SkeletonAnimation charanima = charainlist.GetComponentInChildren<SkeletonAnimation>();
        charanima.AnimationName = "Idle";
        charanima.loop = true;
        charanima.Reset();
    }
    void Jumping()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        SkeletonAnimation charanima = charainlist.GetComponentInChildren<SkeletonAnimation>();
        charanima.loop = false;
        charanima.AnimationName = "Jump_Up";
        charanima.AnimationName = "Jump_Down";
        yield return new WaitForSeconds(0.4f);
        charanima.loop = true;
        charanima.AnimationName = "Idle";
        yield return null;
    }
}