using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceUpgrade : UpgradeBase
{
    public override void OnUpgrade(float upgradeValue)
    {
        base.OnUpgrade(upgradeValue);
        ActionManager.GameplayUpgrade?.Invoke(UpgradeType.Distance, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }
}
