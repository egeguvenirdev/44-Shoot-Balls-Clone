using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using DG.Tweening;

public class RunnerScript : MonoBehaviour
{
    [Header("Scripts and Transforms")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform localMoverTarget;
    [SerializeField] private SimpleAnimancer animancer;
    [SerializeField] private PlayerSwerve playerSwerve;

    [Header("Path Settings")]
    [SerializeField] private float distance = 0;
    [SerializeField] private float startDistance = 0;
    [SerializeField] private float clampLocalX = 2f;

    [Header("Run Settings")]
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float localTargetSwipeSpeed = 2f;
    [SerializeField] private float characterSwipeLerpSpeed = 2f;
    [SerializeField] private float characterRotateLerpSpeed = 2f;
    [SerializeField] private bool canFollow = true;
    [SerializeField] private bool canLookAt = true;

    [Header("Animations")]
    [SerializeField] private AnimationClip runAnim;
    private AnimationClip currentAnim;

    private Vector3 oldPosition;
    private bool canRun = false;
    private bool canSwerve = false;

    public float RunSpeed
    {
        set => runSpeed = value;
    }

    public void Init()
    {
        ActionManager.SwerveValue += PlayerSwipe_OnSwerve;
        ActionManager.Updater += OnUpdate;

        distance = startDistance;

        //oldPosition = localMoverTarget.localPosition;
        //PlayAnimation(runAnim);

        StartToRun(true);
    }

    public void DeInit()
    {
        ActionManager.SwerveValue -= PlayerSwipe_OnSwerve;
        ActionManager.Updater -= OnUpdate;
        StartToRun(false);
    }

    private void OnUpdate(float deltaTime)
    {
        //UpdatePath();
        FollowLocalMoverTarget(deltaTime);
        oldPosition = model.localPosition;
    }

    private void StartToRun(bool checkRun)
    {
        if (checkRun)
        {
            playerSwerve.Init();
            canRun = true;
            canSwerve = true;
        }
        else
        {
            playerSwerve.DeInit();
            canRun = false;
            canSwerve = false;
        }
    }

    private void PlayerSwipe_OnSwerve(float direction)
    {
        if (!canSwerve) return;
        localMoverTarget.localPosition += Vector3.right * direction * localTargetSwipeSpeed * Time.deltaTime;
        ClampLocalPosition();
    }

    void ClampLocalPosition()
    {
        Vector3 pos = localMoverTarget.localPosition;
        pos.x = Mathf.Clamp(pos.x, -clampLocalX, clampLocalX);
        localMoverTarget.localPosition = pos;
    }

    void FollowLocalMoverTarget(float deltaTime)
    {
        /*//pathrunner
        distance += runSpeed * deltaTime;
        localMoverTarget.position = pathCreator.path.GetPointAtDistance(distance);
        localMoverTarget.eulerAngles = pathCreator.path.GetRotationAtDistance(distance).eulerAngles + new Vector3(0f, 0f, 90f);*/

        //classic forward runner
        if (canRun) localMoverTarget.Translate(transform.forward * deltaTime * runSpeed);


        //follower character
        if (canLookAt)
        {
            Vector3 direction = localMoverTarget.localPosition - oldPosition;
            model.transform.forward = Vector3.Lerp(model.transform.forward, direction, characterRotateLerpSpeed * deltaTime);
        }

        if (canFollow)
        {
            //swipe the object
            Vector3 nextPos = new Vector3(localMoverTarget.localPosition.x, model.localPosition.y, localMoverTarget.localPosition.z); ;
            model.localPosition = Vector3.Lerp(model.localPosition, nextPos, characterSwipeLerpSpeed * deltaTime);
        }
    }

    public void DodgeBack()
    {
        StartCoroutine(DodgeBackProcess());
    }

    IEnumerator DodgeBackProcess()
    {
        canSwerve = false;
        canRun = false;
        //PlayAnimation();

        yield return new WaitForSeconds(0.633f);

        //PlayAnimation();
        canRun = true;
        canSwerve = true;
    }

    public void PlayAnimation(AnimationClip anim)
    {
        animancer.PlayAnimation(anim);
        currentAnim = anim;
    }

    public void PlayAnimationWithSpeed(AnimationClip anim, float speed)
    {
        animancer.PlayAnimation(anim);
        animancer.SetStateSpeed(speed);
        currentAnim = anim;
    }
}
