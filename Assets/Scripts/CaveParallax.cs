﻿using UnityEngine;

public class CaveParallax : MonoBehaviour
{
    private Transform player;
    private Vector3 startPos;
    private bool shouldSavePos = true;

    public float minDistanceToPlayer = 15.0f;
    public float movementDistance = 2.0f;
    public int movementSlowFactor = 2;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update() {
        if (ShouldPerform()) {
            if (shouldSavePos) {
                startPos = transform.position;
                shouldSavePos = false;
            }
            transform.Translate(Vector3.right * (player.GetComponent<Player>().GetSpeed() / movementSlowFactor) * Time.deltaTime);
        }
        else if (!shouldSavePos) {
            transform.position = startPos;
            shouldSavePos = true;
        }
    }

    private bool ShouldPerform() {
        return Mathf.Abs(transform.position.x - player.position.x) < minDistanceToPlayer;
    }
}
