using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : InformationLoader
{
    public static GameSetting Instance;

    public const int LEVEL_COUNT = 5;

    public int PlayerID;
    public int PlayerSkillID;
    public int PlayerWeaponID;
    public bool Ingame;
    public int NowScene;

    private const int CHARACTER_COUNT = 7;

    //로비용
    public Sprite[] mParts;
    public Sprite[] mPlayerSpt;
    public Sprite[] mMaterialSpt;
    public Sprite[] mStatueSprites;
    public Artifacts[] mArtifacts;
    public WeaponStat[] mInfoArr;
    public WeaponStat[] GetInfoArr()
    {
        return mInfoArr;
    }
    public Weapon[] mWeapons;
    //
    //저장해야할 것
    public int Language; //0 = 한국어 / 1 = 영어
    public int Syrup;

    public int[] HasMaterial;
    public bool[] PlayerHasSkill;
    public int PlayerSkillIndex;
    public bool[] PlayerHasWeapon;
    public int PlayerWeaponIndex;

    public bool[] StatueOpen;
    public bool TutorialEnd;

    public bool[] StageOpen;
    public int NowStage;
    public Room[] NowStageRoom;
    public const int Room_COUNT =7;//최소
    public bool[] StagePartsget;
    public bool[] CharacterOpen;
    public bool[] NPCOpen;

    public bool FirstSetting;
    //

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadJson(out mInfoArr, Path.WEAPON_STAT);
            //세이브불러오기
            NowScene = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (FirstSetting != true)
        {
            HasMaterial = new int[mMaterialSpt.Length];
            StageOpen = new bool[6];
            NPCOpen = new bool[4];//현재 4명
            StagePartsget = new bool[6];//튜토리얼 스테이지가 0, 5스테이지가 5/ 6스테이지는 파츠 6개가 모여야 들어갈 수 있음
            CharacterOpen = new bool[CHARACTER_COUNT];
            PlayerHasSkill = new bool[SkillController.Instance.mStatInfoArr.Length];
            PlayerHasWeapon = new bool[mWeapons.Length];
            StatueOpen = new bool[8];//석상을 추가할 때마다 수정
            for (int i=0; i<CharacterOpen.Length;i++)
            {
                CharacterOpen[i] = false;
            }
            CharacterOpen[0] = true;//기본캐릭터 오픈
            for (int i=0; i<4;i++)
            {
                StatueOpen[i] = true; //기본 석상 4개 오픈
                NPCOpen[i] = true; //기본 NPC 4명 오픈(원래는 0,5번만 오픈)
            }
            StageOpen[0] = true;//1스테이지 오픈
            PlayerHasSkill[0] = true;//기본 스킬 오픈
            PlayerSkillID = 0;
            PlayerHasWeapon[0] = true; //기본 근접 무기 오픈
            PlayerHasWeapon[1] = true; //기본 원거리 무기 오픈
            PlayerWeaponID = 0;
            Syrup = 0;
            TutorialEnd = false;
            FirstSetting = true;
        }
        Restart();

        Syrup = 10000;
        CharacterOpen[2] = true;//핑크도넛캐릭터 오픈
        PlayerHasSkill[2] = true;//돌진 스킬 오픈
        for (int i=0; i<HasMaterial.Length;i++)
        {
            HasMaterial[i] = 10;
        }
    }

    public void Restart()
    {
        NowStageRoom = new Room[6];
        Ingame = false;
        NowStage = 0;
    }

}
