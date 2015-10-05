using UnityEngine;

namespace Effect
{
    public class Shield : Effect
    {
        void Start() {
            duration = LevelManager.Instance.shieldTimers[LevelManager.Instance.GetPowerUpLevel(type)];
        }

        public override void ApplyEffect(Player player) {
            player.ShieldsUp();
            print("shields up");
        }

        public override void RemoveEffect(Player player) {
            player.ShieldsDown();
            print("shields down");
        }
    }
}
