using UnityEngine;

public class CaveParallax : MonoBehaviour
{
    private Transform mover;
    private Vector3 startPos;
    private bool shouldSavePos = true;

    public float minDistanceToPlayer = 15.0f;
    public float movementDistance = 2.0f;
    public int movementSlowFactor = 2;

    void Start() {
        mover = GameObject.FindGameObjectWithTag("BGMover").transform;
    }


    void Update() {
        if (ShouldPerform()) {
            if (shouldSavePos) {
                startPos = transform.localPosition;
                shouldSavePos = false;
            }
            transform.Translate(Vector3.right * (mover.GetComponent<SimpleMover>().speed / movementSlowFactor) * Time.deltaTime);
        }
        else if (!shouldSavePos) {
            transform.localPosition = startPos;
            shouldSavePos = true;
        }
    }

    private bool ShouldPerform() {
        return Mathf.Abs(transform.position.x - mover.position.x) < minDistanceToPlayer;
    }
}
