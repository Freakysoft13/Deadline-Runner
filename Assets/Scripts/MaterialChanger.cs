using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour {

    public PhysicsMaterial2D swapMaterial;

    public void Swap()
    {
        GetComponent<BoxCollider2D>().sharedMaterial = swapMaterial;
    }
}
