using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash
{
    //player
    public static readonly int Attack = Animator.StringToHash("IsAttack");
    //public static readonly int Dead = Animator.StringToHash("IsDie");
    public static readonly int Walk = Animator.StringToHash("IsWalk");


    //Object
    //HealZone
    public static readonly int Empty = Animator.StringToHash("IsEmpty");
    //chest
    public static readonly int Open = Animator.StringToHash("IsChestOpen");
    //item
    public static readonly int Show = Animator.StringToHash("IsShow");

    //enemy
    public static readonly int Jumping = Animator.StringToHash("IsJump");
}
