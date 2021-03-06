﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    public static TitleController Instance;
    public bool isShowTitle, isShowNotice;

    public int mLanguageCount = 1; 
    public int mLanguage;
    public float mGameVer;

    public int NowStage;
    public int NowChapterCode;

    public int PlayCount;//목숨

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SaveDataController.Instance.LoadGame();
            mLanguage = SaveDataController.Instance.mUser.Language;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
