using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0;
    public bool active = true;

    private Renderer rend;
    private float time = 0;
    float offset;

    void Start() {
        rend = GetComponent<Renderer>();
        EventManager.OnBeforePlayerDied += OnDisable;
        EventManager.OnPlayerResurrected += OnEnable;
    }

    void Update() {
        if (active) {
            time += Time.deltaTime;
            offset = time * scrollSpeed;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        else {
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }

    void OnEnable() {
        active = true;
    }

    void OnDisable() {
        active = false;
    }
}
