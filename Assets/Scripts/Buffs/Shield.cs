﻿namespace Effect
{
    public class Shield : Effect
    {
        void Start() {
            duration = duration != 0 ? duration : LevelManager.Instance.shieldTimers[LevelManager.Instance.GetPowerUpLevel(type)];
        }

        public override void ApplyEffect(Player player) {
            player.ShieldsUp();
        }

        public override void RemoveEffect(Player player) {
            player.ShieldsDown();
        }
    }
}
