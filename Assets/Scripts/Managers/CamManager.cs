using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class CamManager : MonoBehaviour
{
    [Header("Camera Components")]
    [SerializeField] private Camera ortograficCam;

    [Header("Cam Follow Settings")]
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float playerFollowSpeed = 0.125f;
    [SerializeField] private float clampLocalX = 1.5f;

    [Header("Shake Settings")]
    [SerializeField] private MMCameraShaker camShaker;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeAmplitude;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private float shakeAmplitudeX;
    [SerializeField] private float shakeAmplitudeY;
    [SerializeField] private float shakeAmplitudeZ;
    [SerializeField] private bool unscaledTime;

    private Transform player;

    public void Init()
    {
        ActionManager.Updater += OnUpdate;
        ActionManager.CamShake += OnCamShake;
        ActionManager.GetOrtograficScreenToWorldPoint += OnGetOrtograficCam;

        player = FindObjectOfType<PlayerManager>().GetCharacterTransform;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
        ActionManager.CamShake -= OnCamShake;
        ActionManager.GetOrtograficScreenToWorldPoint -= OnGetOrtograficCam;

        transform.position = Vector3.zero;
    }

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            camShaker.ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency, shakeAmplitudeX, shakeAmplitudeY, shakeAmplitudeZ, unscaledTime);
        }
    }*/

    private void OnUpdate(float deltaTime)
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + followOffset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -clampLocalX, clampLocalX);
            transform.position = Vector3.Lerp(transform.position, targetPosition, playerFollowSpeed * deltaTime);
        }
    }

    private Vector3 OnGetOrtograficCam(Vector3 targetPos)
    {
        return ortograficCam.ScreenToWorldPoint(targetPos);
    }

    private void OnCamShake()
    {
        camShaker.ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency, shakeAmplitudeX, shakeAmplitudeY, shakeAmplitudeZ, unscaledTime);
    }
}