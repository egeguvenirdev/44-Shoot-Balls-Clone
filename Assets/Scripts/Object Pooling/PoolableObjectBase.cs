using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObjectBase : MonoBehaviour
{
    public PoolObjectType ObjectType { get => objectType; private set => objectType = value; }
    [SerializeField] private PoolObjectType objectType;

    public abstract void Init();
}
