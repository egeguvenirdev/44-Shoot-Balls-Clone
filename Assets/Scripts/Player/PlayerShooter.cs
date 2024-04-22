using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private bool canShoot;

    public void Init()
    {
        StartCoroutine(ShootCo());
    }

    public void DeInit()
    {
        StopAllCoroutines();
    }

    private IEnumerator ShootCo()
    {

        while (true)
        {
            //instantiate balls
            yield return CoroutineManager.GetTime(ActionManager.GamePlayUpgradeValue.Invoke(UpgradeType.Speed), 30f);
        }
    }
}
