﻿using System;
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

    public float Crit;
    public float CritDamage;
    public float CooltimeReduce;
    public float CCReduce;

    public float Skill_Cooltime;
    public float Skill_Duration;
}

[Serializable]
public class WeaponStat
{
    public int ID;
    public string Name;
    public string EngName;
    public string ContensFormat;
    public string EngContensFormat;

    public int Type; //0 = 근접 1 =원거리 2= 중거리
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
    public string Name;
    public string EngName;
    public int Gold;
    public int Syrup;
    public float Hp;
    public float Atk;
    public float AtkSpd;
    public float Spd;
    public float Resistance;
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

    public float Heal;
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
    public string PlayableText;
    public string EngPlayableText;
}

[Serializable]
public class StatueStat
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
public class StatueText
{
    public int ID;
    public int Price;
    public string Name;
    public string EngName;
    public string ContensFormat;
    public string EngContensFormat;
}


[Serializable]
public class SkillStat
{
    public int ID;

    public float Damage;
    public float Heal;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public float Crit;

    public float Cooltime;
    public float Duration;
}

[Serializable]
public class SkillText
{
    public int ID;
    public int Price;

    public string Title;
    public string EngTitle;
    public string ContensFormat;
    public string EngContensFormat;
}

[Serializable]
public class MapText
{
    public int ID;

    public string Title;
    public string EngTitle;
    public string ContensFormat;
    public string EngContensFormat;
}

[Serializable]
public class MaterialStat
{
    public int ID;
    public string Title;
    public string EngTitle;
    public string ContensFormat;
    public string EngContensFormat;
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