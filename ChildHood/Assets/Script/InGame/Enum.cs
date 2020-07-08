﻿using System.Collections;
using System.Collections.Generic;

public enum eMonsterState
{
    Idle,
    Traking,
    Die
}

public enum eTextType
{
    Gold,
    HP,
    Atk,
    Def,
    AtkSpd,
    Spd,
}

public enum eStatueType
{
    Heal,
    Strength,
    Speed,
    Def
}

public enum eBuffType
{
    Atk,
    Spd,
    AtkSpd,
    Def
}

public enum eEnemyType
{
    Normal,
    Boss
}

public enum eTrapType
{
    Damage,
    Slow
}

public enum eDirection
{
    Up,
    Down,
    Right,
    Left
}

public enum eChestType
{
    Wood,
    Silver,
    Gold
}

public enum eArtifactType
{
    Use,
    Passive
}

public enum eWeaponType
{
    Melee,
    Range
}

public enum eBulletType
{
    normal,
    homing
}
