using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelUpgrade : UpgradeBase
{
    public override void OnUpgrade(float upgradeValue)
    {
        base.OnUpgrade(upgradeValue);
        ActionManager.GameplayUpgrade?.Invoke(UpgradeType.PlayerLevelUpgrade, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }
}
