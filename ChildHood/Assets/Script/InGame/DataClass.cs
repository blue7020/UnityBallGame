using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float Gold;

    public int ID;
    public string Name;
    public string EngName;

    public float Hp;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public bool IsPercent;
    public float Crit;
    public float CritDamage;
    public float CooltimeReduce;
    public float CCReduce;

    public float Damage;

    public float Skill_Cooltime;
    public float Skill_Duration;
}

[Serializable]
public class WeaponStat
{
    public int ID;
    public string Name;
    public string EngName;

    public float Atk;
    public float AtkSpd;
    public float ReloadCool;
    public int Bullet;
    public float Crit;
    public float CritDamage;
}

[Serializable]
public class MonsterStat
{
    public int ID;
    public int Gold;

    public float Hp;
    public float Atk;
    public float AtkSpd;
    public float Spd;

    public float Skill_Cooltime;
    public float Skill_Duration;

}

[Serializable]
public class ItemStat
{
    public int ID;
    public int Price;

    public float Heal;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public float Damage;
    public float Duration;

}

[Serializable]
public class ArtifactStat
{
    public int ID;
    public int Price;

    public float Hp;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public float Crit;
    public float CritDamage;
    public float CooltimeReduce;
    public float CCReduce;

    public float Skill_Cooltime;
    public float Skill_Duration;

}

[Serializable]
public class ArtifactTextStat
{
    public int ID;

    public string Title;
    public string EngTitle;
    public string ContensFormat;
    public string EngContensFormat;
}

[Serializable]
public class StatuetStat
{
    public int ID;

    public float Hp;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public float Cooltime;
    public float Duration;

}

[Serializable]
public class SaveData
{
    public double Greed;

    public bool[] StageProgress;
    public bool[] LobbyObject;

    //일일 보상 대기시간
    //일일 던전 대기시간
    //쿠폰 사용 여부
}