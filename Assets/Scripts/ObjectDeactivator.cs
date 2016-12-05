using UnityEngine;
using System.Collections;

public class ObjectDeactivator : MonoBehaviour
{
    public string[] bypassColliders;

    void OnTriggerEnter2D(Collider2D other)
    {
        bool shouldByPass = false;

        foreach (string s in bypassColliders)
        {
            if (other.CompareTag(s))
            {
                shouldByPass = true;
            }
        }

        if (!shouldByPass)
        {
            if (other.CompareTag("Background"))
            {
                print("Deactivating - " + other.tag);
            }
            other.gameObject.SetActive(false);
        }
    }

}
