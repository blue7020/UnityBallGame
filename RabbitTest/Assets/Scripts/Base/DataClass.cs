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
}

[Serializable]
public class SaveData
{
    public int Language;
    public int HighScore;
    public bool Mute;
    public string ID;
}