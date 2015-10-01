using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float jumpTime = 0.5f;
    public Transform rotator;
    public float gravityAccel = 3.0f;
    public float defaultGravityAccel = 1.0f;
    public GameObject slippyFloor;

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
    private float gravityScale = 1.0f;
    public bool isDead = false;
    public GameObject deathPanel;
    public GameObject scorePanel;

    void Start()
    {
        scorePanel.SetActive(true);
        deathPanel.SetActive(false);
        animationController = GetComponent<AnimationController>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckTouch();
        if (isDead) { velocity.y -= 9.8f * Time.deltaTime * 10.0f * playerFlip * gravityScale; return; }
        velocity.x = speed;
        if (!isRotating)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || jump) && IsGrounded())
            {
                gravityScale = 1.0f;
                velocity.y = jumpSpeed * playerFlip;
                jumpEndTime = Time.time + jumpTime;
                animationController.JumpUp();
                jump = false;
            }
            velocity.y -= 9.8f * Time.deltaTime * playerFlip * gravityScale * defaultGravityAccel;
            if ((Input.GetKeyDown(KeyCode.Space) || jump) && !IsGrounded())
            {
                gravityScale = gravityAccel;
                animationController.MultTimeScale(gravityScale);
                jump = true;
                jumpInterrupt = true;
            }
            if (Time.time > jumpEndTime && !jumpInterrupt)
            {
                jumpInterrupt = true;
            }

            if (IsGrounded() && jumpInterrupt)
            {
                gravityScale = 1.0f;
                animationController.Fly();
            }

            if (jumpInterrupt)
            {
                if ((velocity.y > 0 && !isFlipped()) || velocity.y < 0 && isFlipped())
                {
                    velocity.y = Mathf.MoveTowards(velocity.y, 0, Time.deltaTime * 100);
                    animationController.FallDown();
                }
                else
                {
                    jumpInterrupt = false;
                }
            }
            if ((Input.GetKeyDown(KeyCode.AltGr) || flip) && IsGrounded())
            {
                Flip();
                flip = false;
            }
        }
        else
        {
            if (Quaternion.Angle(rotator.rotation, targetRotatorRotation) < Mathf.Epsilon)
            {
                isRotating = false;
            }
        }
        
    }

    public void Jump()
    {
        jump = true;
    }

    private void CheckTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount > 0)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        flip = true;
                    }
                    else
                    {
                        jump = true;
                    }
                }
            }
            if (isDead && touch.tapCount == 2)
            {
                Application.LoadLevel(0);
            }
        }
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("floor"))
            {
                return true;
            }
        }
        return false;
    }

    private bool isFlipped()
    {
        return playerFlip == -1;
    }

    void FixedUpdate()
    {
        rigidBody.velocity = velocity;
        transform.localRotation = Quaternion.identity;
        rotator.rotation = Quaternion.Lerp(rotator.rotation, targetRotatorRotation, 100 * Time.deltaTime);
    }

    public void Flip()
    {
        velocity.y = 0;
        isRotating = true;
        Vector2 rotation;
        playerFlip *= -1;
        if (!isFlipped())
        {
            rotation = new Vector2(0, 0);
        }
        else
        {
            rotation = new Vector2(180, 0);
        }
        targetRotatorRotation = Quaternion.Euler(rotation);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("obstacle") && !isDead)
        {
            isDead = true;
            animationController.Die();
            slippyFloor.GetComponent<MaterialChanger>().Swap();
            deathPanel.SetActive(true);
            scorePanel.SetActive(false);
            
        }
    }
}
