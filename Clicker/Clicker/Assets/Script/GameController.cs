using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour
{
    private int[] mLevelArr;
    public static GameController Instance;

    [SerializeField]
    private SaveData mUser;


    public Delegates.VoidCallback GoldCallback;//골드를 성공적으로 썼을때만 발동
    public double Gold
    {
        get { return mUser.Gold; } //get을 하지 않아주면 +=같은 수식이 먹히지 않는다.
        set
        {
            if (value >=0)
            {
                mUser.Gold = value;
                //GoldCallback?.Invoke(); //Unity의 Invoke와 Delegate의 Invoke와는 다르다!
                //위와 아래는 같은 코드
                if (GoldCallback!=null)
                {
                    GoldCallback();
                }

            }
            else
            {
                Debug.Log("골드가 부족합니다.");
            }
            GoldCallback = null;
        }
    }

    [SerializeField]
    private double mIncomeWeight = 1.04d; //골드 배수
    private double mIncome;

    [SerializeField]
    private double mProgressWeight = 1.08d;//프로그래스 최댓값 배수
    //소숫점의 기본은 Double이다. 안해도 상관은 없지만 구분을 위해 소숫점 뒤에 d를 붙인다.
    private double mMaxProgress;

    private double mTouchPower;
    public double TouchPower
    {
        get { return mTouchPower; }
        set
        {
            if(value >= 0)
            {
                mTouchPower = value;
            }
            else
            {
                Debug.LogError("Touch Power Update ERROR "+ value);
            }
        }
    }

    public double CriticalRate { get; set; }
    public double CriticalValue { get; set; }

    [SerializeField]//임시(temp)
    private GemPool mGemPool;

    [SerializeField]
    private Gem mCurrentGem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called beforethe first frame update
    void Start()
    {
        ClacStage(mUser.LastGemID);
        mCurrentGem.SetProgress((float)(mUser.Progress / mMaxProgress));
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
        
    }

    private void LoadGame()
    {
        string location = Application.streamingAssetsPath + "/SaveData";//여기다가는 마음대로 확장자를 만들어서 붙여도 된다.
        if (File.Exists(location))
        {
            //파일로 저장하고 싶다면
            //StreamReader Reader = new StreamReader(location); //해당하는 경로로 읽어들이기

            //모바일이면 PlayerPrefs를 사용해 저장하면된다. //유니티 게임 내장 저장방식
            string data = PlayerPrefs.GetString("SaveData"); //Reader.ReadToEnd(); //PlayerPrefs는 윈도우로 치면 레지스트리다.
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
            //Reader.Close();//반드시 Reader와 Write를 사용했을 시 Close 를 해줘야 한다.

        }
        //else
        //{
        //    CreateNewSaveData();
        //}
    }

    private void CreateNewSaveData()
    {
        mUser = new SaveData();
        mUser.Gold = 0;

        mUser.Stage = 0;
        mUser.LastGemID = -1;

        mUser.Progress = 0;

        mUser.mPlayerItemLevelArr = new int[Constants.PLAYER_ITEM_COUNT];
        mUser.mPlayerItemLevelArr[0] = 1;//터치하는 것이기 때문
    }


    private void Save()
    {
        string location = Application.streamingAssetsPath + "/SaveData";
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

    private void OnApplicationQuit()
    {
        //게임이 종료될 때 적용
        Save();
    }


    public int[] GetPlayerItemLevelArr()
    {
        return mUser.mPlayerItemLevelArr;
    }

    private void ClacStage(int id = -1)
    { 
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage);
        //Mathf는 float이기 때문에 return 값이 double인 Using System - Math를 사용한다.
        if (mCurrentGem !=null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        if (mUser.LastGemID <0)
        {
            mUser.LastGemID = UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT);
        }
        mCurrentGem = mGemPool.GetFromPool(mUser.LastGemID);
        mIncome = 5 * Math.Pow(mIncomeWeight, mUser.Stage);

    }

    public void Touch()
    {
        if (mUser.Progress >= mMaxProgress)
        {
            mUser.Gold += mIncome;
            mUser.Stage++;
            mUser.Progress = 0;
            ClacStage();
        }
        else
        {
            mUser.Progress += mTouchPower;
            if (mUser.Progress >mMaxProgress)
            {
                mUser.Progress = mMaxProgress;
            }

            float progress = (float)(mUser.Progress / mMaxProgress);//이미지의 fillamount가 있기 때문에 float을 사용한다
            mCurrentGem.SetProgress(progress);
        }
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

}
