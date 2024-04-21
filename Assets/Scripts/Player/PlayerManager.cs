using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RunnerScript runnerScript;
    [SerializeField] private Transform characterTransform;
    private UpgradeManager upgradeManager;

    private GameManager gameManager;
    private MoneyManager moneyManager;
    Sequence sequence;

    public Transform GetCharacterTransform
    {
        get => characterTransform;
    }

    public void Init()
    {
        gameManager = GameManager.Instance;
        moneyManager = MoneyManager.Instance;
        upgradeManager = FindObjectOfType<UpgradeManager>();
        runnerScript.Init();
        upgradeManager.Init();
    }

    public void DeInit()
    {
        runnerScript.DeInit();
        upgradeManager.DeInit();
    }
}
