using UnityEngine;

public class CaveSound : MonoBehaviour {

    private AudioSource source;
    
    void Start() {
        source = GetComponent<AudioSource>();
    }
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        source.Play();
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        source.Stop();
    }
}
