namespace Effect
{
    public class GreenCrystal : Effect
    {
        private int amt;

        void Start() {
            amt = LevelManager.Instance.crystalValues[LevelManager.Instance.GetPowerUpLevel(type)];
        }
        public override void ApplyEffect(Player player) {
            GameManager.Instance.AddCrystals(amt * (player.IsGreenCrystalDoubledPassive ? 2 : 1));
        }

        public override void RemoveEffect(Player player) {
            
        }
    }
}
