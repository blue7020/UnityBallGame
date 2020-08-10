﻿using System.Collections;
using System.Collections.Generic;

public enum eMonsterState
{
    Idle,
    Traking,
    Die,
    Spawning
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
    Def,
    Gold,
    War,
    Heart,
    Harvest
}
public enum eStatuePay
{
    Pay,
    Free
}


public enum eEnemyType
{
    Normal,
    Boss
}

public enum eTrapType
{
    TickSpike,
    Slow,
    Spike
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

public enum eEnemyBulletType
{
    normal,
    homing,
    boom
}

public enum ePlayerBulletType
{
    normal,
    boomerang
}

public enum eShopType
{
    Item,
    Artifact
}

public enum eRoomType
{
    Monster,
    Boss,
    Normal,
    StageEnd,
    Shop,
    Statue
}

public enum eBuffType
{
    Buff,
    Nurf
}

public enum eSkilltype
{
    Barrier,
    DamageCollider
}