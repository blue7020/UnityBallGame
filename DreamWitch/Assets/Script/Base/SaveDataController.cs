using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataController : InformationLoader
{
    public static SaveDataController Instance;
    public SaveData mUser;

    public bool DataDelete;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGame()
    {
        //string location = Application.streamingAssetsPath + "/SaveData";//여기다가는 마음대로 확장자를 만들어서 붙여도 된다.
#if UNITY_EDITOR
        string location = "Assets/Resources/SaveData.savedata";//유니티 리소시즈 폴더에 저장할 때만.
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA
                string location = "Alone In a Dream_Data/Resources/SaveData.savedata";//게임 파일로 빌드했을때
#endif
        if (File.Exists(location))//(true)
        {
            StreamReader Reader = new StreamReader(location); //해당하는 경로로 읽어들이기

            string data = Reader.ReadToEnd(); 
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));//데이터를 다시 불러오기
            mUser = (SaveData)formatter.Deserialize(stream); //retrun값이 object이기 때문에 세이브 데이터로 강제 형변환
            FixSaveData();
            Debug.Log("불러오기 성공");
            Reader.Close();//반드시 Reader와 Write를 사용했을 시 Close 를 해줘야 한다.
        }
        else
        {
            Debug.Log("파일 생성");
            CreateNewSaveData();
        }
    }

    protected void FixSaveData()//세이브 데이터에 들어간 모든 어레이에 대해서 길이 검증
    {
        //패치 시 아이템 추가 등으로 어레이의 길이가 변했을 시 검증함.
        //세이브 데이터를 로드하는 시점에서 세이브 데이터를 갱신(검증) 후 로드해줘야한다.

        if (mUser.StageClear == null)//StageClear
        {
            mUser.StageClear = new bool[Constants.STAGE_COUNT];
        }
        else if (mUser.StageClear.Length != Constants.STAGE_COUNT) // != 혹은 < 를 사용해 둘 중에 짧은 배열에 긴 배열을 덮어씌운다.
        {
            bool[] temp = new bool[Constants.STAGE_COUNT];
            int count = Mathf.Min(Constants.STAGE_COUNT, mUser.StageClear.Length); //값들 중 더 작은 것을 검출
            for (int i = 0; i < count; i++)//플레이어 아이템 배열의 길이를 넣어준다 == 값이 변했으니까
            {
                temp[i] = mUser.StageClear[i];//최신 배열에 불러온 데이터를 덮어씌운다. (최신 배열의 값이 늘든 줄든 정상적으로 작동한다)
            }
            mUser.StageClear = temp;
        }

        if (mUser.StageShowEvent == null)//StageShowEvent
        {
            mUser.StageShowEvent = new bool[Constants.STAGE_COUNT];
        }
        else if (mUser.StageShowEvent.Length != Constants.STAGE_COUNT)
        {
            bool[] temp = new bool[Constants.STAGE_COUNT];
            int count = Mathf.Min(Constants.STAGE_COUNT, mUser.StageShowEvent.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.StageShowEvent[i];
            }
            mUser.StageShowEvent = temp;
        }

        if (mUser.StageShow == null)//StageShow
        {
            mUser.StageShow = new bool[Constants.STAGE_COUNT];
        }
        else if (mUser.StageShow.Length != Constants.STAGE_COUNT)
        {
            bool[] temp = new bool[Constants.STAGE_COUNT];
            int count = Mathf.Min(Constants.STAGE_COUNT, mUser.StageShow.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.StageShow[i];
            }
            mUser.StageShow = temp;
        }

        if (mUser.Stage_0_CollectionCheck == null)//0 collection check
        {
            mUser.Stage_0_CollectionCheck = new bool[Constants.STAGE_0_COLLECTION];
        }
        else if (mUser.Stage_0_CollectionCheck.Length != Constants.STAGE_0_COLLECTION)
        {
            bool[] temp = new bool[Constants.STAGE_0_COLLECTION];
            int count = Mathf.Min(Constants.STAGE_0_COLLECTION, mUser.Stage_0_CollectionCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.Stage_0_CollectionCheck[i];
            }
            mUser.Stage_0_CollectionCheck = temp;
        }

        if (mUser.Stage_1_CollectionCheck == null)//1 collection check
        {
            mUser.Stage_1_CollectionCheck = new bool[Constants.STAGE_1_COLLECTION];
        }
        else if (mUser.Stage_1_CollectionCheck.Length != Constants.STAGE_1_COLLECTION)
        {
            bool[] temp = new bool[Constants.STAGE_1_COLLECTION];
            int count = Mathf.Min(Constants.STAGE_1_COLLECTION, mUser.Stage_1_CollectionCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.Stage_1_CollectionCheck[i];
            }
            mUser.Stage_1_CollectionCheck = temp;
        }

        if (mUser.Stage_2_CollectionCheck == null)//2 collection check
        {
            mUser.Stage_2_CollectionCheck = new bool[Constants.STAGE_2_COLLECTION];
        }
        else if (mUser.Stage_2_CollectionCheck.Length != Constants.STAGE_2_COLLECTION)
        {
            bool[] temp = new bool[Constants.STAGE_2_COLLECTION];
            int count = Mathf.Min(Constants.STAGE_2_COLLECTION, mUser.Stage_2_CollectionCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.Stage_2_CollectionCheck[i];
            }
            mUser.Stage_2_CollectionCheck = temp;
        }

        if (mUser.Stage_3_CollectionCheck == null)//3 collection check
        {
            mUser.Stage_3_CollectionCheck = new bool[Constants.STAGE_3_COLLECTION];
        }
        else if (mUser.Stage_3_CollectionCheck.Length != Constants.STAGE_3_COLLECTION)
        {
            bool[] temp = new bool[Constants.STAGE_3_COLLECTION];
            int count = Mathf.Min(Constants.STAGE_3_COLLECTION, mUser.Stage_3_CollectionCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.Stage_3_CollectionCheck[i];
            }
            mUser.Stage_3_CollectionCheck = temp;
        }

        if (mUser.Stage_4_CollectionCheck == null)//4 collection check
        {
            mUser.Stage_4_CollectionCheck = new bool[Constants.STAGE_4_COLLECTION];
        }
        else if (mUser.Stage_4_CollectionCheck.Length != Constants.STAGE_4_COLLECTION)
        {
            bool[] temp = new bool[Constants.STAGE_4_COLLECTION];
            int count = Mathf.Min(Constants.STAGE_4_COLLECTION, mUser.Stage_4_CollectionCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.Stage_4_CollectionCheck[i];
            }
            mUser.Stage_4_CollectionCheck = temp;
        }

        if (mUser.Stage_5_CollectionCheck == null)//5 collection check
        {
            mUser.Stage_5_CollectionCheck = new bool[Constants.STAGE_5_COLLECTION];
        }
        else if (mUser.Stage_5_CollectionCheck.Length != Constants.STAGE_5_COLLECTION)
        {
            bool[] temp = new bool[Constants.STAGE_5_COLLECTION];
            int count = Mathf.Min(Constants.STAGE_5_COLLECTION, mUser.Stage_5_CollectionCheck.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.Stage_5_CollectionCheck[i];
            }
            mUser.Stage_5_CollectionCheck = temp;
        }

        SoundController.Instance.BGMVolume = mUser.BGMVolume;
        SoundController.Instance.SEVolume = mUser.SEVolume;
    }


    protected void CreateNewSaveData()
    {
        mUser.LastPlayStage = 0;
        mUser.BGMVolume = 5;
        mUser.SEVolume = 10;
        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            mUser.Language = 0;
        }
        else if (Application.systemLanguage == SystemLanguage.English)
        {
            mUser.Language = 1;
        }
        mUser.StageClear = new bool[Constants.STAGE_COUNT];
        mUser.StageShowEvent = new bool[Constants.STAGE_COUNT];
        mUser.StageShow = new bool[Constants.STAGE_COUNT];
        mUser.CollectionAmount = 0;
        mUser.Stage_0_CollectionCheck = new bool[Constants.STAGE_0_COLLECTION];
        mUser.Stage_1_CollectionCheck = new bool[Constants.STAGE_1_COLLECTION];
        mUser.Stage_2_CollectionCheck = new bool[Constants.STAGE_2_COLLECTION];
        mUser.Stage_3_CollectionCheck = new bool[Constants.STAGE_3_COLLECTION];
        mUser.Stage_4_CollectionCheck = new bool[Constants.STAGE_4_COLLECTION];
        mUser.Stage_5_CollectionCheck = new bool[Constants.STAGE_5_COLLECTION];
