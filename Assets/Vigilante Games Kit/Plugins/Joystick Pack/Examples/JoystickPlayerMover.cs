using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerMover : MonoBehaviour
{
    [Header("Movement Properties")]
    private float bodySpeed = 5;
    [SerializeField] private float minSpeed = 1;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float clampValueXneg = 4.8f;
    [SerializeField] private float clampValueXpos = 4.8f;
    [SerializeField] private float clampValueZneg = 4.8f;
    [SerializeField] private float clampValueZpos = 4.8f;
    [SerializeField] private int gap = 5;
    [SerializeField] private Transform localMover;
    [SerializeField] private Transform character;

    private List<Vector3> PositionsHistory = new List<Vector3>();

    private VariableJoystick variableJoystick;
    private bool canMove;
    private Transform targetPos;

    public void Init()
    {
        variableJoystick = FindObjectOfType<VariableJoystick>();
        bodySpeed = maxSpeed;
        canMove = true;
    }

    public void DeInit()
    {
        bodySpeed = 0;
        canMove = false;
    }

    public void ReduceSpeed()
    {
        bodySpeed = minSpeed;
    }

    public void IncreaseSpeed()
    {
        bodySpeed = maxSpeed;
    }


    private void Update()
    {
        if (!canMove) return;

        //targetPos = DetectEnemies();

        if (targetPos != null) character.LookAt(targetPos);

        if (Input.GetMouseButton(0) && variableJoystick.Direction != Vector2.zero)
        {
            OnPlayerMove();
            //animController.PlayRunAnimation(1);
            //lookat
            float angle = Vector3.Angle(new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical), Vector3.forward);
            if (variableJoystick.Horizontal < 0) angle *= -1;
            localMover.rotation = Quaternion.Euler(0f, angle, 0f);
            localMover.localPosition += localMover.forward * bodySpeed * Time.deltaTime;

            Vector3 pos = localMover.localPosition;

            pos.x = Mathf.Clamp(pos.x, clampValueXneg, clampValueXpos);
            pos.y = 0;
            pos.z = Mathf.Clamp(pos.z, clampValueZneg, clampValueZpos);

            localMover.localPosition = pos;
            return;
        }
        //animController.PlayIdleAnimation(1);
    }

    private void OnPlayerMove()
    {
        PositionsHistory.Insert(0, localMover.position);
        Vector3 point = PositionsHistory[Mathf.Clamp(1 * gap, 0, PositionsHistory.Count - 1)];
        Vector3 moveDirection = point - character.transform.position;
        character.position += moveDirection * bodySpeed * Time.deltaTime;
        //character.LookAt(localMover);
    }
}