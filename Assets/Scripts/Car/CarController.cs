using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    private SkeletonAnimation skelAnimation;

    public const string PREPARE = "Preparation";
    public const string START = "Starting";
    public const string DESTROY = "Destroying";

    void Start () {
        skelAnimation = GetComponent<SkeletonAnimation>();
        skelAnimation.state.Complete += AnimationComplete;
        EventManager.Instance.OnHeadstart += delegate () {
            MoveToPlayer();
            skelAnimation.AnimationName = PREPARE;
            Reset();
        };
    }
	
    private void MoveToPlayer() {
        Player player = GameManager.Instance.Player;
        float yOffset = 0;
        Vector2 newPos = player.transform.position;
        newPos.y += player.PlayerFlip * yOffset;
        transform.position = newPos;
    }

    private void AnimationComplete(Spine.AnimationState state, int trackIndex, int loopCount) {
        EventManager.Instance.FireAnimationComplete(skelAnimation.AnimationName);
        switch(skelAnimation.AnimationName) {
            case PREPARE:
                skelAnimation.AnimationName = START; skelAnimation.loop = true; Reset(); break;
            case START: skelAnimation.AnimationName = DESTROY; Reset(); break;
            case DESTROY: gameObject.SetActive(false); break;
        }
    }

    private void Reset() {
        skelAnimation.Reset();
        skelAnimation.state.Complete += AnimationComplete;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
