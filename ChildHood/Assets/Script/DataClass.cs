﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float Gold;

    public int ID;

    public float Hp;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public bool IsPercent;
    public int Crit;
    public int CritDamage;
    public int CooltimeReduce;
    public int CCReduce;

    public int Damage;

    public float Skill1_Cooltime;
    public float Skill1_Duration;

    public float Skill2_Cooltime;
    public float Skill2_Duration;
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

public class ItemTextStat
{
    public int ID;

    public string Title;
    public string ContensFormat;
}

public class ArtifactStat
{
    public int ID;
    public int Price;

    public float Hp;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;

    public bool IsPercent;
    public int Crit;
    public int CritDamage;
    public int CooltimeReduce;
    public int CCReduce;

    public float Skill_Cooltime;
    public float Skill_Duration;

}

public class ArtifactTextStat
{
    public int ID;

    public string Title;
    public string ContensFormat;

    public string Flavortext;
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