﻿using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public float floatStrength = 0.5f;

    private float originalY;

    void Start() {
        originalY = transform.position.y;
    }

    void Update() {
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x, originalY + (Mathf.Sin(Time.time + transform.GetSiblingIndex()) * floatStrength)));
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            GameManager.Instance.AddScore(value);
            gameObject.SetActive(false);
        }
    }
}
