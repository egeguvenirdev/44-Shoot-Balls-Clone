using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShooterUpgrade : UpgradeBase
{
    public override void OnUpgrade(float upgradeValue)
    {
        base.OnUpgrade(upgradeValue);
        ActionManager.GameplayUpgrade?.Invoke(UpgradeType.DoubleShooter, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }
}
