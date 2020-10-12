using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    //전체에서 다 쓰는 상수를 이렇게 보관해두면 편하다
    //const 변수는 전부 대문자로 띄어쓰기 대신 언더바
    public const int STAGE_COUNT = 6;//게임에 아이템을 추가하면 값을 바꿔주기만 하면 됨

    public const int CHARACTER_COUNT = 3;
    public const int SKILL_COUNT = 13;
    public const int WEAPON_COUNT = 25;
    public const int ITEM_COUNT = 10;
    public const int ARTIFACT_COUNT = 20;
    public const int NPC_COUNT = 9;
    public const int ENEMY_COUNT = 45;
    public const int MATERIAL_COUNT = 16;

    public const float BGM_VOL = 0.3f;
    public const float SE_VOL = 0.3f;
}
