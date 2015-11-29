using UnityEngine;
using System.Collections;
using System;

namespace Effect
{
    public class AfterLife : Effect
    {
        void Start() {
            duration = duration != 0 ? duration : LevelManager.Instance.afterLifeTimers[LevelManager.Instance.GetPowerUpLevel(type)];
        }

        public override void ApplyEffect(Player player) {
            player.AfterLifeStart();
        }

        public override void RemoveEffect(Player player) {
            player.AfterLifeEnd();
        }
    }
}