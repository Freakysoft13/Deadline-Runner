using UnityEngine;
using System.Collections.Generic;
namespace Effect
{
    public class EffectManager : MonoBehaviour
    {
        private List<Effect> effects = new List<Effect>();
        private Player player;
        private AudioSource pickupSound;

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
            pickupSound = GetComponent<AudioSource>();
        }

        void Update() {
            for (int i = 0; i < effects.Count; i++) {
                Effect effect = effects[i];
                if (effect.PickUpTime != 0 && Mathf.Abs(effect.PickUpTime - Time.time) > effect.duration) {
                    effect.RemoveEffect(player);
                    effect.PickUpTime = 0;
                    effects.RemoveAt(i);
                }
                if (effect.PickUpTime != 0 && effect.duration - (Time.time - effect.PickUpTime) < effect.wearOff && !effect.isWearingOff) {
                    effect.WearOff(player);
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
            pickupSound.Play();
        }
    }
}
