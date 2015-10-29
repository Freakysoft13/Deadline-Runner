﻿namespace Effect
{
    public class GreenCrystal : Effect
    {
        private int amt;

        void Start() {
            amt = LevelManager.Instance.crystalValues[LevelManager.Instance.GetPowerUpLevel(type)];
        }
        public override void ApplyEffect(Player player) {
            GameManager.Instance.AddScore(amt);
        }

        public override void RemoveEffect(Player player) {
            
        }
    }
}
