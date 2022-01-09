using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataController : InformationLoader
{
    public static SaveDataController Instance;
    public SaveData mUser;

    public bool DeadAds = false;

    public int mLanguage;
    public bool mMute, mFolderbleSetting;
    public int AdsCount=10;
    public int mCharacterID;

    public bool DataDelete;

    public float mFolderbleTilePos;
    public float mNormalTilePos;
    public float mFolderbleItemPos;
    public float mNormalItemPos;
    public float mMovingTileFolderblePos;
    public float mMovingTileNormalPos;
    public float mIngameUIPos;

    public CharacterText[] mCharacterInfoArr;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadJson(out mCharacterInfoArr, Paths.CHARACTER_TEXT);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadGame()
    {
        if (true)
        {
            //모바일이면 PlayerPrefs를 사용해 저장하면된다. //유니티 게임 내장 저장방식
            string data = PlayerPrefs.GetString("SaveData");//PlayerPrefs는 윈도우로 치면 레지스트리다.
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
            FixSaveData();
        }
    }

    protected void FixSaveData()//세이브 데이터에 들어간 모든 어레이에 대해서 길이 검증
    {
        //패치 시 아이템 추가 등으로 어레이의 길이가 변했을 시 검증함.
        //세이브 데이터를 로드하는 시점에서 세이브 데이터를 갱신(검증) 후 로드해줘야한다.

        //if (mUser.StageClear == null)//StageClear
        //{
        //    mUser.StageClear = new bool[Constants.STAGE_COUNT];
        //}
        //else if (mUser.StageClear.Length != Constants.STAGE_COUNT) // != 혹은 < 를 사용해 둘 중에 짧은 배열에 긴 배열을 덮어씌운다.
        //{
        //    bool[] temp = new bool[Constants.STAGE_COUNT];
        //    int count = Mathf.Min(Constants.STAGE_COUNT, mUser.StageClear.Length); //값들 중 더 작은 것을 검출
        //    for (int i = 0; i < count; i++)//플레이어 아이템 배열의 길이를 넣어준다 == 값이 변했으니까
        //    {
        //        temp[i] = mUser.StageClear[i];//최신 배열에 불러온 데이터를 덮어씌운다. (최신 배열의 값이 늘든 줄든 정상적으로 작동한다)
        //    }
        //    mUser.StageClear = temp;
        //}
        if (mUser.CharacterOpen == null)
        {
            mUser.CharacterOpen = new bool[mCharacterInfoArr.Length];
        }
        else if (mUser.CharacterOpen.Length != mCharacterInfoArr.Length)
        {
            bool[] temp = new bool[mCharacterInfoArr.Length];
            int count = Mathf.Min(mCharacterInfoArr.Length, mUser.CharacterOpen.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.CharacterOpen[i];
            }
            mUser.CharacterOpen = temp;
        }
    }


    protected void CreateNewSaveData()
    {
        mUser.NoAds = false;
        mUser.HighScore = 0;
        mUser.Mute = false;
        mUser.CharacterID = 0;
        mUser.Gold = 0;
        mUser.RevivalCount = 1;
        mUser.CharacterOpen = new bool[mCharacterInfoArr.Length];
        mUser.CharacterOpen[0] = true;
        mCharacterInfoArr[0].isOpen = true;
        mUser.isFolderble = false;
        mUser.ID = "btsui" + UnityEngine.Random.Range(0, 999999);
        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            mUser.Language = 0;
        }
        else if (Application.systemLanguage == SystemLanguage.English)
        {
            mUser.Language = 1;
        }
    }


    public void Save()
    {
        mUser.Language= mLanguage;
        mUser.Mute = mMute;
        mUser.CharacterID = mCharacterID;
        mUser.isFolderble = mFolderbleSetting;
        BinaryFormatter formatter = new BinaryFormatter();//Binary는 메모리를 검색하는 것 = 뜰채
        MemoryStream stream = new MemoryStream();//stream은 메모리를 통째로 담은 것 = 양동이

        formatter.Serialize(stream, mUser);//mUser를 stream에다가 넣은 것

        string data = Convert.ToBase64String(stream.GetBuffer()); //64비트짜리 데이터 파일로 변환(정확하진 않음)
                                                                  //ToBase64String은 일반적으로는 알 수 없는 문자열로 바꿔주는 것이며, GetBuffer는 담긴 덩어리를 통째로 빼는 것

        //유니티 게임 내장 저장방식
        PlayerPrefs.SetString("SaveData", data); //SetString에 들어가는 것은 하나도 빠짐 없이 문자열이 일치해야한다.
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        DataDelete = true;

    }

    private void OnApplicationQuit()
    {
        if (DataDelete==false)
        {
            Save();
        }
    }
}
