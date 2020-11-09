using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    public float UpgradeCrit,UpgradeCCReduce;


    public int Language; //0 = 한국어 / 1 = 영어

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SaveDataController.Instance.LoadGame();
            if (Application.systemLanguage == SystemLanguage.Korean)
            {
                Debug.Log("Kor");
                Language = 0;
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
        Restart();
        PlayerID = 0;
        PlayerSkillID = 0;
        PlayerWeaponID = 0;

        SaveDataController.Instance.mUser.NPCOpen[0] = true; SaveDataController.Instance.mUser.NPCOpen[4] = true;
        SaveDataController.Instance.mUser.StageOpen[0] = true;
        DateTime time = SaveDataController.Instance.mUser.DailyTime;
        DateTime timecheck = DateTime.Now;
        int yesterday = 0;
        int dummyYear = 1000;
        if (time.Day-1>0)
        {
            yesterday = time.Day - 1;
            timecheck = new DateTime(time.Year, time.Month, yesterday);
        }
        else
        {
            dummyYear = time.Year - 1;
            yesterday = 1999;
            timecheck = new DateTime(dummyYear, time.Month, yesterday);
        }
        Debug.Log(time+" / "+ timecheck);
        if (SaveDataController.Instance.mUser.DailyTime >= timecheck.AddDays(1))
        {
            SaveDataController.Instance.mUser.DailyTime = DateTime.Now;
            Debug.Log("24시간 갱신");
        }
    }

    public void Restart()
    {
        NowStageRoom = new Room[6];
        Ingame = false;
        NowStage = 0;
        SaveDataController.Instance.Save();
    }

    public void GetSyrup(int amount)
    {
        if (SaveDataController.Instance.mUser.Syrup + amount >= Constants.MAX_SYRUP)
        {
            SaveDataController.Instance.mUser.Syrup = Constants.MAX_SYRUP;
        }
        else
        {
            SaveDataController.Instance.mUser.Syrup += amount;
        }
    }

    public void ShowAds(eAdsReward reward)
    {
        GoogleAdmobHandler.Instance.SetAdRewardCallBack(reward);
        GoogleAdmobHandler.Instance.PlayAD();
    }

    public void NoneReward()
    {

    }
    public void DailySyrup()
    {
        DateTime time = SaveDataController.Instance.mUser.LastWatchingDailyAdsTime;
        DateTime timecheck = DateTime.Now;
        int yesterday = 0;
        int dummyYear = 1000;
        if (time.Day - 1 > 0)
        {
            yesterday = time.Day - 1;
            timecheck = new DateTime(time.Year, time.Month, yesterday);
        }
        else
        {
            dummyYear = time.Year - 1;
            yesterday = time.Day - 1;
            timecheck = new DateTime(dummyYear, time.Month, yesterday);
        }
        Debug.Log(time + " / " + timecheck);
        if (SaveDataController.Instance.mUser.LastWatchingDailyAdsTime >= timecheck.AddDays(1))
        {
            SaveDataController.Instance.mUser.TodayWatchFirstAD = false;
        }
        if (SaveDataController.Instance.mUser.TodayWatchFirstAD == false)
        {
            SaveDataController.Instance.mUser.LastWatchingDailyAdsTime = DateTime.Now;
            SaveDataController.Instance.mUser.TodayWatchFirstAD = true;
            GetSyrup(500);
            MainLobbyUIController.Instance.ShowSyrupText();
        }
    }

    public void Syrup()
    {
        GetSyrup(500);
        MainLobbyUIController.Instance.ShowSyrupText();
    }

    public void Double()
    {
        SoundController.Instance.SESoundUI(6);
        GameController.Instance.SyrupInStage *= 2;
        GameClearUI.Instance.PlusRewardMaterial += 1;
        if (Language == 0)
        {//한국어
            UIController.Instance.mSyrupText.text = "획득한 시럽: +<color=#FFFF00>" + GameController.Instance.SyrupInStage + "</color>";
            UIController.Instance.mItemText.text = "획득한 재료 x2";
        }
        else if (Language == 1)
        {//영어
            UIController.Instance.mSyrupText.text = "Syrup: +<color=#FFFF00>" + GameController.Instance.SyrupInStage;
            UIController.Instance.mItemText.text = "Material x2";
        }
    }

}
