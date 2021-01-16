using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueText
{
    public int ID;
    public string text_kor;
    public string text_eng;
    public int FaceCode;
}

[Serializable]
public class StageInfo
{
    public int ID;
    public string title_kor;
    public string title_eng;
    public string info_kor;
    public string info_eng;
}

[Serializable]
public class SaveData
{
    public int LastPlayStage;
    public int BGMVolume;
    public int SEVolume;
    public int Language;

    public int CollectionAmount;
    public bool[] Stage_0_CollectionCheck;
    public bool[] Stage_1_CollectionCheck;
    public bool[] Stage_2_CollectionCheck;
    public bool[] Stage_3_CollectionCheck;
    public bool[] Stage_4_CollectionCheck;
    public bool[] Stage_5_CollectionCheck;

    public bool[] StageClear;
    public bool[] StageShowEvent;
    public bool[] StageShow;
}