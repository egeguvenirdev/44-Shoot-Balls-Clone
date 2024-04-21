using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    [Header("Money Settings")]
    [SerializeField] private int addMoney = 0;
    private float moneyMultiplier = 1;

    private float MoneyMultipler
    {
        get => moneyMultiplier;
        set => moneyMultiplier = value;
    }

    private float Money
    {
        get => PlayerPrefs.GetFloat(ConstantVariables.TotalMoneyValue.TotalMoney, 0);
        set
        {
            float calculatedMoney = value;
            if (value > 0)
            {
                calculatedMoney = value * moneyMultiplier;
            }
            PlayerPrefs.SetFloat(ConstantVariables.TotalMoneyValue.TotalMoney, PlayerPrefs.GetFloat(ConstantVariables.TotalMoneyValue.TotalMoney, 0) + calculatedMoney);
            UIManager.Instance.SetMoneyUI(Money, true);
        }
    }

    public void Init()
    {
        ActionManager.GameplayUpgrade += OnUpdateMoney;
        ActionManager.CheckMoneyAmount += OnCheckMoneyAmount;

        if (addMoney > 0)
        {
            Money = addMoney;
        }

        if (Money <= 0) Money = addMoney;
        Money = 0;
    }

    public void DeInit()
    {
        ActionManager.GameplayUpgrade -= OnUpdateMoney;
        ActionManager.CheckMoneyAmount -= OnCheckMoneyAmount;
    }

    private void OnUpdateMoney(UpgradeType upgradeType, float value)
    {
        if (upgradeType == UpgradeType.Money) Money = value;
        if (upgradeType == UpgradeType.Income) MoneyMultipler = value; ;
    }

    private bool OnCheckMoneyAmount(float value)
    {
        if (value <= Money) return true;
        return false;
    }
}
