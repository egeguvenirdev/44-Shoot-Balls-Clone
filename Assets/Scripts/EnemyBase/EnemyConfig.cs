using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct EnemyConfig
{
    public readonly float Health;
    public readonly float Power;
    public readonly int Money;

    public EnemyConfig(float health, float power, int money)
    {
        Health = health;
        Power = power;
        Money = money;
    }
}
