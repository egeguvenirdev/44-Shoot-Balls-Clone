using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeBase[] upgrades;

    public void Init()
    {
        ActionManager.GatherGameplayUpgrade += OnUpgrade;

        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].Init();
        }
    }

    public void DeInit()
    {
        ActionManager.GatherGameplayUpgrade -= OnUpgrade;

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
                upgrades[i].OnUpgrade(type, value);
            }
        }
    }
}
