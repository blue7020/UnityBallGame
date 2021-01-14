﻿using System;
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
        else if (mUser.StageShowEvent.Length != Constants.STAGE_COUNT) // != 혹은 < 를 사용해 둘 중에 짧은 배열에 긴 배열을 덮어씌운다.
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
        else if (mUser.StageShow.Length != Constants.STAGE_COUNT) // != 혹은 < 를 사용해 둘 중에 짧은 배열에 긴 배열을 덮어씌운다.
        {
            bool[] temp = new bool[Constants.STAGE_COUNT];
            int count = Mathf.Min(Constants.STAGE_COUNT, mUser.StageShow.Length);
            for (int i = 0; i < count; i++)
            {
                temp[i] = mUser.StageShow[i];
            }
            mUser.StageShow = temp;
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
        if (DataDelete==false)
        {
            Save();
        }
    }
}