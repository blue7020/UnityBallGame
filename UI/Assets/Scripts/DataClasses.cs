using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ItemData
{
    public int ID;
    public eItemType ItemType;
    public int Level;
    public string Name;
    public string Contents;
    public int ATK;
    public int DEF;
    public float HP;
    public float MP;
    public ItemData GetClone()
    {
        return MemberwiseClone() as ItemData;
    }
}
[Serializable]
public class SkillData
{
    public int ID;
    public eSkillType SkillType;
    public int UnlockLevel;
    public float MP;
    public float Cooltime;
    public int Amount;
    public float Duration;
    public SkillData GetClone()
    {
        return MemberwiseClone() as SkillData;
    }
}