using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates
{
    public delegate void VoidCallback();
    public delegate void TwoIntInVoidCallback(int value, int value2);
}

[Serializable]//Serializable을 해야 SerializeField가 먹힌다.
public class PlayerStat
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;

    public eCostType CostType;
    public double CostBase;
    public double CostWight;
    public double CostCurrent;

    public bool IsPercent;
    public double ValueBase;
    public double ValueWeight;
    public double ValueCurrent;

    //현재 쿨타임은 다른 곳에서 값을 저장하기 때문에 Current값을 데이터 테이블에 넣지 않는다.
    public float Cooltime;
    public float Duration;
}
