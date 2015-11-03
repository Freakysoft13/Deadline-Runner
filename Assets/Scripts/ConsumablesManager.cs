using System.Collections.Generic;

public class ConsumablesManager {    

    //WARNING GAVNOCODE!
    public void ApplyActiveConsumables() {
        LevelManager lm = LevelManager.Instance;
        GameManager gm = GameManager.Instance;
        List<LevelManager.Consumable> activeConsumables = lm.GetActiveConsumables();
        foreach(LevelManager.Consumable c in activeConsumables) {
            switch(c) {
                case LevelManager.Consumable.DOUBLE_CRYSTALS: gm.scoreMultiplier = 2; lm.DeactivateConsumable(c); break;
                case LevelManager.Consumable.DOUBLE_EXP: gm.expMultiplier = 2; lm.DeactivateConsumable(c); break;
                case LevelManager.Consumable.POWER_START: gm.Player.headStart = true; lm.DeactivateConsumable(c); break;
                case LevelManager.Consumable.COLLECTOR_PET: lm.DeactivateConsumable(c); break;
            }
        }
    }
}
