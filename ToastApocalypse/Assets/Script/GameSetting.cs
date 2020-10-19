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
    public Weapon[] mWeaponArr;
    public Artifacts[] mArtifacts;
    public UsingItem[] mItemArr;
    public MapText[] mMapInfoArr;
    public Room[] NowStageRoom;
    public DialogText[] mDialogArr;
    public Sprite[] Illust;
    public int NowStage;


    public int Language; //0 = 한국어 / 1 = 영어

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SaveDataController.Instance.LoadGame();
            DontDestroyOnLoad(gameObject);
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
            LoadJson(out mDialogArr, Path.DIALOG_TEXT);
            LoadJson(out mMapInfoArr, Path.MAP_TEXT);
            NowScene = 0;
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
        PlayerSkillID = 0;
        PlayerWeaponID = 0;

        //테스트용도
        //mPlayerInfoArr[2].Open = true;//핑크도넛캐릭터 오픈
        //mPlayerInfoArr[2].PlayerHas = true;//핑크도넛캐릭터 오픈
        //mSkillInfoArr[2].PlayerHas = true;//돌진 스킬 오픈
    }

    public void Restart()
    {
        NowStageRoom = new Room[6];
        Ingame = false;
        NowStage = 0;
    }

    public void ShowAds()
    {
        if (SaveDataController.Instance.mUser.TodayWatchFirstAD == false)
        {
            //광고 감상 후 300시럽 주기
            SaveDataController.Instance.mUser.TodayWatchFirstAD = true;
        }
    }

}
