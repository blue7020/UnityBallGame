using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float Gold;

    public int ID;
    public int PurchaseID; //0은 시럽 구매, 1은 유료 구매
    public int Price;
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

    public bool PlayerHas;
    public bool Open;
}

[Serializable]
public class WeaponStat
{
    public int ID;
    public int Price;

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

    public bool PlayerHas;
    public bool Open;
    public bool ShopSell;

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
    public int OpenPrice;//로비 상점에서 개방 시 가격
    public string Name;
    public string EngName;
    public string ContensFormat;
    public string EngContensFormat;

    public float Heal;
    public float Atk;
    public float Def;
    public float AtkSpd;
    public float Spd;
    public float Crit;

    public float Duration;
    public bool PlayerHas;//false라면 인게임 상점엔 등장하지 않음
    public bool Open;//false라면 로비 및 인게임 상점엔 등장하지 않음
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

    public bool PlayerHas;
    public bool Open;
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

    public bool PlayerHas;
    public bool Open;
    public bool ShopSell;
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
public class DialogText
{
    public int ID;
    public string ContensFormat;
    public string EngContensFormat;
    public bool IsClose;
}

[Serializable]
public class ArtText
{
    public int ID;
    public int ArtCode;
    public string ContensFormat;
    public string EngContensFormat;
    public bool Open;
}

[Serializable]
public class SaveData
{
    public int Syrup;
    public int[] HasMaterial;
    public bool TutorialEnd;
    public bool[] StageOpen;
    public bool[] StagePartsget;
    public bool[] NPCOpen;

    public bool[] WeaponOpen;
    public bool[] StatueOpen;
    public bool[] SkillOpen;
    public bool[] ItemOpen;
    public bool[] CharacterOpen;
    public bool[] ArtOpen;

    public bool[] WeaponHas;
    public bool[] StatueHas;
    public bool[] SkillHas;
    public bool[] ItemHas;
    public bool[] CharacterHas;


    public int DonateCount;
    public bool TodayWatchFirstAD;
    public float CurrentServerTime;
    public bool FirstSetting;
    public float BGMVolume;
    public float SEVolume;
}