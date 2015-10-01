using UnityEngine;

public abstract class Effect : MonoBehaviour {

    public float duration = 1.0f;

    private float pickUpTime = 0;
    private Player player;

    public float PickUpTime {
        get {
            return pickUpTime;
        }

        set {
            pickUpTime = value;
        }
    }

    public abstract void ApplyEffect(Player player);
    public abstract void RemoveEffect(Player player);

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PickUpTime = Time.time;
            EffectManager.Instance.AddEffect(this); // maybe clone?
            gameObject.SetActive(false);
        }
    }

}
