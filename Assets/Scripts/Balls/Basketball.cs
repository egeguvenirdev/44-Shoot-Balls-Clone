using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Basketball : PoolableObjectBase
{
    [Header("Components")]
    [SerializeField] private Collider col;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject[] balls;
    private ObjectPooler pooler;

    [Header("Throw Settings")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpDuration;
    [SerializeField] private float forceValue = 5;
    [SerializeField] private Ease throwEase;

    [Header("Visual effects")]

    private float ballValue = 0.5f;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }

    public override void Init()
    {
        col.enabled = true;
        col.isTrigger = true;
        rb.isKinematic = true;
        rb.useGravity = false;

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].SetActive(false);
        }
    }

    public void DeInit()
    {
        col.enabled = false;
        transform.parent = pooler.transform;
    }

    public void SetSkin(float ballValue)
    {
        if (ballValue > 30)
        {
            balls[2].SetActive(true);
        }
        else if (ballValue > 15)
        {
            balls[1].SetActive(true);
        }
        else
        {
            balls[0].SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TEST");
        PoolableObjectBase particle = pooler.GetPooledObjectWithType(PoolObjectType.BallHitParticle);
        Debug.Log(particle);
        particle.transform.position = transform.position;
        particle.gameObject.SetActive(true);
        particle.Init();
    }

    public void ThrowTheBall(Transform target, float distance)
    {
        col.enabled = true;
        col.isTrigger = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        if (target != null)
        {
            transform.parent = target;
            transform.DOLocalJump(target.localPosition, jumpHeight, 1, jumpDuration).SetEase(throwEase);
        }
        else
        {
            Vector3 jumpPos = new Vector3(transform.position.x, 0.2875f, transform.position.z + distance);
            transform.DOLocalJump(jumpPos, jumpHeight, 1, jumpDuration).SetEase(throwEase).OnComplete(
                () =>
                {

                    rb.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 0, 3) * forceValue);
                });
        }
    }
}
