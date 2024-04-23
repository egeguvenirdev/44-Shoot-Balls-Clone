using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallMachine : MonoBehaviour
{
    [Header("Bench Settings")]
    [SerializeField] private Transform[] ballInstantiatePoints;
    [SerializeField] private List<BenchBall> balls = new();

    float currentBallValue ;
    float currentSpeedValue;

    public void Init()
    {
        ActionManager.DistributeGameplayUpgradeValue += OnGameplayUpgrade;

        UpgradeBallsValues();
    }

    public void DeInit()
    {
        ActionManager.DistributeGameplayUpgradeValue -= OnGameplayUpgrade;

        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].DeInit();
        }
    }

    public void UpgradeBallsValues()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].Init(ballInstantiatePoints[i].localPosition, currentBallValue, currentSpeedValue);
        }
    }

    private void OnGameplayUpgrade(UpgradeType upgradeType, float value)
    {
        if(upgradeType == UpgradeType.BallsUpgrade)
        {
            currentBallValue = value;
            UpgradeBallsValues();
        }

        if (upgradeType == UpgradeType.Speed)
        {
            currentSpeedValue = value;
            UpgradeBallsValues();
        }
    }

    public float GetBalls()
    {
        balls[0].DeInit();
        return balls[0].CurrentBallValue;
    }

    public void SlideTheBalls()
    {
        for (int i = 1; i < balls.Count; i++)
        {
            balls[i].MoveForward(ballInstantiatePoints[i - 1].localPosition, currentSpeedValue);
        }

        balls[0].Init(ballInstantiatePoints[3].localPosition, currentBallValue, currentSpeedValue);
        ReAdjustTheBallList();
    }

    private void ReAdjustTheBallList()
    {
        var tempBall = balls[0];
        balls.RemoveAt(0);
        balls.Add(tempBall);
    }
}
