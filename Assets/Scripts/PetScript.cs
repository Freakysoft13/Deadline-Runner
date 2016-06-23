using UnityEngine;
using System.Collections;

public class PetScript : MonoBehaviour
{

    public float distanceToPlayer = 10.0f;

    private SkeletonAnimation anim;

    private bool isRunning = false;
    private Player player;
    private bool isFlipped = false;

    // Use this for initialization
    void Start()
    {
        bool isActive = LevelManager.Instance.IsConsumableActive(LevelManager.Consumable.COLLECTOR_PET) || HasPet();
        gameObject.SetActive(isActive);
        if (!isActive)
        {
            return;
        }
        anim = GetComponent<SkeletonAnimation>();
        player = GameManager.Instance.Player;
        Run();
        EventManager.OnBeforePlayerDied += () =>
        {
            LevelManager.Instance.DeactivateConsumable(LevelManager.Consumable.COLLECTOR_PET);
            Stop();
        };
        EventManager.OnPlayerResurrected += () =>
        {
            Run();
        };
        EventManager.OnHeadstart += () =>
        {
            Stop();
        };
        EventManager.OnHeadstartEnd += () =>
        {
            Run();
        };
    }

    private bool HasPet()
    {
        int cnt = PlayerPrefs.GetInt("pet", 0);
        PlayerPrefs.SetInt("pet", ++cnt);
        return cnt < 4;
    }

    // Update is called once per frame
    void Update()
    {
        float currentDistanceToPlayer = player.transform.position.x - transform.position.x;
        if (isRunning && (currentDistanceToPlayer > distanceToPlayer))
        {
            Vector3 futurePos = transform.position;
            Vector3 scale = transform.localScale;
            futurePos.x = player.transform.position.x - distanceToPlayer;

            if (player.isFlipped() && isFlipped)
            {
                isFlipped = false;
                scale.y = 1;
                futurePos.y = 0.7f;
            }
            else if (!player.isFlipped() && !isFlipped)
            {
                scale.y = -1;
                futurePos.y = -0.7f;
                isFlipped = true;
            }
            transform.localScale = scale;
            transform.position = futurePos;
            anim.state.TimeScale = (player.car.activeSelf ? 2 : (1 + player.GetDistance() / (player.speedThreshold * 1000.0f)));
        }
    }

    private void Run()
    {
        isRunning = true;
        anim.state.SetAnimation(0, "PetRun", true);
    }

    private void Stop()
    {
        isRunning = false;
        anim.state.SetAnimation(0, "Idle", true);
    }
}
