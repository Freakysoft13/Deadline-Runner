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

        public static EffectManager Instance
        {
            get
            {
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        void Start()
        {
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            pickupSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            for (int i = 0; i < effects.Count; i++)
            {
                Effect effect = effects[i];
                if (effect.PickUpTime != 0 && Mathf.Abs(effect.PickUpTime - Time.time) > effect.duration)
                {
                    effect.RemoveEffect(player);
                    effect.PickUpTime = 0;
                    effects.RemoveAt(i);
                }
                if (effect.PickUpTime != 0 && effect.duration - (Time.time - effect.PickUpTime) < effect.wearOff && !effect.isWearingOff)
                {
                    effect.WearOff(player);
                }
            }
        }

        public void AddEffect(Effect effect)
        {
            if (effects.Contains(effect))
            {
                effects.Remove(effect);
            }
            effect.ApplyEffect(player);
            effects.Add(effect);
            if (effect.isBuff)
            {
                pickupSound.Play();
            }
        }
        void ApplyEffectAchievements(Effect effect)
        {
            if (effect.isBuff)
            {
                int overallEffectsPickedUp = PlayerPrefs.GetInt("total_effects", 0);
                PlayerPrefs.SetInt("total_effects", Mathf.Clamp(++overallEffectsPickedUp, 0, 101));
#if UNITY_ANDROID
                if (overallEffectsPickedUp > 0)
                {
                    GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_i_feel_the_power, 100.0f);
                }

                if (overallEffectsPickedUp > 49)
                {
                    GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_power_surge, 100.0f);
                }

                if (overallEffectsPickedUp > 99)
                {
                    GooglePlayServices.Instance.ReportProgress(GPGIds.achievement_overpowered, 100.0f);
                }
#endif
            }
        }
    }
}
