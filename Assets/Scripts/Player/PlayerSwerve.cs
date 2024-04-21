using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwerve : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private float moveSpeed = 1000;
    [SerializeField] private float dirMultiplier = 100;

    private float multiplier = 1;
    private float dirMaxMagnitude = float.PositiveInfinity;
    private Vector2 deltaDir;
    private Vector2 joystickCenterPos;
    private float lastPos;

    private bool isControl = false;
    private Vector2 dir;
    private Vector2 dirOld;

    public void Init()
    {
        ActionManager.Updater += OnUpdate;
    }

    public void DeInit()
    {
        ActionManager.Updater -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            joystickCenterPos = (Vector2)Input.mousePosition;
            deltaDir = Vector2.zero;
            dirOld = Vector2.zero;
            isControl = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystickCenterPos = (Vector2)Input.mousePosition;
            deltaDir = Vector2.zero;
            dirOld = Vector2.zero;
            isControl = false;
        }

        if (isControl)
        {
            multiplier = dirMultiplier / Screen.width;
            dir = ((Vector2)Input.mousePosition - joystickCenterPos) * multiplier;
            float m = dir.magnitude;
            if (m > dirMaxMagnitude) dir = dir * dirMaxMagnitude / m;
            deltaDir = dir - dirOld;
            dirOld = dir;
            //lastPos += (deltaDir.x * moveSpeed * Time.deltaTime);
            ActionManager.SwerveValue?.Invoke(deltaDir.x * moveSpeed * deltaTime);
        }
    }
}
