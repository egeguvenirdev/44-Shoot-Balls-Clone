using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBallsUpgrade : UpgradeBase
{
    public override void OnUpgrade(float upgradeValue)
    {
        base.OnUpgrade(upgradeValue);
        ActionManager.GameplayUpgrade?.Invoke(UpgradeType.DoubleBalls, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }
}
