using UnityEngine;
using System.Collections;
using Side = GameManager.Side;

public class FlyingObstacle : MonoBehaviour {

	public float speed = 10.0f;
	public float distanceToPlayer = 10.0f;

	private Transform player;
	private bool isWarningShown = false;
	private Side side;

	// Use this for initialization
	void Start () {
		SpawnableObject so = GetComponent<SpawnableObject>();
		Vector3 pos = transform.position;
		float rng = Random.Range(0, 1);
		if (rng > 0.5f) {
			pos.y = 2;
			side = Side.UPPER;
		} else {
			pos.y = -7;
			side = Side.BOTTOM;
		}
		transform.position = pos;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if (!isWarningShown) {
			EventManager.FireObstacleWarning(true, side);
			isWarningShown = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(speed * Vector2.left * Time.deltaTime);
		if (shouldDisableWarning() && isWarningShown) {
			EventManager.FireObstacleWarning(false, side);
			isWarningShown = false;
		}
	}

	private bool shouldDisableWarning() {
		return transform.position.x - player.position.x < distanceToPlayer;
	}
}
