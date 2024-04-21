using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBase : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] protected UpgradeInfosScriptable upgradeInfos;

    public void Init()
    {

    }

    public void DeInit()
    {

    }

    public float GetCurrentValue()
    {
        return upgradeInfos.GetUpgradeInfos.CurrentValue;
    }

    public virtual void OnUpgrade(float upgradeValue)
    {
        upgradeInfos.GetUpgradeInfos.CurrentValue = upgradeValue;
    }
}
