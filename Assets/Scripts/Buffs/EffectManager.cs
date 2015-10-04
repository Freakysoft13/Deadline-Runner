using UnityEngine;
using System.Collections.Generic;
namespace Effect
{
    public class EffectManager : MonoBehaviour
    {

        private List<Effect> effects = new List<Effect>();
        private Player player;

        private static EffectManager instance;

        public static EffectManager Instance {
            get {
                return instance;
            }

            set {
                instance = value;
            }
        }

        void Start() {
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void Update() {
            foreach (Effect effect in effects) {
                if (effect.PickUpTime != 0 && Mathf.Abs(effect.PickUpTime - Time.time) > effect.duration) {
                    effect.RemoveEffect(player);
                    effect.PickUpTime = 0;
                }
            }
        }

        public void AddEffect(Effect effect) {
            if (effects.Contains(effect)) {
                effects.Remove(effect);
            }
            else {
                effect.ApplyEffect(player);
            }
            effects.Add(effect);
        }
    }
}
