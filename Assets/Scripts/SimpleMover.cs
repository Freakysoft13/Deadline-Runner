using UnityEngine;

public class SimpleMover : MonoBehaviour
{

    public float speed;

    void Update()
    {
		transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
