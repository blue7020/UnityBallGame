using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash : MonoBehaviour
{
    public static readonly int Attack = Animator.StringToHash("IsAttack");
    //public static readonly int Dead = Animator.StringToHash("IsDie");
    public static readonly int Walk = Animator.StringToHash("IsWalk");
    public static readonly int Tumble = Animator.StringToHash("IsTumble");


    //All monster has Idle, IsWalk, IsAttack
    public static readonly int Enemy_Spawn = Animator.StringToHash("IsSpawn");
    public static readonly int Enemy_Attack = Animator.StringToHash("IsAttack");
    public static readonly int Enemy_Walk = Animator.StringToHash("IsWalk");
    public static readonly int Enemy_Death = Animator.StringToHash("IsDeath");
}
