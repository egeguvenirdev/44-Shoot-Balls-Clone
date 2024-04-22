using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeBase[] upgrades;

    public void Init()
    {
        ActionManager.GameplayUpgrade += OnUpgrade;
        ActionManager.GamePlayUpgradeValue += OnGamePlayUpgradeValue;

        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].Init();
        }
    }

    public void DeInit()
    {
        ActionManager.GameplayUpgrade -= OnUpgrade;
        ActionManager.GamePlayUpgradeValue -= OnGamePlayUpgradeValue;

        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].DeInit();
        }
    }

    private void OnUpgrade(UpgradeType type, float value)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            if (upgrades[i].UpgradeType == type)
            {
                upgrades[i].OnUpgrade(value);
            }
        }
    }

    private float OnGamePlayUpgradeValue(UpgradeType upgradeType)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            if(upgrades[i].UpgradeType == upgradeType) return upgrades[i].GetCurrentValue();
        }
        return 0;
    }
}
