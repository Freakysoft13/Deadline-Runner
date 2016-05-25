using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public float floatStrength = 0.5f;
    private AudioSource source;

    private Transform target;

    void Start()
    {
        source = GameObject.Find("CoinAudio").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, .5f);
            if (Mathf.Abs(transform.position.x - target.position.x) < Mathf.Epsilon)
            {
                PickUp();
            }
        }
    }

    void FixedUpdate()
    {
        if (null == target)
        {
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x, transform.parent.position.y + (Mathf.Sin(Time.time + transform.GetSiblingIndex()) * floatStrength)));
        }
    }

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().MovePosition(new Vector3(transform.position.x, 0, transform.position.z));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PickUp();
        }
        if (col.CompareTag("pet"))
        {
            target = col.transform;
        }
    }

    private void PickUp()
    {
        target = null;
        source.pitch = Random.Range(0.8f, 1.0f);
        source.Play();
        GameManager.Instance.AddCrystals(value);
        gameObject.SetActive(false);
    }
}
