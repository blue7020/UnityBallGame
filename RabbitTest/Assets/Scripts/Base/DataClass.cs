using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GuideText
{
    public int ID;
    public string title_kor;
    public string title_eng;
    public string text_kor;
    public string text_eng;
}

[Serializable]
public class SaveData
{
    public int Language;
    public int HighScore;
    public bool Mute;
    public string ID;
    public bool NoAds;
    public int CharacterID;
    public int Gold;
    public int RevivalCount;
    public bool[] CharacterOpen;
}

[Serializable]
public class CharacterText
{
    public int ID;
    public int Price;
    public bool isOpen;
    public string name_kor;
    public string name_eng;
    public string text_kor;
    public string text_eng;
}