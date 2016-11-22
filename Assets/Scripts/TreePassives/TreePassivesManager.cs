using System.Collections.Generic;

class TreePassivesManager
{
    public void ApplyActivePassives() {
        LevelManager lm = LevelManager.Instance;
        GameManager gm = GameManager.Instance;
        LevelManager.Passive activePassive = lm.GetActivePassive();
        switch (activePassive) {
            case LevelManager.Passive.WILLOW: gm.Player.IsShieldPassive = true; break;
            case LevelManager.Passive.DELMOR: gm.Player.IsCrystalBoostPassive = true; break;
            case LevelManager.Passive.ARDEN: gm.Player.IsGreenCrystalDoubledPassive = true; break;
            case LevelManager.Passive.CRIMSON: gm.Player.IsIncreasedBuffSpawnPassive = true; break;
            case LevelManager.Passive.MAXWELL: gm.Player.IsScoreDoubledPassive = true; break;
            case LevelManager.Passive.AMORET: gm.Player.IsCrystalsDoubledPassive = true; break;
            case LevelManager.Passive.MORDECAI:
                GameManager.Instance.upgradePassives = true;
                break;
            case LevelManager.Passive.MORGANA: gm.Player.IsObstacleInvurnerablePassive = true; gm.Player.IsAfterlifeBoostPassive = true; break;
            case LevelManager.Passive.ELENOR: gm.Player.IsNoAdsResPassive = true; break;
            case LevelManager.Passive.ARCHIBALD:
                gm.Player.IsShieldPassive = true;
                gm.Player.IsCrystalBoostPassive = true;
                gm.Player.IsGreenCrystalDoubledPassive = true;
                gm.Player.IsIncreasedBuffSpawnPassive = true;
                gm.Player.IsScoreDoubledPassive = true;
                gm.Player.IsCrystalsDoubledPassive = true;
                gm.Player.IsObstacleInvurnerablePassive = true;
                gm.Player.IsAfterlifeBoostPassive = true;
                gm.Player.IsNoAdsResPassive = true; break;
        }
        gm.Player.IsNoAdsResPassive = true;
    }
}