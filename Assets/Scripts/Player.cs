using UnityEngine;
using Effect;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5.0f;
    private float doubleJumpGravityAccel = 20.0f;
    public float maxJumpHeight = 4;
    public bool headStart = false;
    public int headStartDistance = 200;
    public float gravityMultiplier = 3;

    [Header("Speed increse muliplier")]
    public float speedThreshold = 50;

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

    private bool isInAfterLife = false;

    private bool jumpTouch = false;
    private bool flipTouch = false;
    private Collider2D floorCollider;

    public RectTransform moonPanel;

    public bool CanFlip {
        get {
            return canFlip;
        }

        set {
            canFlip = value;
        }
    }

    public int Exp {
        get {
            return exp;
        }

        set {
            exp = value;
        }
    }

    public int PlayerFlip {
        get {
            return playerFlip;
        }

        set {
            playerFlip = value;
        }
    }

    void Start() {
        SetActiveSkin();
        animationController = GetComponent<AnimationController>();
        rigidBody = GetComponent<Rigidbody2D>();
        //new ConsumablesManager().ApplyActiveConsumables();
        moonPanel.localPosition = new Vector3(0, -350);
        floorCollider = GameObject.FindGameObjectWithTag("floor").GetComponent<Collider2D>();
    }

    private void SetActiveSkin() {
        SetSkin(LevelManager.Instance.GetEquippedSkin());
    }

    private void SetSkin(LevelManager.Skin skin) {
        LevelManager lm = LevelManager.Instance;
        SkeletonDataAsset asset = lm.skins[lm.GetItemIndex(skin)];
        SkeletonAnimation sa = GetComponent<SkeletonAnimation>();
        sa.skeletonDataAsset = asset;
        sa.Reset();
    }

    public void AfterLifeStart() {
        isInAfterLife = true;
        SetSkin(LevelManager.Skin.GHOST);
    }

    public void AfterLifeEnd() {
        SetActiveSkin();
        isInAfterLife = false;
        Die();
    }

    void Update() {
        ApplyGravity();
        if (isDead || !canMove) { Stop(); return; }
        CheckTouch();
        Go();
        if (IsFlipPressed() && IsGrounded()) {
            Flip();
        }
        if ((IsJumpPressed() || jumpOnLand) && IsGrounded()) {
            Jump();
        }
        if (IsJumpPressed() && !IsGrounded()) {
            jumpInterrupt = true;
            jumpOnLand = true;
            gravityScale = doubleJumpGravityAccel;
        }
        if (PlayerFlip * transform.position.y > maxJumpHeight || jumpInterrupt || isFalling()) {
            FallDown();
        }
        /* if(Input.GetKeyDown(KeyCode.LeftControl)) {
             EventManager.Instance.FireHeadstart();
             canMove = false;
             animationController.Idle();
         }
         */
    }

    private bool isFalling() {
        return !IsGrounded() && velocity.y * PlayerFlip < 0;
    }

    private bool IsJumpPressed() {
        bool result = Input.GetKeyDown(KeyCode.Space) || jumpTouch;
        return result;
    }

    private bool IsFlipPressed() {
        bool result = Input.GetKeyDown(KeyCode.AltGr) || flipTouch;
        return result;
    }

    public void Go() {
        float speedMultiplier = (1 + GetDistance() / 1000.0f);
        velocity.x = speed * (speedMultiplier);
        if (IsGrounded() && !isJumping) {
            gravityScale = 1;
            animationController.Run(speedMultiplier);
        }
    }
    public void Stop() {
        velocity.x = 0;
        if (PlayerFlip * velocity.y > 0) {
            velocity.y = 0;
        }
    }

    public void Jump() {
        isJumping = true;
        jumpOnLand = false;
        velocity.y = jumpSpeed * PlayerFlip;
        animationController.JumpUp();
    }
    public void FallDown() {
        isJumping = false;
        jumpInterrupt = false;
        if (PlayerFlip * velocity.y > 0) {
            velocity.y = 0;
        }
        animationController.FallDown();
    }

    public void ApplyGravity() {
        velocity.y -= 9.8f * Time.deltaTime * PlayerFlip * gravityScale * gravityMultiplier;
    }

    void FixedUpdate() {
        rigidBody.velocity = velocity;
    }

    public float GetSpeed() {
        return rigidBody.velocity.x;
    }

    public GameObject shieldEffect;

    public void ShieldsUp() {
        isShielded = true;
        shieldEffect.SetActive(true);
    }

    public void ShieldsDown() {
        isShielded = false;
        shieldEffect.SetActive(false);
    }

    public void Die() {
        exp = GetDistance() / expToDistanceThreshold;
        EventManager.Instance.FireBeforePlayerDied();
        isDead = true;
        jumpOnLand = false;
        EventManager.Instance.OnAnimationComplete += delegate (string name) {
            if (name != AnimationController.DEATH) { return; }
            canMove = false;
            EventManager.Instance.FirePlayerDied();
        };
        animationController.Die();
    }

    public void Ressurect() {
        EventManager.Instance.FireBeforePlayerResurrected();
        EventManager.Instance.OnAnimationComplete += delegate (string name) {
            if (name != AnimationController.RESURRECTION) { return; }
            isDead = false;
            canMove = true;
            Vector3 pos = transform.position;
            pos.y = PlayerFlip;
            transform.position = pos;
            EventManager.Instance.FirePlayerResurrected();
        };
        animationController.Ressurect();
    }

    private void CheckTouch() {
        jumpTouch = false;
        flipTouch = false;
        if (Input.touchCount == 1 && Time.timeScale == 1) {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount > 0) {
                if (touch.phase == TouchPhase.Began) {
                    if (touch.position.x < Screen.width / 2) {
                        flipTouch = true;
                    }
                    else {
                        jumpTouch = true;
                    }
                }
            }
            if (isDead && touch.tapCount == 2) {
                Application.LoadLevel(0);
            }
        }
    }

    private bool IsGrounded() {
        return floorCollider.IsTouching(GetComponent<Collider2D>());
    }

    private bool isFlipped() {
        return PlayerFlip == -1;
    }

    public void Flip() {
        if (!canFlip) { return; }
        PlayerFlip *= -1;
        Vector2 newPosition = transform.position;
        Vector2 newScale = transform.localScale;
        newPosition.y = isFlipped() ? -2 : 2;
        newScale.y = PlayerFlip;
        transform.position = newPosition;
        transform.localScale = newScale;
        velocity.y = 0;

        if (!isFlipped()) {
            moonPanel.localPosition = new Vector3(0, -350);
        }
        else {
            moonPanel.localPosition = new Vector3(0, -150);
        }
    }

    public int expToDistanceThreshold = 3;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("obstacle") && !isDead && !isShielded) {
            GameManager.Instance.ApplyAfterLife();
        }
    }

    public int GetDistance() {
        return (int)transform.position.x + 8;
    }
}
