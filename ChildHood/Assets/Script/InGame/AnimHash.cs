using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash : MonoBehaviour
{
    public static readonly int Attack = Animator.StringToHash("IsAttack");
    //public static readonly int Dead = Animator.StringToHash("IsDie");
    public static readonly int Walk = Animator.StringToHash("IsWalk");


    //All monster has IsMove, IsAttack
    public static readonly int Enemy_Move = Animator.StringToHash("IsMove");
    public static readonly int Enemy_Attack = Animator.StringToHash("IsAttack");
}
