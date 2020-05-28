using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash : MonoBehaviour
{
    public static readonly int Attack = Animator.StringToHash("IsAttack");
    //public static readonly int Dead = Animator.StringToHash("IsDie");
    public static readonly int Walk = Animator.StringToHash("IsWalk");


    //mimic
    public static readonly int Move = Animator.StringToHash("IsMove");

}
