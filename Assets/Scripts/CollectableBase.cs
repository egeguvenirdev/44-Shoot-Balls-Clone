using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableBase : PoolableObjectBase, ICollectable
{
    [Header("Collect Settings")]
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected UpgradeType upgradeType;
    [SerializeField] protected float upgradeValue = 1;

    [Header("Animation Settings")]
    [SerializeField] private Vector3 rotateVelocity;
    [SerializeField] private Space rotateSpace;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float value = 100f;
    [SerializeField] private bool rotate;
    [SerializeField] private Vector3 maxHeight = new Vector3(0f, 0.5f, 0f);

    private Vector3 minHeight;
    private bool canRotate = false;
    private Transform imageTransform;

    private UIManager uIManager;
    private Camera mainCam;

    public override void Init()
    {
        minHeight = transform.position;
        canRotate = rotate;
        gameObject.SetLayerRecursively("Collectable");

        if (uIManager == null) uIManager = UIManager.Instance;
        if (mainCam == null) mainCam = Camera.main;
        if (imageTransform == null) imageTransform = uIManager.GetMoneyImageTranform;
    }

    void Update()
    {
        if (canRotate)
        {
            transform.Rotate(rotateVelocity * Time.deltaTime, rotateSpace);

            float lerpValue = (Mathf.Sin(speed * Time.time) + 1f) / 2f; // <0, 1> 
            transform.position = Vector3.Lerp(minHeight, minHeight + maxHeight, lerpValue);
            return;
        }
    }

    public void Collect(Transform target, bool UIAnim)
    {
        canRotate = false;

        if (target != null)
        {
            Vector3 targetPos = target.position;
            transform.DOJump(targetPos, 1f, 1, 0.5f).OnUpdate(() =>
            {
                targetPos = target.position;
            }).OnComplete(() =>
            {
                if (UIAnim)
                {
                    MoveMoneyArea();
                    return;
                }
                ActionManager.GameplayUpgrade?.Invoke(upgradeType, upgradeValue);
            });

            return;
        }

        ActionManager.GameplayUpgrade?.Invoke(upgradeType, upgradeValue);
    }

    private void MoveMoneyArea()
    {
        Vector3 firstPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 secondPos = ActionManager.GetOrtograficScreenToWorldPoint.Invoke(firstPos);
        secondPos.z = imageTransform.position.z;
        transform.position = secondPos;

        gameObject.SetLayerRecursively("UI");

        Vector3[] path = new Vector3[3];
        path[0] = secondPos;
        path[1] = imageTransform.position + Vector3.down * 4f + Vector3.left * 1.5f;
        path[2] = imageTransform.position;


        transform.DOPath(path, 1f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetId(GetHashCode())
            .OnComplete(() =>
            {
                ActionManager.GameplayUpgrade?.Invoke(upgradeType, upgradeValue);
            });

        transform.DOScale(Vector3.zero, .2f)
            .SetDelay(1f)
            .SetEase(Ease.Linear)
            .SetId(GetHashCode());
    }
}