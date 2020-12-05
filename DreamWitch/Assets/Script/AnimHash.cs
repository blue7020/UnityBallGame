using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash : MonoBehaviour
{
    //Player
    public static readonly int Walk = Animator.StringToHash("IsWalk");
    public static readonly int Jump = Animator.StringToHash("IsJump");

    //TriggerObj
    public static readonly int On = Animator.StringToHash("IsTrigger");

    //Enemy
    public static readonly int Enemy_Attack = Animator.StringToHash("IsAttack");
    public static readonly int Enemy_Death = Animator.StringToHash("IsDeath");
}
