using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class ActionManager
{
    //Game Actions
    public static Action GameStart { get; set; }
    public static Action<bool> GameEnd { get; set; }
    public static Action<float> Updater { get; set; }
    public static Action<Vector3> AiUpdater { get; set; }
    public static Action<AudioClip> PlaySound { get; set; }

    //MoneyActions
    public static Action<float> UpdateMoneyMultiplier { get; set; }
    public static Predicate<float> CheckMoneyAmount { get; set; }

    //Player Actions
    public static Action<float> SwerveValue { get; set; }
    public static Action<UpgradeType, float> GameplayUpgrade { get; set; }
    public static Func<UpgradeType> GetUpgradeValue { get; set; }

    //Visual Effects
    public static Action CamShake { get; set; }
    public static Func<Vector3, Vector3> GetOrtograficScreenToWorldPoint { get; set; }

    public static void ResetAllStaticVariables()
    {
        Type type = typeof(ActionManager);
        var fields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Public);
        foreach (var fieldInfo in fields)
        {
            fieldInfo.SetValue(null, GetDefault(type));
        }
    }

    public static object GetDefault(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        return null;
    }
}
