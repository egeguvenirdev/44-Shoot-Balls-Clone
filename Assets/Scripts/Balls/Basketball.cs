using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : PoolableObjectBase
{
    [Header("Components")]
    [SerializeField] private Collider col;
    [SerializeField] private Rigidbody rb;

    private float ballValue = 0.5f;

    public override void Init()
    {
        col.enabled = true;
    }

    public void DeInit()
    {
        col.enabled = false;
    }
}
