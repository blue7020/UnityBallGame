using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash : MonoBehaviour
{
    //Player
    public static readonly int Walk = Animator.StringToHash("IsWalk");
    public static readonly int Jump = Animator.StringToHash("IsJump");
    public static readonly int Climb = Animator.StringToHash("IsClimb");
    public static readonly int Grab = Animator.StringToHash("IsGrab");


    //CollectionObj
    public static readonly int Get = Animator.StringToHash("IsGet");

    //TriggerObj,JumperObj,CollectionObj
    public static readonly int On = Animator.StringToHash("IsTrigger");

    //FallingTile
    public static readonly int Falling = Animator.StringToHash("IsFalling");

    //CheckPoint
    public static readonly int CheckPoint = Animator.StringToHash("IsActive");

    //Enemy
    public static readonly int Enemy_Attack = Animator.StringToHash("IsAttack");
    public static readonly int Enemy_Death = Animator.StringToHash("IsDeath");
    public static readonly int Enemy_Spawn = Animator.StringToHash("IsSpawn");
    public static readonly int Enemy_Damage_Boss = Animator.StringToHash("IsDamage");

    //StageSelect
    public static readonly int MAP_SHOW = Animator.StringToHash("IsClear");

    //UI
    public static readonly int Close = Animator.StringToHash("IsClose");
}
