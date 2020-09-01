using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHash : MonoBehaviour
{
    //LobbyObj
    public static readonly int Furniture = Animator.StringToHash("NPCon");

    //GetParts
    public static readonly int Parts = Animator.StringToHash("IsGet");

    public static readonly int Attack = Animator.StringToHash("IsAttack");
    //public static readonly int Dead = Animator.StringToHash("IsDie");
    public static readonly int Walk = Animator.StringToHash("IsWalk");
    public static readonly int Tumble = Animator.StringToHash("IsTumble");

    //SlotMachine
    public static readonly int Slot = Animator.StringToHash("IsRolling");
    //portal
    public static readonly int PortalSpawn = Animator.StringToHash("IsSpawn");
    //effect
    public static readonly int Fading = Animator.StringToHash("IsFade");
    public static readonly int Spining = Animator.StringToHash("IsSpin");
    public static readonly int Aurora = Animator.StringToHash("IsAurora");

    //All monster has Idle, IsWalk, IsAttack
    public static readonly int Enemy_Spawn = Animator.StringToHash("IsSpawn");
    public static readonly int Enemy_Attack = Animator.StringToHash("IsAttack");
    public static readonly int Enemy_Walk = Animator.StringToHash("IsWalk");
    public static readonly int Enemy_Death = Animator.StringToHash("IsDeath");
    public static readonly int Enemy_Phase = Animator.StringToHash("Phase");
}
