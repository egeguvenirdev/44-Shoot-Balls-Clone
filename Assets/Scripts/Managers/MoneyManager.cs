using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoSingleton<MoneyManager>
{
    [Header("Money Settings")]
    [SerializeField] private int addMoney = 0;
    private float moneyMultiplier = 1;

    public float MoneyMultipler
    {
        get => moneyMultiplier;
        private set => moneyMultiplier = value;
    }

    public float Money
    {
        get => PlayerPrefs.GetFloat(ConstantVariables.TotalMoneyValue.TotalMoney, 0);
        private set
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
        ActionManager.UpdateMoneyMultiplier += OnUpdateMoneyMultiplier;
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
        ActionManager.UpdateMoneyMultiplier -= OnUpdateMoneyMultiplier;
        ActionManager.CheckMoneyAmount -= OnCheckMoneyAmount;
    }

    private void OnUpdateMoney(UpgradeType upgradeType, float value)
    {
        if (upgradeType != UpgradeType.Money) return;

        Money = value;
    }

    private void OnUpdateMoneyMultiplier(float value)
    {
        MoneyMultipler = value;
    }

    private bool OnCheckMoneyAmount(float value)
    {
        if (value <= Money) return true;
        return false;
    }
}
