using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VigilanteGamesUtilies;
using DG.Tweening;
using TMPro;

public abstract class ShootableObjectBase : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform ballTargetPos;
    [SerializeField] protected Transform hitEffectPart;
    [SerializeField] private Collider[] col;
    [SerializeField] protected TMP_Text text;

    [Header("Shootable Object Settings")]
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] protected float value;
    [SerializeField] private float followDuration;
    [SerializeField] private float followDistance;

    [Header("Collectable Object Settings")]
    [SerializeField] protected bool canCollectable;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private float forwardSpeed = 2f;
    [SerializeField] private Ease throwEase = Ease.Linear;
    private bool canMove;
    private bool canMoveWithPlayer = true;

    private ShootableObjInfo infos;

    internal ShootableObjInfo Infos { get => infos; set => infos = value; }

    void Start()
    {
        for (int i = 0; i < col.Length; i++)
        {
            col[i].enabled = true;
        }

        infos = new ShootableObjInfo(ballTargetPos, upgradeType, value, followDuration);
        text.text = "" + value;
        if (value <= 0) text.color = Color.red;
    }

    public virtual void Init()
    {
        if (canMoveWithPlayer) StartCoroutine(StartMoving());
    }

    public virtual void DeInit()
    {
        canMove = false;
        this.DOKill();
    }

    private void Update()
    {
        if (canMove) transform.position += Vector3.forward * forwardSpeed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.transform.parent.TryGetComponent(out Basketball ball))
        {
            if(ball.transform.parent == transform) GetBallValue(ball.BallValue);
            ball.transform.parent = null;
            other.isTrigger = false;
            if (canCollectable) ball.DeInit();
        }
    }

    protected abstract void GetBallValue(float ballValue);

    private IEnumerator StartMoving()
    {
        canMoveWithPlayer = false;
        transform.DOMove(transform.position + Vector3.forward * followDistance, followDuration);

        yield return CoroutineManager.GetTime(followDuration, 15f);

        for (int i = 0; i < col.Length; i++)
        {
            col[i].enabled = false;
        }
    }

    protected void JumpOnToBand()
    {
        Vector3 jumpTargetPos = new Vector3(-3.021f, 0, transform.position.z + 5);
        transform.DOJump(jumpTargetPos, jumpHeight / 2, 1, jumpDuration).SetEase(throwEase).OnComplete(
            () =>
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].enabled = false;
                }
                canMove = true;
            });
    }
}
