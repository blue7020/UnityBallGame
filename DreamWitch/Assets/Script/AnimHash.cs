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

    //TriggerObj
    public static readonly int On = Animator.StringToHash("IsTrigger");

    //FallingTile
    public static readonly int Falling = Animator.StringToHash("IsFalling");

    //CheckPoint
    public static readonly int CheckPoint = Animator.StringToHash("IsActive");

    //Enemy
    public static readonly int Enemy_Attack = Animator.StringToHash("IsAttack");
    public static readonly int Enemy_Death = Animator.StringToHash("IsDeath");
}
