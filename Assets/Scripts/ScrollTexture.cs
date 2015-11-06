using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0;
    public bool active = true;

    private Renderer rend;
    private float startTime = 0;
    float offset;

    void Start() {
        rend = GetComponent<Renderer>();
        startTime = Time.time;
        EventManager.Instance.OnPlayerDied += OnDisable;
        EventManager.Instance.OnPlayerResurrected += OnEnable;
    }

    void Update() {
        if (active) {
            offset = (Time.time - startTime) * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }

    void OnEnable() {
        active = true;
        startTime = Time.time;
    }

    void OnDisable() {
        active = false;
    }
}
