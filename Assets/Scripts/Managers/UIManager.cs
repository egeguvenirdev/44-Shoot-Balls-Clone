using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private ButtonBase[] panels;
    [SerializeField] private UpgradeCard[] upgradeButtons;
    [SerializeField] private GameObject[] InGameUis;
    [SerializeField] private ButtonBase upgradePanel;

    [Header("Level & Progress Props")]
    [SerializeField] private TMP_Text currentLV;
    [SerializeField] private TMP_Text totalMoneyText;
    [SerializeField] private TMP_Text timerText;

    [Header("Health & Xp Bars")]
    [SerializeField] private Image progressBarImage;
    [SerializeField] private Image moneyImage;

    private LevelManager levelManager;
    private Tweener smoothTween;
    private float smoothMoneyNumbers = 0;

    public Transform GetMoneyImageTranform
    {
        get => moneyImage.transform;
    }

    public void Init()
    {
        levelManager = LevelManager.Instance;
        ActionManager.GameStart += OpenInGameUis;
        ActionManager.GameEnd += CloseInGameUis;

        LevelText();

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Init();
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].Init();
        }
    }

    public void DeInit()
    {
        ActionManager.GameStart -= OpenInGameUis;
        ActionManager.GameEnd -= CloseInGameUis;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].DeInit();
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].DeInit();
        }
    }

    public void LevelText()
    {
        int levelInt = levelManager.LevelIndex + 1;
        currentLV.text = "Level " + levelInt;
    }

    public void TimerText(string refText)
    {
        timerText.text = refText;
    }

    public void SetProgress(float progress)
    {
        progressBarImage.fillAmount = progress;
    }

    public void UpgradeButtons()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].OnPurchase();
        }
    }

    private void OpenInGameUis()
    {
        for (int i = 0; i < InGameUis.Length; i++)
        {
            InGameUis[i].SetActive(true);
        }
    }

    public void CloseInGameUis(bool check)
    {
        for (int i = 0; i < InGameUis.Length; i++)
        {
            InGameUis[i].SetActive(false);
        }
        upgradePanel.DeInit();
    }

    #region Money
    public void SetMoneyUI(float totalMoney, bool setSmoothly)
    {
        //totalMoneyText.text = money;

        if (setSmoothly)
        {
            smoothTween.Kill();
            smoothTween = DOTween.To(() => smoothMoneyNumbers, x => smoothMoneyNumbers = x, totalMoney, 0.5f).SetSpeedBased(false).OnUpdate(() => { UpdateMoneyText(); });
        }
        else
        {
            smoothTween.Kill();
            smoothMoneyNumbers = totalMoney;
            UpdateMoneyText();
        }

        UpgradeButtons();
    }

    private void UpdateMoneyText()
    {
        totalMoneyText.text = FormatFloatToReadableString(smoothMoneyNumbers);
    }
    #endregion

    public string FormatFloatToReadableString(float value)
    {
        float number = value;
        if (number < 1000)
        {
            return ((int)number).ToString();
        }
        string result = number.ToString();

        if (result.Contains(","))
        {
            result = result.Substring(0, 4);
            result = result.Replace(",", string.Empty);
        }
        else
        {
            result = result.Substring(0, 3);
        }

        do
        {
            number /= 1000;
        }
        while (number >= 1000);
        number = Mathf.CeilToInt(number);
        if (value >= 1000000000000000)
        {
            result = result + "Q";
        }
        else if (value >= 1000000000000)
        {
            result = result + "T";
        }
        else if (value >= 1000000000)
        {
            result = result + "B";
        }
        else if (value >= 1000000)
        {
            result = result + "M";
        }
        else if (value >= 1000)
        {
            result = result + "K";
        }

        if (((int)number).ToString().Length > 0 && ((int)number).ToString().Length < 3)
        {
            result = result.Insert(((int)number).ToString().Length, ".");
        }
        return result;
    }
}
