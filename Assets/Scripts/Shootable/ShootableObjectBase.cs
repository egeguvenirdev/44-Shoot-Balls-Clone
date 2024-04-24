using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VigilanteGamesUtilies;

public abstract class ShootableObjectBase : MonoBehaviour
{
    [Header("Shootable Object Settings")]
    [SerializeField] private Transform ballTargetPos;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private float value;
    [SerializeField] private float followDuration;

    private ShootableObjInfo infos;

    internal ShootableObjInfo Infos { get => infos; set => infos = value; }

    public virtual void Init()
    {
        Infos = new ShootableObjInfo(ballTargetPos, upgradeType, value, followDuration);
    }
}
