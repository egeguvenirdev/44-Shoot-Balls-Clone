using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RunnerScript runnerScript;
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private BallMachine ballMachine;
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
      
        ballMachine.Init();
    }

    public void OnGameStart()
    {
        OnStartShooting();
        runnerScript.Init();
        shooter.Init(ballMachine);
    }

    public void DeInit()
    {
        runnerScript.DeInit();
        ballMachine.DeInit();
        shooter.DeInit();
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
