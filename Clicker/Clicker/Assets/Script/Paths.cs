using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전역 변수용 클래스
public static class Paths
{
    public const string GEM_PREFAB = "Gem";
    public const string PLAYER_ITEM_ICON = "PlayerItems";
}

public static class Constants
{
    //전체에서 다 쓰는 상수를 이렇게 보관해두면 편하다
    //const 변수는 전부 대문자로 띄어쓰기 대신 언더바
    public const int TOTAL_GEM_COUNT = 3;
}
