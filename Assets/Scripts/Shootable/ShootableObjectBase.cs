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

    [SerializeField] private ShootableObjInfo infos;

    internal ShootableObjInfo Infos { get => infos; set => infos = value; }

    void Start()
    {
        for (int i = 0; i < col.Length; i++)
        {
            col[i].enabled = true;
        }

        infos = new ShootableObjInfo(ballTargetPos, upgradeType, value, followDuration);
    }

    public virtual void Init()
    {
        StartCoroutine(StartMoving());
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("basket potasinda carpisma");
        if(other.transform.parent.TryGetComponent(out Basketball ball))
        {
            GetBallValue(ball.BallValue);
        }
    }

    protected abstract void GetBallValue(float ballValue);

    private IEnumerator StartMoving()
    {
        transform.DOMove(transform.position + Vector3.forward * followDistance, followDuration);
        yield return CoroutineManager.GetTime(followDuration, 15f);
        for (int i = 0; i < col.Length; i++)
        {
            col[i].enabled = false;
        }
    }
}
