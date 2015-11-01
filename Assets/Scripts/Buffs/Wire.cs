using System;

namespace Effect
{
    public class Wire : Effect
    {
        public override void ApplyEffect(Player player) {
            player.CanFlip = false;
        }

        public override void RemoveEffect(Player player) {
            player.CanFlip = true;
        }
    }
}
