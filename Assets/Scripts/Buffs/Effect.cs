using UnityEngine;

namespace Effect
{
    public abstract class Effect : MonoBehaviour
    {
        public LevelManager.PowerUp type;
        public float duration = 1.0f;
        public float wearOff = 1.0f;
        public bool isWearingOff = false;
        public float upgradeDuration = 1.0f;
        public bool disappearOnPickup = true;

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
        public virtual void WearOff(Player player) {
            isWearingOff = true;
        }

        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                PickUp();
            }
        }

        public void PickUp() {
            isWearingOff = false;
            PickUpTime = Time.time;
            EffectManager.Instance.AddEffect(this); // maybe clone?
            if (disappearOnPickup) {
                gameObject.SetActive(false);
            }
        }

        public override bool Equals(object obj) {
            Effect effect = obj as Effect;
            if (effect == null)
                return false;
            else
                return type == effect.type;
        }

        public override int GetHashCode() {
            return this.type.GetHashCode();
        }

        public override string ToString() {
            return type.ToString();
        }
    }
}
