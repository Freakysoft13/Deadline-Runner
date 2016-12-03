using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5.0f;
    private float doubleJumpGravityAccel = 20.0f;
    public float maxJumpHeight = 4;
    public bool headStart = false;
    public int headStartDistance = 200;
    public float gravityMultiplier = 3;
    public Effect.Shield startShieldStub;
    public Effect.Shield machineShieldStub;

    [Header("Speed increse muliplier")]
    public float speedThreshold = 2;

    public float speedTime = 0.5f;

    private Vector2 velocity;
    private AnimationController animationController;
    private Rigidbody2D rigidBody;
    private int playerFlip = 1;
    private bool isJumping = false;
    private bool jumpOnLand = false;
    private bool jumpInterrupt = false;
    private bool flip = false;
    private bool canFlip = true;
    private float gravityScale = 1.0f;
    private bool isDead = false;
    private bool isShielded = false;
    private bool canMove = true;
    private int exp;
    private GameManager.Side side;

    private bool isInAfterLife = false;

    private bool jumpTouch = false;
    private bool flipTouch = false;
    private Collider2D floorCollider;

    public RectTransform moonPanel;

    private AudioSource jumpSound;
    private AudioSource runSound;
    private AudioSource deathSound;

    public bool IsShielded
    {
        get { return isShielded; }
        set { isShielded = value; }
    }

    //tree passives
    private bool isShieldPassive = false;
    private bool isCrystalBoostPassive = false;
    private bool isGreenCrystalDoubledPassive = false;
    private bool isIncreasedBuffSpawnPassive = false;
    private bool isScoreDoubledPassive = false;
    private bool isCrystalsDoubledPassive = false;
    private bool isObstacleInvurnerablePassive = false;
    private bool isAfterlifeBoostPassive = false;
    private bool isNoAdsResPassive = false;
    private bool isAllPassives = false;

    [SerializeField]
    private GameObject snowHolder;

    public bool CanFlip
    {
        get
        {
            return canFlip;
        }

        set
        {
            canFlip = value;
        }
    }

    public int Exp
    {
        get
        {
            return exp;
        }

        set
        {
            exp = value;
        }
    }

    public int PlayerFlip
    {
        get
        {
            return playerFlip;
        }

        set
        {
            playerFlip = value;
        }
    }

    public GameManager.Side Side
    {
        get
        {
            return side;
        }

        set
        {
            side = value;
        }
    }

    public bool IsShieldPassive
    {
        get
        {
            return isShieldPassive;
        }

        set
        {
            isShieldPassive = value;
        }
    }

    public bool IsCrystalBoostPassive
    {
        get
        {
            return isCrystalBoostPassive;
        }

        set
        {
            isCrystalBoostPassive = value;
        }
    }

    public bool IsGreenCrystalDoubledPassive
    {
        get
        {
            return isGreenCrystalDoubledPassive;
        }

        set
        {
            isGreenCrystalDoubledPassive = value;
        }
    }

    public bool IsIncreasedBuffSpawnPassive
    {
        get
        {
            return isIncreasedBuffSpawnPassive;
        }

        set
        {
            isIncreasedBuffSpawnPassive = value;
        }
    }

    public bool IsScoreDoubledPassive
    {
        get
        {
            return isScoreDoubledPassive;
        }

        set
        {
            isScoreDoubledPassive = value;
        }
    }

    public bool IsCrystalsDoubledPassive
    {
        get
        {
            return isCrystalsDoubledPassive;
        }

        set
        {
            isCrystalsDoubledPassive = value;
        }
    }

    public bool IsObstacleInvurnerablePassive
    {
        get
        {
            return isObstacleInvurnerablePassive;
        }

        set
        {
            isObstacleInvurnerablePassive = value;
        }
    }

    public bool IsAfterlifeBoostPassive
    {
        get
        {
            return isAfterlifeBoostPassive;
        }

        set
        {
            isAfterlifeBoostPassive = value;
        }
    }

    public bool IsNoAdsResPassive
    {
        get
        {
            return isNoAdsResPassive;
        }

        set
        {
            isNoAdsResPassive = value;
        }
    }

    public bool IsAllPassives
    {
        get
        {
            return isAllPassives;
        }

        set
        {
            isAllPassives = value;
        }
    }

    public bool IsDead
    {
        get { return isDead; }
    }

    public bool IsInAfterLife
    {
        get { return isInAfterLife; }
    }

    void Start()
    {
        SetActiveSkin();
        animationController = GetComponent<AnimationController>();
        rigidBody = GetComponent<Rigidbody2D>();
        moonPanel.localPosition = new Vector3(0, -350);
        floorCollider = GameObject.FindGameObjectWithTag("floor").GetComponent<Collider2D>();
        if (isScoreDoubledPassive)
        {
            GameManager.Instance.expMultiplier = 2;
        }
        if (IsCrystalsDoubledPassive)
        {
            GameManager.Instance.scoreMultiplier = 2;
        }
        AudioSource[] sources = GetComponents<AudioSource>();
        jumpSound = sources[0];
        runSound = sources[1];
        if (!LevelManager.Instance.GetEquippedSkin().Equals(LevelManager.Skin.UR900))
        {
            Invoke("RunSound", speedTime);
        }
    }

    private void SetActiveSkin()
    {
        SetSkin(LevelManager.Instance.GetEquippedSkin());
    }

    private void SetSkin(LevelManager.Skin skin)
    {
        LevelManager lm = LevelManager.Instance;
        SkeletonDataAsset asset = lm.skins[lm.GetItemIndex(skin)];
        SkeletonAnimation sa = GetComponent<SkeletonAnimation>();
        if (skin == LevelManager.Skin.GHOST && sa.AnimationName == AnimationController.ELECTRICITY_RUN)
        {
            sa.AnimationName = AnimationController.RUN;
        }
        sa.skeletonDataAsset = asset;
        sa.Reset();
    }

    public void AfterLifeStart()
    {
        isInAfterLife = true;
        if (isAfterlifeBoostPassive)
        {
            speed *= 2;
        }
        SetSkin(LevelManager.Skin.GHOST);
        EventManager.FireAfterlifeToggled(true);
    }

    public void AfterLifeEnd()
    {
        SetActiveSkin();
        isInAfterLife = false;
        if (isAfterlifeBoostPassive)
        {
            speed /= 2;
        }
        EventManager.FireAfterlifeToggled(false);
        Die();
    }

    private float updatedAtDistance = 0;
    public GameObject car;
    void Update()
    {
        if (isInCar)
        {
            velocity = Vector3.zero;
            transform.position = car.transform.position;
            Vector3 newPos = transform.position;
            newPos.x -= 0.2f;
            newPos.y -= playerFlip * -0.5f;
            transform.position = newPos;
            return;
        }
        ApplyGravity();
        if (isDead || !canMove) { Stop(); return; }
        CheckTouch();
        Go();
        if (IsFlipPressed() && IsGrounded())
        {
            Flip();
        }
        if ((IsJumpPressed() || jumpOnLand) && IsGrounded() && !isInAfterLife)
        {
            Jump();
        }
        if (IsJumpPressed() && !IsGrounded())
        {
            jumpInterrupt = true;
            jumpOnLand = true;
            gravityScale = doubleJumpGravityAccel;
        }
        if (PlayerFlip * transform.position.y > maxJumpHeight || jumpInterrupt || isFalling())
        {
            FallDown();
        }
        if (GetDistance() % 200 == 0 && isCrystalBoostPassive && updatedAtDistance != GetDistance())
        {
            GameManager.Instance.AddCrystals(20);
            updatedAtDistance = GetDistance();
        }
        /* if(Input.GetKeyDown(KeyCode.LeftControl)) {
             EventManager.Instance.FireHeadstart();
             canMove = false;
             animationController.Idle();
         }
         */
        if (headStart)
        {
            ToggleCar(true);
        }
    }

    private bool isFalling()
    {
        return !IsGrounded() && velocity.y * PlayerFlip < 0;
    }

    private bool IsJumpPressed()
    {
        bool result = Input.GetKeyDown(KeyCode.Space) || jumpTouch;
        return result;
    }

    private bool IsFlipPressed()
    {
        bool result = Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl) || flipTouch;
        return result;
    }

    public float maxSpeed = 20;

    public void Go()
    {
        float speedMultiplier = (1 + GetDistance() / (speedThreshold * 1000.0f));
        velocity.x = Mathf.Clamp(speed * (speedMultiplier), velocity.x, maxSpeed);
        if (IsGrounded() && !isJumping)
        {
            gravityScale = 1;
            if (canFlip || isInAfterLife)
            {
                animationController.Run(speedMultiplier);
            }
            else
            {
                animationController.RunWithElectricity(speedMultiplier);
            }
        }

        if (isShieldPassive)
        {
            startShieldStub.PickUp();
            isShieldPassive = false;
        }
    }
    private bool isInCar = false;

    public void ActivateCar()
    {
        car.SetActive(true);
    }

    public void ToggleCar(bool flag)
    {
        isInCar = flag;
        if (flag)
        {
            animationController.Ride();
            ActivateCar();
        }
        else
        {
            machineShieldStub.PickUp();
            if (headStart)
            {
                headStart = false;
            }
            ToggleMeshRenderer(true);
        }
    }

    public void ToggleMeshRenderer(bool active)
    {
        GetComponent<MeshRenderer>().enabled = active;
    }
    private void RunSound()
    {
        float speedMultiplier = (1 + GetDistance() / (speedThreshold * 1000.0f));
        if (IsGrounded() && !isInAfterLife)
        {
            runSound.pitch = Random.Range(0.8f, 0.9f);
            runSound.Play();
        }
        if (!isDead)
        {
            Invoke("RunSound", speedTime / speedMultiplier);
        }
    }
    public void Stop()
    {
        velocity.x = 0;
        if (PlayerFlip * velocity.y > 0)
        {
            velocity.y = 0;
        }
        runSound.Stop();
    }

    public void Jump()
    {
        isJumping = true;
        jumpOnLand = false;
        velocity.y = jumpSpeed * PlayerFlip;
        animationController.JumpUp();
        jumpSound.Play();
    }
    public void FallDown()
    {
        isJumping = false;
        jumpInterrupt = false;
        if (PlayerFlip * velocity.y > 0)
        {
            velocity.y = 0;
        }
        animationController.FallDown();
    }

    public void ApplyGravity()
    {
        velocity.y -= 9.8f * Time.deltaTime * PlayerFlip * gravityScale * gravityMultiplier;
    }

    void FixedUpdate()
    {
        rigidBody.velocity = velocity;
    }

    public float GetSpeed()
    {
        return rigidBody.velocity.x;
    }

    public GameObject shieldEffect;

    public void ShieldsUp()
    {
        shieldEffect.GetComponent<SkeletonAnimation>().AnimationName = "anim";
        shieldEffect.GetComponent<SkeletonAnimation>().Reset();
        isShielded = true;
        shieldEffect.SetActive(true);
    }

    public void ShieldsDown()
    {
        isShielded = false;
        shieldEffect.SetActive(false);
    }

    public void ShieldWearOff()
    {
        shieldEffect.GetComponent<SkeletonAnimation>().AnimationName = "ending";
        shieldEffect.GetComponent<SkeletonAnimation>().Reset();
    }

    public void Die()
    {
        exp = (GetDistance() * GameManager.Instance.expMultiplier) / expToDistanceThreshold;
        EventManager.FireBeforePlayerDied();
        isDead = true;
        jumpOnLand = false;
        runSound.Stop();
        EventManager.OnAnimationComplete += delegate (string name)
        {
            if (name != AnimationController.DEATH) { return; }
            canMove = false;
            EventManager.FirePlayerDied();
        };
        animationController.Die();
    }

    public void Ressurect()
    {
        EventManager.FireBeforePlayerResurrected();
        EventManager.OnAnimationComplete += delegate (string name)
        {
            if (name != AnimationController.RESURRECTION) { return; }
            isDead = false;
            canMove = true;
            Vector3 pos = transform.position;
            pos.y = PlayerFlip;
            transform.position = pos;
            EventManager.FirePlayerResurrected();
        };
        animationController.Ressurect();
    }

    private void CheckTouch()
    {
        jumpTouch = false;
        flipTouch = false;
        if (Input.touchCount == 1 && Time.timeScale == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount > 0)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    float ceiling = Screen.height - Screen.height / 8;
                    if (touch.position.x < Screen.width / 2 && touch.position.y < ceiling)
                    {
                        flipTouch = true;
                    }
                    else
                    {
                        jumpTouch = true;
                    }
                }
            }
        }
    }

    private bool IsGrounded()
    {
        float botY = isFlipped() ? GetComponent<Collider2D>().bounds.max.y : GetComponent<Collider2D>().bounds.min.y;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, botY), 0.1f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("floor"))
            {
                return true;
            }
        }
        return false;
    }

    public bool isFlipped()
    {
        return PlayerFlip == -1;
    }

    public void Flip()
    {
        if (!canFlip) { return; }
        PlayerFlip *= -1;
        Vector2 newPosition = transform.position;
        Vector2 newScale = transform.localScale;
        newPosition.y = isFlipped() ? -2 : 2;
        newScale.y = PlayerFlip;
        transform.position = newPosition;
        transform.localScale = newScale;
        velocity.y = 0;

        snowHolder.transform.position = new Vector2(snowHolder.transform.position.x,
                                            snowHolder.transform.position.y + (!isFlipped() ? -2 : 2));

        if (!isFlipped())
        {
            moonPanel.localPosition = new Vector3(0, -350);
            side = GameManager.Side.UPPER;
        }
        else
        {
            moonPanel.localPosition = new Vector3(0, -150);
            side = GameManager.Side.BOTTOM;
        }
    }

    public int expToDistanceThreshold = 2;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.CompareTag("obstacle") || collider.CompareTag("flying")) && !isDead && !isShielded && !isInAfterLife && !isInCar)
        {
            GameManager.Instance.ApplyAfterLife();
        }
    }

    public int GetDistance()
    {
        return (int)transform.position.x + 8;
    }

    public void ElectricityHit()
    {
        canFlip = false;
    }

    public void ElectricityWearOff()
    {
        canFlip = true;
    }
}
