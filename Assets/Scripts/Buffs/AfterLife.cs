using UnityEngine;
using System.Collections;
using System;

namespace Effect
{
    public class AfterLife : Effect
    {
        void Start() {
            duration = duration != 0 ? duration : LevelManager.Instance.afterLifeTimers[LevelManager.Instance.GetPowerUpLevel(type)];
            if (GameManager.Instance.upgradePassives) {
                duration += upgradeDuration;
            }
        }

        public override void ApplyEffect(Player player) {
            player.AfterLifeStart();
        }

        public override void WearOff(Player player) {
        }

        public override void RemoveEffect(Player player) {
            player.AfterLifeEnd();
        }
    }
}