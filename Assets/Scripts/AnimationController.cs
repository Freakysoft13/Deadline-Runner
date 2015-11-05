using UnityEngine;
using System;

public class AnimationController : MonoBehaviour {

    private SkeletonAnimation skelAnimation;

    public Action animationEnd;


    void Start()
    {
        skelAnimation = GetComponent<SkeletonAnimation>();
        skelAnimation.state.Complete += AnimationComplete;
    }

    private void AnimationComplete(Spine.AnimationState state, int trackIndex, int loopCount) {
        if (animationEnd != null) {
            animationEnd.DynamicInvoke();
            animationEnd = null;
        }
    }

    public void JumpUp()
    {
        skelAnimation.loop = false;
        skelAnimation.timeScale = 1;
        skelAnimation.AnimationName = "Jump_Up";
    }

    public void FallDown()
    {
        skelAnimation.timeScale = 0.5f;
        skelAnimation.AnimationName = "Jump_Down";
    }

    public void Fly()
    {
        skelAnimation.loop = true;
        skelAnimation.timeScale = 1;
        skelAnimation.AnimationName = "Run";
    }

    public void MultTimeScale(float gravitiScale)
    {
        skelAnimation.timeScale *= gravitiScale;
    }

    public void Die()
    {
        skelAnimation.loop = false;
        skelAnimation.timeScale = 1;
        skelAnimation.AnimationName = "Death";
    }

    public void Ressurect(Action animationEnd) {
        this.animationEnd = animationEnd;
        skelAnimation.loop = false;
        skelAnimation.timeScale = 1;
        skelAnimation.AnimationName = "Resurrection";
    }
}
