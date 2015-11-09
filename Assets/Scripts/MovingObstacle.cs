using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    private Transform player;
    private Vector2 velocity;
    private float gravityScale = 1.0f;
    private bool isAnimationTriggered = false;

    public float speed;
    public int direction = -1;
    public float defaultGravityAccel = 1.0f;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        if (IsPlayerClose()) {
            if(!isAnimationTriggered) {
                anim.SetTrigger("fall");
                isAnimationTriggered = true;
            }
            velocity.x = direction * speed;
        } else {
            velocity.x = 0;
        }
        //velocity.y -= 9.8f * Time.deltaTime * gravityScale * defaultGravityAccel;
    }

    private bool IsPlayerClose() {
        return Mathf.Abs(player.position.x - transform.position.x) < 25;
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void OnDisable() {
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        isAnimationTriggered = false;
        velocity.x = 0;
        anim.SetTrigger("idle");
    }
}
