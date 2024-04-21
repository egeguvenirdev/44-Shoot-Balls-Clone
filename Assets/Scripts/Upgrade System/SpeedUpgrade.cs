using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : UpgradeBase
{
    public override void OnUpgrade(float upgradeValue)
    {
        base.OnUpgrade(upgradeValue);
        ActionManager.GameplayUpgrade?.Invoke(UpgradeType.Speed, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }
}
