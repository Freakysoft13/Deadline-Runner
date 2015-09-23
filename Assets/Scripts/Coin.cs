using UnityEngine;

public class Coin : MonoBehaviour {

    public float value = 1;
    public float floatStrength = 0.5f;

    private float originalY;

    void Start()
    {
        originalY = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x, originalY + (Mathf.Sin(Time.time + transform.GetSiblingIndex()) * floatStrength));
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(value);
            gameObject.SetActive(false);
        }
    }
}
