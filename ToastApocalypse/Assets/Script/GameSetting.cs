using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : InformationLoader
{
    public static GameSetting Instance;

    public int CrawlerCount,MinRoomLength, MaxRoomLength;

    public int PlayerID;
    public int PlayerSkillID;
    public int PlayerWeaponID;
    public bool Ingame;
    public int NowScene;

    public const int STAGELEVEL_COUNT = 5;
    public int ReviveSyrup;

    //로비용
    public Sprite[] mParts;
    public Sprite[] mPlayerSpt;
    public Sprite[] mMaterialSpt;
    public Sprite[] mStatueSprites;
    public ArtText[] mArtInfoArr;
    public Artifacts[] mArtifacts;
    public PlayerStat[] mPlayerInfoArr;
    public SkillStat[] mSkillInfoArr;
    public WeaponStat[] mWeaponInfoArr;
    public ItemStat[] mItemInfoArr;
    public StatueStat[] mStatueInfoArr;

    public Weapon[] mWeaponArr;
    public UsingItem[] mItemArr;
    public Room[] NowStageRoom;
    public int NowStage;

    public int PlayerSkillIndex;
    public int PlayerWeaponIndex;

    public int Language; //0 = 한국어 / 1 = 영어

    public int Syrup;
    public int[] HasMaterial;
    public bool TutorialEnd;
    public bool[] StageOpen;
    public bool[] StagePartsget;
    public bool[] NPCOpen;
    public int DonateCount;
    public bool TodayWatchFirstAD;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadJson(out mWeaponInfoArr, Path.WEAPON_STAT);
            LoadJson(out mPlayerInfoArr, Path.PLAYER_STAT);
            LoadJson(out mSkillInfoArr, Path.SKILL_STAT);
            LoadJson(out mArtInfoArr, Path.ART_STAT);
            LoadJson(out mItemInfoArr, Path.ITEM_STAT);
            LoadJson(out mStatueInfoArr, Path.STATUE_STAT);
            if (Application.systemLanguage == SystemLanguage.Korean)
            {
                Debug.Log("Kor");
                Language = 0; //0 = 한국어 / 1 = 영어
            }
            else
            {
                Debug.Log("None Kor" + (int)Application.systemLanguage);
                Language = 1;
            }
            NowScene = 0;
            GameSaver.Instance.GameLoad();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //if (TutorialEnd!=true)
        //{
        //    //오프닝 출력
        //}
        //else
        //{

        //}
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
        NPCOpen[1] = true;//스킬 상인 npc 오픈
        TutorialEnd = true;
        //
    }

    public void Restart()
    {
        NowStageRoom = new Room[6];
        Ingame = false;
        NowStage = 0;
        PlayerSkillID = 0;
        PlayerWeaponID = 0;
        PlayerSkillIndex = 0;
        PlayerWeaponIndex = 0;
    }

    public void ShowAds()
    {
        if (TodayWatchFirstAD == false)
        {
            //광고 감상 후 300시럽 주기
            TodayWatchFirstAD = true;
            GameSaver.Instance.GameSave();
        }
    }

}
