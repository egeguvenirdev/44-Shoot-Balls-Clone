using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Income,
    FireRate,
    FireRange,
    Health,
    Money
}

public enum EnemyType
{
    Melee,
    Range
}

public enum PoolObjectType
{
    PlayerThrowable,
    PlayerGravityThrowable,
    RangeEnemy,
    MeeleEnemy,
    EnemyThrowable,
    SlideText,
    Coin,
    Health,
    BloodParticle
}

[System.Flags]
public enum DropType : int
{
    Nothing = 0x00,
    Coin = 0x01,
    Health = 0x02,
    Armor = 0x04
}

public class Enums : MonoBehaviour
{
    //
}
