using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates
{
    public delegate void VoidCallback();
    public delegate void IntInVoidCallback(int value, int value2);
}

public class PlayerStat
{
    public int ID;
    public int CurrentLevel;
    public int MaxLevel;
    public double CostBase;
    public double CostWight;
    public double CostCurrent;
}
