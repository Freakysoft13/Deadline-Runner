using UnityEngine;
using System;

public class AnimationController : MonoBehaviour
{

    private SkeletonAnimation skelAnimation;

    public Action animationEnd;

    public const string RUN = "Run";
    public const string DEATH = "Death";
    public const string RESURRECTION = "Resurrection";
    public const string JUMP_UP = "Jump_Up";
    public const string JUMP_DOWN = "Jump_Down";
    public const string IDLE = "Idle";

    void Start() {
        skelAnimation = GetComponent<SkeletonAnimation>();
        skelAnimation.state.Complete += AnimationComplete;
    }

    private void AnimationComplete(Spine.AnimationState state, int trackIndex, int loopCount) {
        EventManager.Instance.FireAnimationComplete(skelAnimation.AnimationName);
    }

    public void JumpUp() {
        skelAnimation.loop = false;
        skelAnimation.timeScale = 0.5f;
        skelAnimation.AnimationName = JUMP_UP;
        Reset();
    }

    public void FallDown() {
        skelAnimation.loop = false;
        skelAnimation.timeScale = 0.5f;
        skelAnimation.AnimationName = JUMP_DOWN;
        Reset();
    }

    public void Run(float speedScale) {
        if (skelAnimation.AnimationName != RUN) {
            skelAnimation.loop = true;
            skelAnimation.timeScale = speedScale;
            skelAnimation.AnimationName = RUN;
            Reset();
        }
    }

    public void MultTimeScale(float gravitiScale) {
        skelAnimation.timeScale *= gravitiScale;
    }

    public void Die() {
        skelAnimation.loop = false;
        skelAnimation.timeScale = 1;
        skelAnimation.AnimationName = DEATH;
        Reset();
    }

    public void Ressurect() {
        skelAnimation.loop = false;
        skelAnimation.timeScale = 0.7f;
        skelAnimation.AnimationName = RESURRECTION;
        Reset();
    }

    public void Idle() {
        skelAnimation.loop = true;
        skelAnimation.timeScale = 1f;
        skelAnimation.AnimationName = IDLE;
        Reset();
    }

    private void Reset() {
        skelAnimation.Reset();
        skelAnimation.state.Complete += AnimationComplete;
    }
}
