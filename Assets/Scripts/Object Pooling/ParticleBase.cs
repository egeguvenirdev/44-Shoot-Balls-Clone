using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : PoolableObjectBase
{
    [SerializeField] private ParticleSystem particle;
    public override void Init()
    {
        particle.Play();
    }
}
