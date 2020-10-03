using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : InformationLoader
{
    public static GameSetting Instance;

    public int MinRoomLength, MaxRoomLength;

    public int PlayerID;
    public int PlayerSkillID;
    public int PlayerWeaponID;
    public bool Ingame;
    public int NowScene;

    public const int STAGELEVEL_COUNT = 5;
    private const int STATUE_COUNT = 8;
    private const int NPC_COUNT = 5;
    public int ReviveSyrup;

    //로비용
    public Sprite[] mParts;
    public Sprite[] mPlayerSpt;
    public Sprite[] mMaterialSpt;
    public Sprite[] mStatueSprites;
    public Artifacts[] mArtifacts;
    public PlayerStat[] mPlayerInfoArr;
    public SkillStat[] mSkillInfoArr;
    public WeaponStat[] mWeaponInfoArr;
    public Weapon[] mWeapons;
    public Room[] NowStageRoom;
    public int NowStage;

    public int PlayerSkillIndex;
    public int PlayerWeaponIndex;

    //저장해야할 것
    public int Language; //0 = 한국어 / 1 = 영어
    public int Syrup;
    public int[] HasMaterial;
    public bool[] StatueOpen;
    public bool TutorialEnd;
    public bool[] StageOpen;
    public bool[] StagePartsget;
    public bool[] NPCOpen;
    public int DonateCount;
    public bool TodayWatchFirstAD;
    //TODO 서버시간불러와서 TodayWatchFirstAD 초기화해야함
    public bool FirstSetting;
    //

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadJson(out mWeaponInfoArr, Path.WEAPON_STAT);
            LoadJson(out mPlayerInfoArr, Path.PLAYER_STAT);
            LoadJson(out mSkillInfoArr, Path.SKILL_STAT);
            //TODO 세이브불러오기
            NowScene = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (FirstSetting == false)
        {
            TodayWatchFirstAD = false;
            HasMaterial = new int[mMaterialSpt.Length];
            StageOpen = new bool[6];
            StageOpen[0] = true;//1스테이지 오픈
            StagePartsget = new bool[6];//튜토리얼 스테이지가 0, 5스테이지가 5/ 6스테이지는 파츠 6개가 모여야 들어갈 수 있음
            PlayerSkillID = 0;
            PlayerWeaponID = 0;
            StatueOpen = new bool[STATUE_COUNT];
            for (int i=0; i<4;i++)
            {
                StatueOpen[i] = true; //기본 석상 4개 오픈
            }
            NPCOpen = new bool[NPC_COUNT];
            for (int i = 0; i < NPC_COUNT; i++)
            {
                NPCOpen[i] = false; //기본 석상 4개 오픈
            }
            NPCOpen[0] = true; //사서 npc 오픈
            NPCOpen[4] = true; //유료상인 npc 오픈
            Syrup = 0;
            TutorialEnd = false;
            FirstSetting = true;
        }
        Restart();
        PlayerID = 0;

        //테스트용도
        mPlayerInfoArr[2].Open = true;//핑크도넛캐릭터 오픈
        mPlayerInfoArr[2].PlayerHas = true;//핑크도넛캐릭터 오픈
        mSkillInfoArr[2].PlayerHas = true;//돌진 스킬 오픈
        Syrup = 10000;
        for (int i=0; i<HasMaterial.Length;i++)
        {
            HasMaterial[i] = 10;
        }
        //
    }

    public void Restart()
    {
        NowStageRoom = new Room[6];
        Ingame = false;
        NowStage = 0;
        PlayerSkillIndex = 0;
        PlayerWeaponIndex = 0;
    }

    public void ShowAds()
    {
        if (TodayWatchFirstAD == false)
        {
            //광고 감상 후 300시럽 주기
            TodayWatchFirstAD = true;
        }
    }

}
