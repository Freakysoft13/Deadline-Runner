﻿using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public float floatStrength = 0.5f;
    private AudioSource source;

    void Start()
    {
        source = GameObject.Find("CoinAudio").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x, transform.parent.position.y + (Mathf.Sin(Time.time + transform.GetSiblingIndex()) * floatStrength)));
    }

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().MovePosition(new Vector3(transform.position.x, 0, transform.position.z));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            source.pitch = Random.Range(0.8f, 1.0f);
            source.Play();
            GameManager.Instance.AddCrystals(value);
            gameObject.SetActive(false);
        }
    }
}
