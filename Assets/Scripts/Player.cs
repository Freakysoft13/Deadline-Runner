using UnityEngine;
using Effect;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float jumpTime = 0.5f;
    public Transform rotator;
    public float gravityAccel = 3.0f;
    public float defaultGravityAccel = 1.0f;
    public bool headStart = false;
    public int headStartDistance = 200;

    [Header("Speed increse muliplier")]
    public float speedThreshold = 50;

    private Vector2 velocity;
    private float jumpEndTime = 0.0f;
    private bool jumpInterrupt = false;
    private AnimationController animationController;
    private Rigidbody2D rigidBody;
    private int playerFlip = 1;
    private Quaternion targetRotatorRotation = Quaternion.identity;
    private bool isRotating = false;
    private bool jump = false;
    private bool flip = false;
    private bool canFlip = true;
    private float gravityScale = 1.0f;
    private bool isDead = false;
    private bool isShielded = false;
    private bool canMove = true;
    private int exp;


    private float speedAtDeath = 0;

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

    void Start() {
        LevelManager lm = LevelManager.Instance;
        LevelManager.Skin skin = lm.GetEquippedSkin();
        SkeletonDataAsset asset = lm.skins[lm.GetItemIndex(skin)];
        SkeletonAnimation sa = GetComponent<SkeletonAnimation>();
        sa.skeletonDataAsset = asset;
        sa.Reset();
        animationController = GetComponent<AnimationController>();
        rigidBody = GetComponent<Rigidbody2D>();
        //new ConsumablesManager().ApplyActiveConsumables();
    }

    void Update() {
        if(!canMove) { return; }
        if(headStart) {
            if (transform.position.x < headStartDistance) {
                velocity.x = speed * 10;
                velocity.y -= 9.8f * Time.deltaTime * playerFlip * gravityScale * defaultGravityAccel;
                isShielded = true;
            }
            else {
                headStart = false;
                EventManager.Instance.FireHeadstartEnd();
            }
            return;
        }
        CheckTouch();
        if (isDead) { velocity.y -= 9.8f * Time.deltaTime * playerFlip * 10 * gravityScale; return; }
        velocity.x = speed + speedThreshold * Mathf.Log(transform.position.x + 8) * Time.deltaTime;
        if (!isRotating) {
            if ((Input.GetKeyDown(KeyCode.Space) || jump) && IsGrounded()) {
                gravityScale = 1.0f;
                velocity.y = jumpSpeed * playerFlip;
                jumpEndTime = Time.time + jumpTime;
                animationController.JumpUp();
                jump = false;
            }
            velocity.y -= 9.8f * Time.deltaTime * playerFlip * gravityScale * defaultGravityAccel;
            if ((Input.GetKeyDown(KeyCode.Space) || jump) && !IsGrounded()) {
                gravityScale = gravityAccel;
                animationController.MultTimeScale(gravityScale);
                jump = true;
                jumpInterrupt = true;
            }
            if (Time.time > jumpEndTime && !jumpInterrupt) {
                jumpInterrupt = true;
            }

            if (IsGrounded() && jumpInterrupt) {
                gravityScale = 1.0f;
                animationController.Run();
            }

            if (jumpInterrupt) {
                if ((velocity.y > 0 && !isFlipped()) || velocity.y < 0 && isFlipped()) {
                    velocity.y = Mathf.MoveTowards(velocity.y, 0, Time.deltaTime * 100);
                    animationController.FallDown();
                }
                else {
                    jumpInterrupt = false;
                }
            }
            if ((Input.GetKeyDown(KeyCode.AltGr) || flip) && IsGrounded()) {
                Flip();
                flip = false;
            }
        }
        else {
            if (Quaternion.Angle(rotator.rotation, targetRotatorRotation) < Mathf.Epsilon) {
                isRotating = false;
            }
        }

    }

    public void ShieldsUp() {
        isShielded = true;
    }

    public void ShieldsDown() {
        isShielded = false;
    }

    public void Die() {
        EventManager.Instance.FireBeforePlayerDied();
        isDead = true;
        speedAtDeath = speed;
        speed = 0;
        animationController.Die(OnDead);
    }
    private void OnDead() {
        canMove = false;
        EventManager.Instance.FirePlayerDied();
    }
    public void Ressurect() {
        EventManager.Instance.FireBeforePlayerResurrected();
        animationController.Ressurect(OnResurrected);        
    }

    private void OnResurrected() {
        isDead = false;
        canMove = true;
        speed = speedAtDeath;
        Vector3 pos = transform.position;
        pos.y = 1;
        transform.position = pos;
        EventManager.Instance.FirePlayerResurrected();
    }

    public void Jump() {
        jump = true;
    }

    private void CheckTouch() {
        if (Input.touchCount == 1 && Time.timeScale == 1) {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount > 0) {
                if (touch.phase == TouchPhase.Began) {
                    if (touch.position.x < Screen.width / 2) {
                        flip = true;
                    }
                    else {
                        jump = true;
                    }
                }
            }
            if (isDead && touch.tapCount == 2) {
                Application.LoadLevel(0);
            }
        }
    }

    private bool IsGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D col in colliders) {
            if (col.CompareTag("floor")) {
                return true;
            }
        }
        return false;
    }

    private bool isFlipped() {
        return playerFlip == -1;
    }

    void FixedUpdate() {
        rigidBody.velocity = velocity;
        transform.localRotation = Quaternion.identity;
        rotator.rotation = Quaternion.Lerp(rotator.rotation, targetRotatorRotation, 100 * Time.deltaTime);
    }

    public void Flip() {
        if(!canFlip) { return; }
        velocity.y = 0;
        isRotating = true;
        Vector2 rotation;
        playerFlip *= -1;
        if (!isFlipped()) {
            rotation = new Vector2(0, 0);
        }
        else {
            rotation = new Vector2(180, 0);
        }
        targetRotatorRotation = Quaternion.Euler(rotation);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("obstacle") && !isDead && !isShielded) {
            exp = 2500;
            Die();
        }
    }

    public int GetDistance() {
        return (int)transform.position.x + 8;
    }
}
