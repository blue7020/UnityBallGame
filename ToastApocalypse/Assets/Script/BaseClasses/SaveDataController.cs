using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataController : InformationLoader
{
    public static SaveDataController Instance;

    public SaveData mUser;

    public bool DataDelete;

    public ArtText[] mArtInfoArr;
    public PlayerStat[] mPlayerInfoArr;
    public SkillStat[] mSkillInfoArr;
    public WeaponStat[] mWeaponInfoArr;
    public ItemStat[] mItemInfoArr;
    public StatueStat[] mStatueInfoArr;
    public CodeStat[] mCodeInfoArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadJson(out mWeaponInfoArr, Path.WEAPON_STAT);
            LoadJson(out mPlayerInfoArr, Path.PLAYER_STAT);
            LoadJson(out mSkillInfoArr, Path.SKILL_STAT);
            LoadJson(out mArtInfoArr, Path.ART_STAT);
            LoadJson(out mItemInfoArr, Path.ITEM_STAT);
            LoadJson(out mStatueInfoArr, Path.STATUE_STAT);
            LoadJson(out mCodeInfoArr, Path.CODE_STAT);
            DataDelete = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGame()
    {
        //string location = Application.streamingAssetsPath + "/SaveData";//여기다가는 마음대로 확장자를 만들어서 붙여도 된다.
        if (true) //(File.Exists(location))
        {
            //파일로 저장하고 싶다면
            //StreamReader Reader = new StreamReader(location); //해당하는 경로로 읽어들이기

            //모바일이면 PlayerPrefs를 사용해 저장하면된다. //유니티 게임 내장 저장방식
            string data = PlayerPrefs.GetString("SaveData");//PlayerPrefs는 윈도우로 치면 레지스트리다.
            //string data = Reader.ReadToEnd(); 
            if (string.IsNullOrEmpty(data))
            {
                CreateNewSaveData();
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));//데이터를 다시 불러오기
                mUser = (SaveData)formatter.Deserialize(stream); //retrun값이 object이기 때문에 세이브 데이터로 강제 형변환
            }
            //BinaryFormatter formatter = new BinaryFormatter();
            //MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));//데이터를 다시 불러오기
            //mUser = (SaveData)formatter.Deserialize(stream); //retrun값이 object이기 때문에 세이브 데이터로 강제 형변환
            FixSaveData();
            //Reader.Close();//반드시 Reader와 Write를 사용했을 시 Close 를 해줘야 한다.
        }
        //else
        //{
        //    CreateNewSaveData();
        //}
    }

    protected void FixSaveData()//세이브 데이터에 들어간 모든 어레이에 대해서 길이 검증
    {
        //패치 시 아이템 추가 등으로 어레이의 길이가 변했을 시 검증함.
        //세이브 데이터를 로드하는 시점에서 세이브 데이터를 갱신(검증) 후 로드해줘야한다.
        if (mUser.HasMaterial == null)//Material
        {
            mUser.HasMaterial = new int[Constants.MATERIAL_COUNT];
        }
        else if (mUser.HasMaterial.Length != Constants.MATERIAL_COUNT) // != 혹은 < 를 사용해 둘 중에 짧은 배열에 긴 배열을 덮어씌운다.
        {
            int[] temp = new int[Constants.MATERIAL_COUNT];
            int count = Mathf.Min(Constants.MATERIAL_COUNT, mUser.HasMaterial.Length); //값들 중 더 작은 것을 검출
            for (int i = 0; i < count; i++)//플레이어 아이템 배열의 길이를 넣어준다 == 값이 변했으니까
            {
                temp[i] = mUser.HasMaterial[i];//최신 배열에 불러온 데이터를 덮어씌운다. (최신 배열의 값이 늘든 줄든 정상적으로 작동한다)
            }
            mUser.HasMaterial = temp;
        }

        if (mUser.StageOpen == null)//Stage
        {
            mUser.StageOpen = new bool[Constants.STAGE_COUNT];
        }
        else if (mUser.StageOpen.Length != Constants.STAGE_COUNT)
        {
            bool[] temp = new bool[Constants.STAGE_COUNT];
            int count = Mathf.Min(Constants.STAGE_COUNT, mUser.StageOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.StageOpen[i];
            }
            mUser.StageOpen = temp;
        }

        if (mUser.NPCOpen == null)//NPC
        {
            mUser.NPCOpen = new bool[Constants.NPC_COUNT];
        }
        else if (mUser.NPCOpen.Length != Constants.NPC_COUNT)
        {
            bool[] temp = new bool[Constants.NPC_COUNT];
            int count = Mathf.Min(Constants.NPC_COUNT, mUser.NPCOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.NPCOpen[i];
            }
            mUser.NPCOpen = temp;
        }

        if (mUser.WeaponHas == null)//Weapon
        {
            mUser.WeaponHas = new bool[Constants.WEAPON_COUNT];
        }
        else if (mUser.WeaponHas.Length != Constants.WEAPON_COUNT)
        {
            bool[] temp = new bool[Constants.WEAPON_COUNT];
            int count = Mathf.Min(Constants.WEAPON_COUNT, mUser.WeaponHas.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.WeaponHas[i];
            }
            mUser.WeaponHas = temp;
        }
        if (mUser.WeaponOpen == null)
        {
            mUser.WeaponOpen = new bool[Constants.WEAPON_COUNT];
        }
        else if (mUser.WeaponOpen.Length != Constants.WEAPON_COUNT)
        {
            bool[] temp = new bool[Constants.WEAPON_COUNT];
            int count = Mathf.Min(Constants.WEAPON_COUNT, mUser.WeaponOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.WeaponOpen[i];
            }
            mUser.WeaponOpen = temp;
        }

        if (mUser.SkillHas == null)//Skill
        {
            mUser.SkillHas = new bool[Constants.SKILL_COUNT];
        }
        else if (mUser.SkillHas.Length != Constants.SKILL_COUNT)
        {
            bool[] temp = new bool[Constants.SKILL_COUNT];
            int count = Mathf.Min(Constants.SKILL_COUNT, mUser.SkillHas.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.SkillHas[i];
            }
            mUser.SkillHas = temp;
        }
        if (mUser.SkillOpen == null)
        {
            mUser.SkillOpen = new bool[Constants.SKILL_COUNT];
        }
        else if (mUser.SkillOpen.Length != Constants.SKILL_COUNT)
        {
            bool[] temp = new bool[Constants.SKILL_COUNT];
            int count = Mathf.Min(Constants.SKILL_COUNT, mUser.SkillOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.SkillOpen[i];
            }
            mUser.SkillOpen = temp;
        }

        if (mUser.StatueHas == null)//Statue
        {
            mUser.StatueHas = new bool[Constants.STATUE_COUNT];
        }
        else if (mUser.StatueHas.Length != Constants.STATUE_COUNT)
        {
            bool[] temp = new bool[Constants.STATUE_COUNT];
            int count = Mathf.Min(Constants.STATUE_COUNT, mUser.StatueHas.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.StatueHas[i];
            }
            mUser.StatueHas = temp;
        }
        if (mUser.StatueOpen == null)
        {
            mUser.StatueOpen = new bool[Constants.STATUE_COUNT];
        }
        else if (mUser.StatueOpen.Length != Constants.STATUE_COUNT)
        {
            bool[] temp = new bool[Constants.STATUE_COUNT];
            int count = Mathf.Min(Constants.STATUE_COUNT, mUser.StatueOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.StatueOpen[i];
            }
            mUser.StatueOpen = temp;
        }

        if (mUser.ItemHas == null)//Item
        {
            mUser.ItemHas = new bool[Constants.ITEM_COUNT];
        }
        else if (mUser.ItemHas.Length != Constants.ITEM_COUNT)
        {
            bool[] temp = new bool[Constants.ITEM_COUNT];
            int count = Mathf.Min(Constants.ITEM_COUNT, mUser.ItemHas.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.ItemHas[i];
            }
            mUser.ItemHas = temp;
        }
        if (mUser.ItemOpen == null)
        {
            mUser.ItemOpen = new bool[Constants.ITEM_COUNT];
        }
        else if (mUser.ItemOpen.Length != Constants.ITEM_COUNT)
        {
            bool[] temp = new bool[Constants.ITEM_COUNT];
            int count = Mathf.Min(Constants.ITEM_COUNT, mUser.ItemOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.ItemOpen[i];
            }
            mUser.ItemOpen = temp;
        }

        if (mUser.CharacterHas == null)//Character
        {
            mUser.CharacterHas = new bool[Constants.CHARACTER_COUNT];
        }
        else if (mUser.CharacterHas.Length != Constants.CHARACTER_COUNT)
        {
            bool[] temp = new bool[Constants.CHARACTER_COUNT];
            int count = Mathf.Min(Constants.CHARACTER_COUNT, mUser.CharacterHas.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CharacterHas[i];
            }
            mUser.CharacterHas = temp;
        }

        if (mUser.CharacterOpen == null)
        {
            mUser.CharacterOpen = new bool[Constants.CHARACTER_COUNT];
        }
        else if (mUser.CharacterOpen.Length != Constants.CHARACTER_COUNT)
        {
            bool[] temp = new bool[Constants.CHARACTER_COUNT];
            int count = Mathf.Min(Constants.CHARACTER_COUNT, mUser.CharacterOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CharacterOpen[i];
            }
            mUser.CharacterOpen = temp;
        }

        if (mUser.CharacterUpgrade == null)
        {
            mUser.CharacterUpgrade = new int[Constants.CHARACTER_COUNT];
        }
        else if (mUser.CharacterUpgrade.Length != Constants.CHARACTER_COUNT)
        {
            int[] temp = new int[Constants.CHARACTER_COUNT];
            int count = Mathf.Min(Constants.CHARACTER_COUNT, mUser.CharacterUpgrade.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CharacterUpgrade[i];
            }
            mUser.CharacterUpgrade = temp;
        }

        if (mUser.CodeUse == null)
        {
            mUser.CodeUse = new bool[Constants.Code_Count];
        }
        else if (mUser.CodeUse.Length != Constants.Code_Count)
        {
            bool[] temp = new bool[Constants.Code_Count];
            int count = Mathf.Min(Constants.Code_Count, mUser.CodeUse.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CodeUse[i];
            }
            mUser.CodeUse = temp;
        }

        if (mUser.ArtifactFound == null)
        {
            mUser.ArtifactFound = new bool[Constants.ARTIFACT_COUNT];
        }
        else if (mUser.ArtifactFound.Length != Constants.ARTIFACT_COUNT)
        {
            bool[] temp = new bool[Constants.ARTIFACT_COUNT];
            int count = Mathf.Min(Constants.ARTIFACT_COUNT, mUser.ArtifactFound.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.ArtifactFound[i];
            }
            mUser.ArtifactFound = temp;
        }

        SoundController.Instance.BGMVolume = mUser.BGMVolume;
        SoundController.Instance.SEVolume = mUser.SEVolume;
    }


    protected void CreateNewSaveData()
    {
        if (mUser.FirstSetting!=true)
        {
            mUser.Syrup = 0;
            mUser.HasMaterial = new int[Constants.MATERIAL_COUNT];
            mUser.TutorialEnd = false;
            mUser.StagePartsget = new bool[6];
            mUser.GameClear = false;
            mUser.FirstGameClearEvent = false;
            mUser.CodeUse = new bool[Constants.Code_Count];

            mUser.DonateCount = 0;
            mUser.NoAds = false;
            mUser.DailyTime = DateTime.Now;//24시간 타이머
            mUser.TodayWatchFirstNotice = false;
            mUser.LastWatchingDailyAdsTime = DateTime.Now.AddDays(-1);
            mUser.TodayWatchFirstAD = false;

            mUser.ArtifactFound = new bool[Constants.ARTIFACT_COUNT];

            mUser.ArtOpen = new bool[Constants.ART_COUNT];
            for (int i = 0; i < mArtInfoArr.Length; i++)
            {
                if (mArtInfoArr[i].Open == true)
                {
                    mUser.ArtOpen[i] = true;
                }
            }

            mUser.CharacterOpen = new bool[Constants.CHARACTER_COUNT];
            mUser.CharacterHas = new bool[Constants.CHARACTER_COUNT];
            mUser.CharacterUpgrade = new int[Constants.CHARACTER_COUNT];
            for (int i=0; i< Constants.CHARACTER_COUNT;i++)
            {
                mUser.CharacterHas[i] = mPlayerInfoArr[i].PlayerHas;
                mUser.CharacterOpen[i] = mPlayerInfoArr[i].Open;
                mUser.CharacterUpgrade[i] = 0;
            }

            mUser.WeaponOpen = new bool[Constants.WEAPON_COUNT];
            mUser.WeaponHas = new bool[Constants.WEAPON_COUNT];
            for (int i = 0; i < Constants.WEAPON_COUNT; i++)
            {
                mUser.WeaponHas[i] = mWeaponInfoArr[i].PlayerHas;
                mUser.WeaponOpen[i] = mWeaponInfoArr[i].Open;
            }

            mUser.SkillOpen = new bool[Constants.SKILL_COUNT];
            mUser.SkillHas = new bool[Constants.SKILL_COUNT];
            for (int i = 0; i < Constants.SKILL_COUNT; i++)
            {
                mUser.SkillHas[i] = mSkillInfoArr[i].PlayerHas;
                mUser.SkillOpen[i] = mSkillInfoArr[i].Open;
            }

            mUser.StatueOpen = new bool[Constants.STATUE_COUNT];
            mUser.StatueHas = new bool[Constants.STATUE_COUNT];
            for (int i = 0; i < Constants.STATUE_COUNT; i++)
            {
                mUser.StatueHas[i] = mStatueInfoArr[i].PlayerHas;
                mUser.StatueOpen[i] = mStatueInfoArr[i].Open;
            }

            mUser.ItemOpen = new bool[Constants.ITEM_COUNT];
            mUser.ItemHas = new bool[Constants.ITEM_COUNT];
            for (int i = 0; i < Constants.ITEM_COUNT; i++)
            {
                mUser.ItemHas[i] = mItemInfoArr[i].PlayerHas;
                mUser.ItemOpen[i] = mItemInfoArr[i].Open;
            }

            mUser.StageOpen = new bool[Constants.STAGE_COUNT];
            mUser.StageOpen[0] = true;//1스테이지 오픈

            mUser.NPCOpen = new bool[Constants.NPC_COUNT];
            mUser.NPCOpen[0] = true; //사서 npc 오픈
            mUser.NPCOpen[4] = true; //유료상인 npc 오픈
            mUser.BGMVolume = Constants.BGM_VOL;
            mUser.SEVolume = Constants.SE_VOL;
            mUser.FirstSetting = true;

            mUser.DeveloperID = false;
        }
    }


    public void Save()
    {
        //string location = Application.streamingAssetsPath + "/SaveData";
        BinaryFormatter formatter = new BinaryFormatter();//Binary는 메모리를 검색하는 것 = 뜰채
        MemoryStream stream = new MemoryStream();//stream은 메모리를 통째로 담은 것 = 양동이
        //파일로 저장하고 싶으면
        //StreamWriter writer = new StreamWriter(location);

        formatter.Serialize(stream, mUser);//mUser를 stream에다가 넣은 것

        string data = Convert.ToBase64String(stream.GetBuffer()); //64비트짜리 데이터 파일로 변환(정확하진 않음)
        //ToBase64String은 일반적으로는 알 수 없는 문자열로 바꿔주는 것이며, GetBuffer는 담긴 덩어리를 통째로 빼는 것
        
        //유니티 게임 내장 저장방식
        PlayerPrefs.SetString("SaveData", data); //SetString에 들어가는 것은 하나도 빠짐 없이 문자열이 일치해야한다.


        //writer.Write(data);
        //writer.Close();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        DataDelete = true;

    }

    private void OnApplicationQuit()
    {
        //게임이 종료될 때 적용
        if (DataDelete==false)
        {
            Save();
        }
    }
}
