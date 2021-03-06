﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//Serializable을 해야 SerializeField가 먹힌다.
public class PlayerStat
{
    public double Gold;
    public double Soul;
    public double Crystal;

    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public eCostType CostType;
    public double CostBase;
    public double CostWight;
    public double CostCurrent;
    public double CostTenWeight;

    public bool IsPercent;
    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;

    //현재 쿨타임은 다른 곳에서 값을 저장하기 때문에 Current값을 데이터 테이블에 넣지 않는다.
    public float Cooltime;
    public float Duration;
}

[Serializable]
public class PlayerStatText
{
    public int ID;
    public string Title;
    public string ContentsFormat;
}


[Serializable]
public class CoworkerInfo
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public eCostType CostType;
    public double CostBase;
    public double CostWight;
    public double CostCurrent;
    public double CostTenWeight;

    public float PeriodBase;
    public float PeriodCurrent;
    public float PeriodUpgreadeAmount;
    public int PeriodLevelStep;

    public eCalculationType ValueCalcType;
    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;

}

[Serializable]
public class CoworkerTextInfo
{
    public int ID;
    public string Title;
    public string ContentsFormat;
}

[Serializable]
public class SaveData
{
    public double Gold;
    public double Soul;
    public double Crystal;

    public int Stage;
    public double Progress;
    public int LastGemID;

    public int[] PlayerItemLevelArr;
    public float[] SkillCooltimeArr;
    public float[] SkillMaxCooltimeArr;

    public int[] CoworkerLevelArr;
}