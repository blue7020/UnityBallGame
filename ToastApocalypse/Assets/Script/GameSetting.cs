﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;

    public const int LEVEL_COUNT = 5;

    public int PlayerID;
    public int PlayerSkillID;
    public bool Ingame;

    private const int CHARACTER_COUNT = 6;

    public Sprite[] mParts;
    public Sprite[] mPlayerSpt;

    //로비 상점용
    public Sprite[] mStatueSprites;
    public Artifacts[] mArtifacts;
    public Weapon[] mWeapons;
    //
    //저장해야할 것
    public int Language; //0 = 한국어 / 1 = 영어
    public int Syrup;

    public int PartsIndex;

    public bool[] PlayerHasSkill;
    public int PlayerSkillIndex;

    public bool[] StatueOpen;

    public bool[] StageOpen;
    public int NowStage;
    public Room[] NowStageRoom;
    public const int Room_COUNT =7;//최소
    public bool[] StagePartsget;
    public bool[] CharacterOpen;

    public bool FirstSetting = false;
    //

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (FirstSetting != true)
        {
            StageOpen = new bool[6];
            StagePartsget = new bool[6];//튜토리얼 스테이지가 0, 5스테이지가 5/ 6스테이지는 파츠 6개가 모여야 들어갈 수 있음
            CharacterOpen = new bool[CHARACTER_COUNT];
            PlayerHasSkill = new bool[SkillController.Instance.mStatInfoArr.Length];
            StatueOpen = new bool[8];//석상을 추가할 때마다 수정
            for (int i=0; i<4;i++)
            {
                StatueOpen[i] = true; //기본 석상 4개 오픈
            }
            StageOpen[0] = true;//1스테이지 오픈
            CharacterOpen[0] = true;//기본캐릭터 오픈
            PlayerHasSkill[0] = true;//기본 스킬 오픈
            PlayerSkillID = 1;
            Syrup = 0;
            PartsIndex = 0;
        }
        Restart();

        Syrup = 30000;
        CharacterOpen[1] = true;//햄에그캐릭터 오픈
        CharacterOpen[2] = true;//핑크도넛캐릭터 오픈
        CharacterOpen[3] = true;//허니브레드캐릭터 오픈
        CharacterOpen[4] = true;//스시닌자캐릭터 오픈

        StageOpen[1] = true;//2스테이지 오픈
        StageOpen[2] = true;//3스테이지 오픈
        StageOpen[3] = true;//4스테이지 오픈
    }

    public void Restart()
    {
        NowStageRoom = new Room[6];
        Ingame = false;
        NowStage = 0;
        PlayerID = 0;
    }

    public void GetParts(int Stage)
    {
        if (StagePartsget[Stage] == false && Instance.PartsIndex < 6)
        {
            Instance.StagePartsget[Stage] = true;
            Instance.PartsIndex++;
        }
    }

    public void ShowParts()
    {
        for (int i = 0; i < 6; i++)
        {
            if (StagePartsget[i] == true)
            {
                MainLobbyUIController.Instance.mPartsLock[i].sprite = mParts[i];
            }
        }
    }

}
