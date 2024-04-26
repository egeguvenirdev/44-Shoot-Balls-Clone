using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VigilanteGamesUtilies;

public class PlayerShooter : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SimpleAnimancer animancer;
    [SerializeField] private Transform laserPoint;
    [SerializeField] private Transform instantiatePoint;
    private BallMachine ballMachine;
    private ObjectPooler pooler;

    [Header("Shooting Settings")]
    [SerializeField] private float shootingCooldown = 1f;
    [SerializeField] private float shootingGrabCooldown = 1f;
    [SerializeField] private float shootingThrowCooldown = 1f;
    [SerializeField] private float rangeReducer = 0.75f;
    [SerializeField] private LayerMask layer;
    private ShootableObjInfo currentTargetInfos;

    private float speedMultiplier = 1;
    private float range = 1;

    public void Init(BallMachine ballMachine)
    {
        this.ballMachine = ballMachine;
        ActionManager.DistributeGameplayUpgradeValue += OnGamePlayUpgrade;
        ActionManager.Updater += OnUpdate;
        pooler = ObjectPooler.Instance;
    }

    public void OnGameStart()
    {
        StartCoroutine(ShootCo());
    }

    public void DeInit()
    {
        ActionManager.DistributeGameplayUpgradeValue -= OnGamePlayUpgrade;
        ActionManager.Updater -= OnUpdate;
        StopAllCoroutines();
    }

    private void OnUpdate(float deltaTime)
    {


        Ray ray = new Ray(laserPoint.position, transform.forward * 5);
        RaycastHit hit;
        Debug.DrawRay(laserPoint.position, transform.forward * range * rangeReducer, Color.red);

        if (Physics.Raycast(ray, out hit, range * rangeReducer, layer))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.TryGetComponent(out ShootableObjectBase shootable))
            {
                currentTargetInfos = shootable.Infos;
                shootable.Init();
                //StartCoroutine(TargetCoolDown());
            }
        }
        else
        {
            Debug.Log("no target");
            currentTargetInfos = new();
        }
    }

    private void OnGamePlayUpgrade(UpgradeType upgradeType, float value)
    {
        if (upgradeType == UpgradeType.Speed)
        {
            speedMultiplier = value;
        }

        if (upgradeType == UpgradeType.Distance)
        {
            range = value;
        }
    }

    private IEnumerator ShootCo()
    {
        while (true)
        {
            //wait for animation
            yield return CoroutineManager.GetTime(shootingGrabCooldown / speedMultiplier, 30f);

            //create balls
            Basketball ball = InstantiateBall(ballMachine.GetBalls());
            //BURADA PARENTLA

            //wait for animation to throw
            yield return CoroutineManager.GetTime((shootingThrowCooldown - shootingGrabCooldown) / speedMultiplier, 30f);
            //BURADA PARENTTAN CIKAR

            ball.ThrowTheBall(currentTargetInfos.ballTargetPos, range);

            //wait for the animation to complete
            yield return CoroutineManager.GetTime(shootingCooldown / speedMultiplier, 30f);
        }
    }

    public Basketball InstantiateBall(float ballValueballValue)
    {
        Basketball ball = pooler.GetPooledObjectWithType(PoolObjectType.Ball).GetComponent<Basketball>();
        ball.gameObject.SetActive(true);
        ball.transform.position = instantiatePoint.position;
        ball.Init();
        ball.SetSkin(ballValueballValue);
        return ball;
    }
}