#if UNITY_EDITOR
        File.Create("Assets/Resources/SaveData.savedata");
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA
                File.Create("Alone In a Dream_Data/Resources/SaveData.savedata");
#endif
    }


    public void Save(bool ShowIcon=false)
    {
        mUser.Language= TitleController.Instance.mLanguage;
        mUser.BGMVolume= SoundController.Instance.BGMVolume;
        mUser.SEVolume =SoundController.Instance.SEVolume;
        if (ShowIcon)
        {
            Loading.Instance.StartSaving();
        }
#if UNITY_EDITOR
        string location = "Assets/Resources/SaveData.savedata";//유니티 리소시즈 폴더에 저장할 때만.
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WSA
                string location = "Alone In a Dream_Data/Resources/SaveData.savedata";//게임 파일로 빌드했을때
#endif
        BinaryFormatter formatter = new BinaryFormatter();//Binary는 메모리를 검색하는 것 = 뜰채
        MemoryStream stream = new MemoryStream();//stream은 메모리를 통째로 담은 것 = 양동이
        StreamWriter writer = new StreamWriter(location);

        formatter.Serialize(stream, mUser);//mUser를 stream에다가 넣은 것

        string data = Convert.ToBase64String(stream.GetBuffer()); //64비트짜리 데이터 파일로 변환(정확하진 않음)
                                                                  //ToBase64String은 일반적으로는 알 수 없는 문자열로 바꿔주는 것이며, GetBuffer는 담긴 덩어리를 통째로 빼는 것

        writer.Write(data);
        Debug.Log("저장완료");
        writer.Close();
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
