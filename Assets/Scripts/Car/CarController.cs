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

    private AudioSource hit;
    private AudioSource prepare;
    private AudioSource flying;
    private AudioSource destroy;

    void OnEnable()
    {
        travelDistance = LevelManager.Instance.strangeMachineTimers[LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.HP_BOOST)];
        //print(travelDistance);
        if (skelAnimation != null)
        {
            GetInTheCar();
            skelAnimation.state.Complete += AnimationComplete;
            skelAnimation.state.Event += OnEvent;
        }
    }

    private void OnEvent(Spine.AnimationState state, int trackIndex, Spine.Event e)
    {
        if (e.Data.Name == "Hit")
        {
            hit.Play();
        }
        if (e.Data.Name == "Starting")
        {
            prepare.Play();
        }
    }

    void Start()
    {
        skelAnimation = GetComponent<SkeletonAnimation>();
        var audios = GetComponents<AudioSource>();
        hit = audios[0];
        prepare = audios[1];
        flying = audios[2];
        destroy = audios[3];
        skelAnimation.state.Complete += AnimationComplete;
        skelAnimation.state.Event += OnEvent;
        GetInTheCar();
    }

    void OnDisable()
    {
        skelAnimation.state.Complete -= AnimationComplete;
        skelAnimation.state.Event -= OnEvent;
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
        EventManager.FireHeadstart();
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
                GameManager.Instance.Player.ToggleMeshRenderer(false);
                flying.Play();
                EventManager.FireHeadstartEnd();
                skelAnimation.state.SetAnimation(0, START, true);
                isMoving = true;
                EarthShaker.instance.StartShaking(0.03f, 0.03f);
                break;
            case DESTROY:
                gameObject.SetActive(false);
                EarthShaker.instance.StopShaking();
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
            GameManager.Instance.Player.ToggleCar(false);
            flying.Stop();
            destroy.Play();
            skelAnimation.state.SetAnimation(0, DESTROY, false);
            isMoving = false;
        }
    }

    public float speed = 10f;
}
