using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    //전체에서 다 쓰는 상수를 이렇게 보관해두면 편하다
    //const 변수는 전부 대문자로 띄어쓰기 대신 언더바
    public const int STAGE_COUNT = 7;//게임에 아이템을 추가하면 값을 바꿔주기만 하면 됨
    public const int EVENT_STAGE = 1;//1=할로윈
    public const int MAX_SYRUP = 999999;
    public const int MAX_MATERIAL = 99;


    public const int CHARACTER_COUNT = 14;
    public const int SKILL_COUNT = 14;
    public const int STATUE_COUNT = 9;
    public const int WEAPON_COUNT = 30;
    public const int ITEM_COUNT = 11;
    public const int NPC_COUNT = 10;

    public const int MATERIAL_COUNT = 18;
    public const int ARTIFACT_COUNT = 28;
    public const int ENEMY_COUNT = 51; //기본 몬스터 수 45+ 할로윈 몬스터 6(ID 46~51)
    public const int ART_COUNT = 9;

    public const int Code_Count = 10;
}
