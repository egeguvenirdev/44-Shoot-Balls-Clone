using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    private PlayerManager playerManager;
    private LevelManager levelManager;
    private UpdateManager updateManager;
    private CamManager camManager;
    private MoneyManager moneyManager;
    private UIManager uIManager;
    private BandRotator bandRotator;
    private ObjectPooler pooler;
    private AudioManager audioManager;
    private UpgradeManager upgradeManager;

    void Start()
    {
        if (clearPlayerPrefs) PlayerPrefs.DeleteAll();

        levelManager = LevelManager.Instance;
        moneyManager = MoneyManager.Instance;
        uIManager = UIManager.Instance;
        pooler = ObjectPooler.Instance;
        updateManager = FindObjectOfType<UpdateManager>();
        camManager = FindObjectOfType<CamManager>();
        audioManager = FindObjectOfType<AudioManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();

        SetInits();
    }

    private void SetInits()
    {
        levelManager.Init();
        uIManager.Init();
        moneyManager.Init();
        updateManager.Init();
        audioManager.Init();
        upgradeManager.Init();
    }

    private void DeInits()
    {
        levelManager.DeInit();
        uIManager.DeInit();
        moneyManager.DeInit();
        updateManager.DeInit();
        camManager.DeInit();
        bandRotator.DeInit();
        pooler.DeInit();
        audioManager.DeInit();
        upgradeManager.DeInit();
    }

    public void OnStartTheGame()
    {
        ActionManager.GameStart?.Invoke();

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.Init();

        camManager.Init();

        bandRotator = FindObjectOfType<BandRotator>();
        bandRotator.Init();
    }

    public void OnLevelSucceed()
    {
        levelManager.LevelUp();
        DeInits();
        SetInits();
    }

    public void OnLevelFailed()
    {
        DeInits();
        SetInits();
    }

    public void FinishTheGame(bool check)
    {
        playerManager.DeInit();

        ActionManager.GameplayUpgrade(UpgradeType.Money, 50f);
        ActionManager.GameEnd?.Invoke(check);

    }
}
