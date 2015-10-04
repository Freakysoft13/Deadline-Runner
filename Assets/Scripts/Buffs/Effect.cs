using UnityEngine;

namespace Effect
{
    public abstract class Effect : MonoBehaviour
    {
        public ObjectTypesDataHolder.EffectType type;
        public float duration = 1.0f;

        private float pickUpTime = 0;
        private Player player;

        void Start() {
            type = ObjectTypesDataHolder.EffectType.BUFF;
        }

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

        public override bool Equals(object obj) {
            Effect personObj = obj as Effect;
            if (personObj == null)
                return false;
            else
                return type == personObj.type;
        }

        public override int GetHashCode() {
            return this.type.GetHashCode();
        }
    }
}
