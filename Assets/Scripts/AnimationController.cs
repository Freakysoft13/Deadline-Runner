using UnityEngine;

public class AnimationController : MonoBehaviour {

    private SkeletonAnimation skelAnimation;

    void Start()
    {
        skelAnimation = GetComponent<SkeletonAnimation>();
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
}
