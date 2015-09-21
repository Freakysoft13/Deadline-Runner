using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public float value = 1;

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(value);
            gameObject.SetActive(false);
        }
    }
}
