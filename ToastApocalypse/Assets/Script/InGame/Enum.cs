using System.Collections;
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
    Harvest,
    Resistance
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
    Spike,
    Heal
}
public enum eTrapObjectType
{
    normal,
    SkillObj
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
    Active,
    Passive
}

public enum eWeaponType
{
    Melee,
    Range,
    Fire
}

public enum eEnemyBulletType
{
    normal,
    homing,
    boom,
    Ray
}

public enum ePlayerBulletType
{
    normal,
    boomerang,
    shotgun,
    granade,
    fire,
    boom,
    homing
}

public enum eBulletEffect
{
    none,
    stun,
    slow,
    attackslow
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
    Statue,
    Slot,
    Start
}

public enum eBuffType
{
    Buff,
    Debuff
}

public enum eSkilltype
{
    Barrier,
    DamageCollider,
    None
}


public enum eTutorialStage
{
    Artifact,
    Item,
    Statue,
    Combat1,
    Combat2,
    Skill,
    End,
    Start,
    Boss
}