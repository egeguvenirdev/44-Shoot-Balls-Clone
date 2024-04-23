//using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : PoolableObjectBase, IDamageable
{
    [Header("Properties")]
    [SerializeField] protected Transform model;
    [SerializeField] protected SkinnedMeshRenderer mesh;
    [SerializeField] protected Collider mainCol;
    [SerializeField] protected AudioClip clip;
    //[SerializeField] [EnumFlags] private DropType dropType;
    private Collider[] ragDollColliders;
    private Rigidbody[] limbsRigidbodies;

    [Header("Stats")]
    [SerializeField] protected EnemyInfos enemyInfos;
    [SerializeField] protected PoolObjectType[] dropTypes;
    [SerializeField] private int level = 1;
    [SerializeField] protected int money = 100;
    private float maxHealth;
    private float speed;
    protected float range;
    protected float attackDamage;
    protected float directionMultiplier = 1f;
    private float currentHealth;

    [Header("Animation")]
    [SerializeField] protected SimpleAnimancer animancer;
    [SerializeField] protected AnimationClip idleAnim;
    [SerializeField] protected AnimationClip runAnim;
    [SerializeField] protected NavMeshAgent agent;

    protected bool canMove = true;
    protected bool isRunning = false;

    protected MaterialPropertyBlock matBlock;
    protected ObjectPooler pooler;
    protected VibrationManager vibration;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
        vibration = VibrationManager.Instance;

        matBlock = new MaterialPropertyBlock();
        mesh.SetPropertyBlock(matBlock);

        CloseRagdoll(); 
        animancer.PlayAnimation(idleAnim);
    }

    public override void Init()
    {
        ActionManager.AiUpdater += MoveTowardsPlayer;
        ActionManager.GameEnd += OnGameEnd;

        agent.isStopped = false;
        agent.updateRotation = false;
        canMove = true;
        SetProperties();
    }

    public void DeInit(Vector3 hittersPos)
    {
        ActionManager.AiUpdater -= MoveTowardsPlayer;
        ActionManager.GameEnd -= OnGameEnd;

        if (agent.navMeshOwner) agent.isStopped = true;
        if (agent.navMeshOwner) canMove = false;
        StartCoroutine(DieCo(hittersPos));
    }

    protected abstract void MoveTowardsPlayer(Vector3 player);

    protected abstract void CheckDirection();

    private void OnGameEnd(bool playerWin)
    {
        StopAllCoroutines();
        agent.isStopped = true;
        canMove = false;
        animancer.PlayAnimation(idleAnim);
    }

    private void SetProperties()
    {
        EnemyInfos.CharacterPref currentLevel = enemyInfos.GetCharacterPrefs[level - 1];

        maxHealth = currentLevel.maxHealth;
        speed = currentLevel.speed;
        range = currentLevel.range;
        attackDamage = currentLevel.attackDamge;

        agent.speed = speed;
    }

    public void TakeDamage(float damage, Vector3 hittersPos)
    {
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        currentHealth -= damage;

        SlideText hitText = pooler.GetPooledText();
        hitText.gameObject.SetActive(true);
        hitText.SetTheText("", (int)damage, Color.red, transform.position);
        vibration.SoftVibration();

        var particle = pooler.GetPooledObjectWithType(PoolObjectType.BloodParticle);
        particle.gameObject.SetActive(true);
        particle.transform.position = transform.position;
        particle.Init();

        ActionManager.PlaySound?.Invoke(clip);
        if (currentHealth <= 0)
        {
            var drop = pooler.GetPooledObjectWithType(dropTypes[Random.Range(0, dropTypes.Length)]);
            drop.gameObject.SetActive(true);
            drop.transform.position = transform.position;
            drop.Init();
            DeInit(hittersPos);
        }
    }

    private void OnFear(float duration)
    {
        if (!gameObject.activeInHierarchy) return;
        StopAllCoroutines();
        StartCoroutine(FearCo(duration));
    }

    private IEnumerator FearCo(float duration)
    {
        canMove = false;
        agent.isStopped = false;
        directionMultiplier = -1;
        isRunning = true;
        yield return CoroutineManager.GetTime(duration, 30f);
        directionMultiplier = 1;
        canMove = true;
    }

    private IEnumerator DieCo(Vector3 hittersPos)
    {
        matBlock.SetColor("_Color", Color.gray);
        mesh.SetPropertyBlock(matBlock);

        animancer.enabled = false;
        OpenRagdoll(hittersPos);

        ActionManager.GatherGameplayUpgrade?.Invoke(UpgradeType.Money, money);

        yield return CoroutineManager.GetTime(2f, 30f);
        gameObject.SetActive(false);
    }

    public void GetRagdollBits()
    {
        ragDollColliders = gameObject.GetComponentsInChildren<Collider>();
        limbsRigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    public void CloseRagdoll()
    {
        if (ragDollColliders == null) GetRagdollBits();

        foreach (Collider col in ragDollColliders)
        {
            if (!col == mainCol) col.enabled = false;
        }
        foreach (Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = true;
        }
    }

    public void OpenRagdoll(Vector3 player)
    {
        if (ragDollColliders == null) GetRagdollBits();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = false;
            rigid.AddForce(Vector3.forward * 10f, ForceMode.VelocityChange);
            rigid.AddForce(transform.up * 3.5f, ForceMode.VelocityChange);
            rigid.AddForce(transform.right * Random.Range(-13.7f, 13.7f), ForceMode.VelocityChange);
        }
    }
}