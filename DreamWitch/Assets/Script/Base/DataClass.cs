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

    public bool[] StageClear;
    public bool[] StageShowEvent;
    public bool[] StageShow;
}