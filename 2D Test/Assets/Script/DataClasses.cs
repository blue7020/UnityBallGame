using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//Serializable을 해야 SerializeField가 먹힌다.
public class PlayerStat
{
    public int Id;
    public float Hp;
    public float Atk;
    public float AtkSpd;
    public float Speed;
    public int Gold;

    public bool IsPercent;
    public float Crit;

    
    public Skills[] Skill;
    //현재 쿨타임은 다른 곳에서 값을 저장하기 때문에 Current값을 데이터 테이블에 넣지 않는다.
    public float Cooltime;
    public float Duration;
}

[Serializable]
public class Monster
{
    public int Id;
    public float Hp;
    public float Atk;
    public float AtkSpd;
    public float Speed;
    public Pattern[] PatternsArr;
    public int Pattern;

}
//[Serializable]
//public class PlayerStatText
//{
//    public int ID;
//    public string Title;
//    public string ContentsFormat;
//}


//[Serializable]
//public class SaveData
//{
//    public double Gold;

//    public int Stage;
//    public double Progress;
//    public int LastGemID;

//    public int[] PlayerItemLevelArr;
//    public float[] SkillCooltimeArr;
//}