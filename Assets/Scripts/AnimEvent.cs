using UnityEngine;
using System.Collections;

public class AnimEvent : MonoBehaviour {

    public void AnimationStop(int index)
    {
        //AnimationManager.Instance.SpinGear(index);
    }

    public void OpenAncestor()
    {
        AnimationManager.Instance.OpenAncestor();
    }
}
