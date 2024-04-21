using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager
{
    private static readonly Dictionary<float, WaitForSecondsData> waitForSecondsPool = new Dictionary<float, WaitForSecondsData>();
    private static readonly List<float> removalList = new List<float>();
    private static float unassignedDisposeCoolDown = 30f;

    public static WaitForSeconds GetTime(float desiredTime, float desiredTimeDisposeCoolDown)
    {
        if (waitForSecondsPool.TryGetValue(desiredTime, out WaitForSecondsData waitForSeconds))
        {
            //waitForSecondsPool[desiredTime] =  new WaitForSecondsData(desiredTime, desiredTimeDisposeCoolDown);
            return waitForSeconds.WaitForSeconds;
        }
        else
        {
            var waitForSecondsData = new WaitForSecondsData(desiredTime, desiredTimeDisposeCoolDown);
            waitForSecondsPool.Add(desiredTime, waitForSecondsData);
            Debug.Log($"WaitForSecondsPool Adding {desiredTime} Size {waitForSecondsPool.Count}");
            return waitForSecondsData.WaitForSeconds;
        }
    }

    // Like the update method of mono behaviours
    public static void Tick()
    {
        removalList.Clear();

        foreach (var (desiredTime, waitForSecondsData) in waitForSecondsPool)
        {
            if (waitForSecondsData.DesiredTimeDisposeCooldown <= Time.time)
                removalList.Add(desiredTime);
        }

        foreach (var desiredTime in removalList)
            waitForSecondsPool.Remove(desiredTime);
    }
}

public readonly struct WaitForSecondsData
{
    public readonly WaitForSeconds WaitForSeconds;
    public readonly float DesiredTimeDisposeCooldown; // To remove this data from pool

    public WaitForSecondsData(float desiredTime, float desiredTimeDisposeCooldown)
    {
        WaitForSeconds = new WaitForSeconds(desiredTime);
        DesiredTimeDisposeCooldown = Time.time + desiredTimeDisposeCooldown;
    }
}
