using UnityEngine;

namespace Effect
{
    public class Shield : Effect
    {
        void Start() {
            type = ObjectTypesDataHolder.EffectType.SHIELD;
            duration = LevelManager.Instance.shieldTimers[LevelManager.Instance.GetPowerUpLevel(LevelManager.PowerUp.SHIELD)];
        }

        public override void ApplyEffect(Player player) {
            player.ShieldsUp();
        }

        public override void RemoveEffect(Player player) {
            player.ShieldsDown();
        }
    }
}
