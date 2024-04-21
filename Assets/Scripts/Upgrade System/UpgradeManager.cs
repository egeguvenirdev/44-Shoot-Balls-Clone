using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public void Init()
    {
        ActionManager.GameplayUpgrade += OnUpgrade;
    }

    public void DeInit()
    {
        ActionManager.GameplayUpgrade -= OnUpgrade;
    }

    public void OnUpgrade(UpgradeType type, float value)
    {
        switch (type)
        {
            case UpgradeType.Income:
                IncomeUpgrade(value);
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }
    }

    private void IncomeUpgrade(float value)
    {
        if (value < 1)
        {
            ActionManager.UpdateMoneyMultiplier?.Invoke(1);
            return;
        }
        ActionManager.UpdateMoneyMultiplier?.Invoke(value);
    }
}
