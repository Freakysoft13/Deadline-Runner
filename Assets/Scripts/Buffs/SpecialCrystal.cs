namespace Effect
{
    public class SpecialCrystal : Effect
    {
        public int amt;

        public override void ApplyEffect(Player player) {
            GameManager.Instance.AddSpecialCurrency(amt);
        }

        public override void RemoveEffect(Player player) {
            
        }
        public override void WearOff(Player player) {

        }
    }
}
