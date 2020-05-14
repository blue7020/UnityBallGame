using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전역 변수용 클래스
public static class Paths
{
    //Resources를 만들 때는 확장자가 필요 없다. 확장자를 넣으면 로드가 잘 안된다.
    public const string GEM_PREFAB = "Gem";
    public const string PLAYER_ITEM_ICON = "PlayerItems";

    private const string JSON_FILE_LOCATION = "JsonFiles/";
    public const string PLAYER_ITEM_TABLE = JSON_FILE_LOCATION + "PlayerItem";
    public const string PLAYER_ITEM_TEXT_TABLE = JSON_FILE_LOCATION + "PlayerItemText";
}


