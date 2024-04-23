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
        ActionManager.DistributeGameplayUpgradeValue?.Invoke(upgradeType, upgradeInfos.GetUpgradeInfos.CurrentValue);
        ActionManager.ClearGameplayValues += OnClearValue;

        Debug.Log(upgradeType + " upgradeValue");
    }

    public void DeInit()
    {
        ActionManager.ClearGameplayValues -= OnClearValue;
    }

    public virtual void OnUpgrade(UpgradeType upgradeType,float upgradeValue)
    {
        if (this.upgradeType == upgradeType)
        {
            Debug.Log(upgradeType + " upgradeValue");
            upgradeInfos.GetUpgradeInfos.CurrentValue = upgradeValue;
            ActionManager.DistributeGameplayUpgradeValue?.Invoke(this.upgradeType, upgradeInfos.GetUpgradeInfos.CurrentValue);
        }
    }

    private void OnClearValue()
    {
        upgradeInfos.GetUpgradeInfos.ClearCurrentValue();
        ActionManager.DistributeGameplayUpgradeValue?.Invoke(this.upgradeType, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }
}
