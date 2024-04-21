using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private int value;

    [Header("Gate Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text gateValue;
    [SerializeField] private TMP_Text gateType;
    [SerializeField] private Collider thisTrigger = null;
    [SerializeField] private Collider otherTrigger = null;

    private void Start()
    {
        SetGatesInfos();
    }

    private void SetGatesInfos()
    {
        if (value > 0) gateValue.text = "+" + value;
        if (value < 0) gateValue.text = "" + value;
        gateType.text = GetUpgradeName(upgradeType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out PlayerManager playerManager))
        {
            //playerManager.InGameUpgrades(upgradeType, value);
            OnPlayerEnter();
        }
    }

    private void OnPlayerEnter()
    {
        TurnToGrey();
        CloseTriggers();
        PlayDoPunch();
    }

    private void TurnToGrey()
    {
        spriteRenderer.color = new Color32(118, 118, 118, 255);
    }

    private void CloseTriggers()
    {
        thisTrigger.enabled = false;
        if (otherTrigger != null)
        {
            otherTrigger.enabled = false;
        }
    }

    private void PlayDoPunch()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
    }

    public string GetUpgradeName(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Income:
                return ConstantVariables.UpgradeTypes.Income;
            case UpgradeType.FireRate:
                return ConstantVariables.UpgradeTypes.FireRate;
            case UpgradeType.FireRange:
                return ConstantVariables.UpgradeTypes.FireRange;
            default:
                return "?";
        }
    }
}