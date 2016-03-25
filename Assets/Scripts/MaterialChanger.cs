using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour
{

    public PhysicsMaterial2D swapMaterial;
    private PhysicsMaterial2D originalMaterial;

    private bool isSwapped = false;

    void Start() {
        originalMaterial = GetComponent<BoxCollider2D>().sharedMaterial;
        EventManager.OnPlayerResurrected += Swap;
        EventManager.OnBeforePlayerDied += Swap;
    }

    public void Swap() {
        GetComponent<BoxCollider2D>().sharedMaterial = isSwapped ? originalMaterial : swapMaterial;
        isSwapped = !isSwapped;
    }
}
