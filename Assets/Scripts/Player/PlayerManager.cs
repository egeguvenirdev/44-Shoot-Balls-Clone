using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RunnerScript runnerScript;
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private Transform characterTransform;

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

        OnStartShooting();
        runnerScript.Init();
    }

    public void DeInit()
    {
        runnerScript.DeInit();
    }

    public void OnStartShooting()
    {

    }

    public void OnStopShooting()
    {

    }

    private IEnumerator ShootingCo()
    {
        return null;
    }
}
