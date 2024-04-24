using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Upgrade Info", menuName = "Upgrade Info")]

public class UpgradeInfosScriptable : ScriptableObject
{
    [SerializeField] private UpgradeInfo upgradeInfos;

    public UpgradeInfo GetUpgradeInfos { get => upgradeInfos; set => upgradeInfos = value; }

    [Serializable]
    public class UpgradeInfo
    {
        [Header("Infos")]
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 2;
        [ShowOnly, SerializeField] private float currentValue = 0;

        public UpgradeType UpgradeType { get => upgradeType; set => upgradeType = value; }
        public float MinValue { get => minValue; set => minValue = value; }
        public float MaxValue { get => maxValue; set => maxValue = value; }
        public float CurrentValue
        {
            get
            {
                if (currentValue > maxValue) currentValue = maxValue;
                if (currentValue < minValue) currentValue = minValue;
                return currentValue;
            }
            set
            {
                currentValue += value;
                if (currentValue > maxValue) currentValue = maxValue;
                if (currentValue < minValue) currentValue = minValue;
            }
        }

        public void ClearCurrentValue() => currentValue = minValue;
    }
}
