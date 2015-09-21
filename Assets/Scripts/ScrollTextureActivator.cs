using UnityEngine;
using System.Collections;

public class ScrollTextureActivator : MonoBehaviour
{

    public GameObject[] objectsToActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject g in objectsToActivate)
        {
            g.GetComponent<Animator>().SetTrigger("Play");
        }
    }
}
