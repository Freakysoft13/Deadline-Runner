using UnityEngine;
using System;

public class AnimationController : MonoBehaviour {

    private SkeletonAnimation skelAnimation;

    public Action animationEnd;

    private const string RUN = "Run";

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
        Reset();
    }

    public void FallDown()
    {
        skelAnimation.timeScale = 0.5f;
        skelAnimation.AnimationName = "Jump_Down";
        Reset();
    }

    public void Run()
    {
        if (skelAnimation.AnimationName != RUN) {
            skelAnimation.loop = true;
            skelAnimation.timeScale = 1;
            skelAnimation.AnimationName = RUN;
            Reset();
        }
    }

    public void MultTimeScale(float gravitiScale)
    {
        skelAnimation.timeScale *= gravitiScale;
    }

    public void Die(Action animationEnd)
    {
        this.animationEnd = animationEnd;
        skelAnimation.loop = false;
        skelAnimation.timeScale = 1;
        skelAnimation.AnimationName = "Death";
        Reset();
    }

    public void Ressurect(Action animationEnd) {
        this.animationEnd = animationEnd;
        skelAnimation.loop = false;
        skelAnimation.timeScale = 0.7f;
        skelAnimation.AnimationName = "Resurrection";
        Reset();
    }

    private void Reset() {
        skelAnimation.Reset();
        skelAnimation.state.Complete += AnimationComplete;
    }
}
