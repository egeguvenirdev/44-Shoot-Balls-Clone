using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public abstract class UpgradeCard : ButtonBase
{
    [Header("Button Settings")]
    [SerializeField] protected UpgradeType upgradeType;
    [SerializeField] protected Button button;
    [SerializeField] protected Color32 white = Color.white;
    [SerializeField] protected Color32 red = Color.red;

    [Header("Button Prices")]
    [SerializeField] protected int startPrice = 100;
    [SerializeField] protected int incrementalBasePrice = 10;
    protected int priceIncrementalValue;

    [Header("Button Upgrade Values")]
    [SerializeField] protected float upgradeValue = 1f;

    [Header("Panel Settings")]
    [SerializeField] protected TMP_Text levelText;
    [SerializeField] protected TMP_Text priceText;

    private UIManager uiManager;
    private PlayerManager playerManager;
    private VibrationManager vibrationManager;

    public override void Init()
    {
        base.Init();

        uiManager = UIManager.Instance;
        vibrationManager = VibrationManager.Instance;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public override void DeInit()
    {

    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();

        //playerManager.OnUpgrade(upgradeType, UpgradeCurrentValue);
        ActionManager.GatherGameplayUpgrade?.Invoke(upgradeType, upgradeValue);
        ActionManager.GatherGameplayUpgrade?.Invoke(UpgradeType.Money, -(float)CurrentPrice);
        SkillLevel = 1;
        button.enabled = false;

        SetButtonPrice();
        uiManager.UpgradeButtons();

        transform.DOKill(true);
        transform.DOScale(Vector3.one, 0); 
        transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 6).SetUpdate(true);
    }

    public void OnPurchase()
    {
        SetButtonApperence();
        SetButtonText();
    }

    protected void SetButtonPrice()
    {
        //CurrentPrice = startPrice + incrementalBasePrice * (SkillLevel - 1);
        CurrentPrice = startPrice + IncrementalPrice;
        IncrementalPrice = IncrementalPrice;
    }

    protected void SetButtonApperence()
    {
        if (!ActionManager.CheckMoneyAmount(CurrentPrice))
        {
            button.enabled = false;
            priceText.color = red;
        }
        else
        {
            button.enabled = true;
            priceText.color = white;
        }
    }

    protected void SetButtonText()
    {
        levelText.text = SkillLevel + ConstantVariables.LevelStats.Lv;
        priceText.text = "" + uiManager.FormatFloatToReadableString(CurrentPrice);
    }

    protected int SkillLevel
    {
        get => PlayerPrefs.GetInt(upgradeType.ToString() + ConstantVariables.LevelStats.SkillLevel, 1);
        set => PlayerPrefs.SetInt(upgradeType.ToString() + ConstantVariables.LevelStats.SkillLevel, PlayerPrefs.GetInt(upgradeType.ToString()
            + ConstantVariables.LevelStats.SkillLevel, 1) + value);
    }

    protected int CurrentPrice
    {
        get => PlayerPrefs.GetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.CurrentPrice, startPrice);
        set => PlayerPrefs.SetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.CurrentPrice, value);
    }

    protected int IncrementalPrice
    {
        get => PlayerPrefs.GetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.IncrementalPrice, incrementalBasePrice);
        set => PlayerPrefs.SetInt(upgradeType.ToString() + ConstantVariables.UpgradePrices.IncrementalPrice, PlayerPrefs.GetInt(upgradeType.ToString()
            + ConstantVariables.UpgradePrices.IncrementalPrice, incrementalBasePrice) + value);
    }

    protected void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.LevelStats.SkillLevel);
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.UpgradePrices.CurrentPrice);
        PlayerPrefs.DeleteKey(upgradeType.ToString() + ConstantVariables.UpgradePrices.IncrementalPrice);
    }
}