using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateMover : MonoBehaviour
{
    private Vector3 swerveVector;
    [SerializeField] private float swerveFloat = -1.2f;

    void Start()
    {
        DOTween.Init();
        StartMovement();
    }


    private void StartMovement()
    {
        swerveVector = new Vector3(swerveFloat, transform.position.y, transform.position.z);
        transform.DOMove(swerveVector, 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
