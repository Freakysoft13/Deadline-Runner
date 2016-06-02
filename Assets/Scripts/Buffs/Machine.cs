namespace Effect
{
    public class Machine : Effect
    {
        public override void ApplyEffect(Player player) {
            player.ActivateCar();
        }

        public override void WearOff(Player player) {
        }

        public override void RemoveEffect(Player player) {
        }
    }
}
