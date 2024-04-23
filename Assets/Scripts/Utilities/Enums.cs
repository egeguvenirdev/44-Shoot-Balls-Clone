using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Money,
    Income,
    BallsUpgrade,
    PlayerLevelUpgrade,
    Distance,
    Speed,
    DoubleBalls,
    DoubleShooter
}

public enum EnemyType
{
    Melee,
    Range
}

public enum PoolObjectType
{
    Ball,
    BloodParticle,
    SlideText
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
