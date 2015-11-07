using UnityEngine;

public class FlipInitializer : MonoBehaviour {
    
	void Start () {
        Vector3 scale = transform.localScale;
        scale.y = WorldFlipper.Instance.flipped;
        transform.localScale = scale;
    }
}
