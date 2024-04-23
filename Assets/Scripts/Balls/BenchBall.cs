using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BenchBall : MonoBehaviour
{
    [Header("Ball Components")]
    [SerializeField] private GameObject ballElements;
    [SerializeField] private TMP_Text ballText;

    [Header("Ball Animation Settings")]
    [SerializeField] private float ballInstantiateDuration = 0.5f;
    [SerializeField] private float ballMoveDuration = 0.5f;
    [SerializeField] private Ease animationEase = Ease.Linear;
    [SerializeField] private ParticleSystem dissapperParticle;

    private float currentBallValue = 0.5f;
    public float CurrentBallValue { get => currentBallValue; private set => currentBallValue = value; }

    private Tween tween;

    public void Init(Vector3 positionOfBall, float ballValue, float speedMultiplier)
    {
        Debug.Log(speedMultiplier);
        CurrentBallValue = ballValue;

        ballElements.SetActive(true);
        ballText.text = "" + CurrentBallValue;
        transform.localPosition = positionOfBall;

        var main = dissapperParticle.main;
        main.simulationSpeed = speedMultiplier;
        dissapperParticle.Play();

        tween.Kill();
        transform.localScale = Vector3.zero;
        tween = transform.DOScale(Vector3.one, ballInstantiateDuration / speedMultiplier).SetEase(animationEase);
    }

    public void DeInit()
    {
        ballElements.SetActive(false);
    }

    public void MoveForward(Vector3 positionOfBall, float speedMultiplier)
    {
        transform.DOLocalMove(positionOfBall, ballMoveDuration / speedMultiplier).SetEase(animationEase);
    }
}
