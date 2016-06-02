namespace Effect
{
    public class Machine : Effect
    {
        public override void ApplyEffect(Player player) {
            player.ToggleCar(true);
        }

        public override void WearOff(Player player) {
        }

        public override void RemoveEffect(Player player) {
        }
    }
}
