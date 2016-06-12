using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour
{

    private SkeletonAnimation skelAnimation;
    private float xSpawn;

    public const string PREPARE = "Preparation";
    public const string START = "Starting";
    public const string DESTROY = "Destroying";

    public float travelDistance;

    void OnEnable()
    {
        travelDistance = LevelManager.Instance.strangeMachineTimers[LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST)];
        //print(travelDistance);
        if (skelAnimation != null)
        {
            GetInTheCar();
            skelAnimation.state.Complete += AnimationComplete;
        }
    }

    void Start()
    {
        skelAnimation = GetComponent<SkeletonAnimation>();
        skelAnimation.state.Complete += AnimationComplete;
        GetInTheCar();
    }

    void OnDisable()
    {
        skelAnimation.state.Complete -= AnimationComplete;
        GameManager.Instance.Player.ToggleCar(false);
    }
    public float headStartDistance = 20.0f;
    private void GetInTheCar()
    {
        MoveToPlayer();
        skelAnimation.state.SetAnimation(0, PREPARE, false);
        xSpawn = GameManager.Instance.Player.transform.position.x;
        GameManager.Instance.Player.ToggleCar(true);
        if (GameManager.Instance.Player.headStart)
        {
            travelDistance = headStartDistance;
        }
        target = new Vector3(xSpawn + travelDistance, GameManager.Instance.Player.PlayerFlip * 2, 0);
    }

    private Vector3 target;

    private void MoveToPlayer()
    {
        Player player = GameManager.Instance.Player;
        float yOffset = 0;
        Vector2 newPos = player.transform.position;
        newPos.y += player.PlayerFlip * yOffset;
        transform.position = newPos;
        transform.localScale = player.transform.localScale;
    }

    private bool isMoving = false;

    private void AnimationComplete(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        switch (skelAnimation.AnimationName)
        {
            case PREPARE:
                skelAnimation.state.SetAnimation(0, START, true); isMoving = true; break;
            case DESTROY:
                gameObject.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - target.x) < Mathf.Epsilon && isMoving)
        {
            skelAnimation.state.SetAnimation(0, DESTROY, false);
            isMoving = false;
        }
    }

    public float speed = 10f;
}
