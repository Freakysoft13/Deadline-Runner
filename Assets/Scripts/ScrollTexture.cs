using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0;
    public bool active = true;

    private Renderer rend;
    private float startTime = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startTime = Time.time;
    }

    void Update()
    {
        if (active)
        {
            float offset = (Time.time - startTime) * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        }
    }

    void OnEnable()
    {
        startTime = Time.time;
    }

    void OnDisable()
    {
        active = false;
        startTime = 0;
    }
}
