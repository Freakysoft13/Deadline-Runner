using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    private Vector2 velocity;
    private float gravityScale = 1.0f;

    public float speed;
    public int direction = -1;
    public float defaultGravityAccel = 1.0f;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (IsGrounded()) {
            velocity.x = direction * speed;
        }
        //velocity.y -= 9.8f * Time.deltaTime * gravityScale * defaultGravityAccel;
    }
    private bool IsGrounded() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D col in colliders) {
            if (col.CompareTag("floor")) {
                return true;
            }
        }
        return false;
    }

    void FixedUpdate() {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
