using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SimpleAnimancer animancer;
    private BallMachine ballMachine;

    [Header("Shooting Settings")]
    [SerializeField] private float shootingCooldown = 1f;
    private float speedMultiplier = 1;

    public void Init(BallMachine ballMachine)
    {
        this.ballMachine = ballMachine;
        ActionManager.DistributeGameplayUpgradeValue += OnSpeedUpgrade;

        StartCoroutine(ShootCo());
    }

    public void DeInit()
    {
        ActionManager.DistributeGameplayUpgradeValue -= OnSpeedUpgrade;
        StopAllCoroutines();
    }

    private void OnSpeedUpgrade(UpgradeType upgradeType, float value)
    {
        if (upgradeType == UpgradeType.Speed)
        {
            speedMultiplier = value;
        }
    }

    private IEnumerator ShootCo()
    {
        while (true)
        {
            //instantiate balls
            ballMachine.GetBalls();
            ballMachine.SlideTheBalls();
            Debug.Log(shootingCooldown + " + " + speedMultiplier);
            yield return CoroutineManager.GetTime(shootingCooldown / speedMultiplier, 30f);
        }
    }
}
