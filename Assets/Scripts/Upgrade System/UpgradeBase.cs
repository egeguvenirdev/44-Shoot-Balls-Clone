using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBase : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] protected UpgradeInfosScriptable upgradeInfos;
    [SerializeField] private UpgradeType upgradeType;

    public UpgradeType UpgradeType { get => upgradeType; private set => upgradeType = value; }

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
