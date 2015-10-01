using UnityEngine;
using System.Collections;

public class CaveParallax : MonoBehaviour
{

    private bool isPlayerNear = false;
    private Transform player;
    private Vector3 startPos;

    public float minDistanceToPlayer = 15.0f;
    public float movementDistance = 2.0f;
    public int movementSlowFactor = 2;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }


    void Update() {
        if (ShouldPerform()) {
            transform.Translate(Vector3.right * (player.GetComponent<Player>().speed / movementSlowFactor) * Time.deltaTime);
        }
        else {
            transform.position = startPos;
        }
    }

    private bool ShouldPerform() {
        return Mathf.Abs(transform.position.x - player.position.x) < minDistanceToPlayer;
    }
}
