using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    [SerializeField]
    protected SaveData mUser;

    protected void LoadGame()
    {
        string location = Application.streamingAssetsPath + "/SaveData";//여기다가는 마음대로 확장자를 만들어서 붙여도 된다.
        if (File.Exists(location)) //File.Exists(location)
        {
            //파일로 저장하고 싶다면
            StreamReader Reader = new StreamReader(location); //해당하는 경로로 읽어들이기

            //모바일이면 PlayerPrefs를 사용해 저장하면된다. //유니티 게임 내장 저장방식
            //string data = PlayerPrefs.GetString("SaveData");//PlayerPrefs는 윈도우로 치면 레지스트리다.
            string data = Reader.ReadToEnd(); 
            //if (string.IsNullOrEmpty(data))
            //{
            //    CreateNewSaveData();
            //}
            //else
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));//데이터를 다시 불러오기
            //    mUser = (SaveData)formatter.Deserialize(stream); //retrun값이 object이기 때문에 세이브 데이터로 강제 형변환
            //}
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));//데이터를 다시 불러오기
            mUser = (SaveData)formatter.Deserialize(stream); //retrun값이 object이기 때문에 세이브 데이터로 강제 형변환
            FixSaveData();
            Reader.Close();//반드시 Reader와 Write를 사용했을 시 Close 를 해줘야 한다.
        }
        else
        {
            CreateNewSaveData();
        }
    }

    protected void FixSaveData()//세이브 데이터에 들어간 모든 어레이에 대해서 길이 검증
    {
        //패치 시 아이템 추가 등으로 어레이의 길이가 변했을 시 검증함.
        //세이브 데이터를 로드하는 시점에서 세이브 데이터를 갱신(검증) 후 로드해줘야한다.
        if (mUser.HasMaterial == null)
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

        if (mUser.StageOpen == null)
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

        if (mUser.NPCOpen == null)
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
        GameSetting.Instance.Syrup = mUser.Syrup;
        GameSetting.Instance.HasMaterial = mUser.HasMaterial;
        GameSetting.Instance.TutorialEnd = mUser.TutorialEnd;
        GameSetting.Instance.StageOpen = mUser.StageOpen;
        GameSetting.Instance.StagePartsget = mUser.StagePartsget;
        GameSetting.Instance.NPCOpen = mUser.NPCOpen;
        GameSetting.Instance.DonateCount = mUser.DonateCount;
        GameSetting.Instance.TodayWatchFirstAD = mUser.TodayWatchFirstAD;
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
            mUser.StageOpen = new bool[Constants.STAGE_COUNT];
            mUser.StagePartsget = new bool[6];
            mUser.NPCOpen = new bool[Constants.NPC_COUNT];
            mUser.DonateCount = 0;
            mUser.TodayWatchFirstAD = false;
            mUser.CurrentServerTime = 0;//서버시간불러와서 TodayWatchFirstAD 초기화해야함
            mUser.FirstSetting = false;
            mUser.StageOpen[0] = true;//1스테이지 오픈
            mUser.NPCOpen[0] = true; //사서 npc 오픈
            mUser.NPCOpen[4] = true; //유료상인 npc 오픈
            mUser.FirstSetting = true;
            mUser.BGMVolume = Constants.BGM_VOL;
            mUser.SEVolume = Constants.SE_VOL;
        }
    }


    protected void Save()
    {
        string location = Application.streamingAssetsPath + "/SaveData";
        BinaryFormatter formatter = new BinaryFormatter();//Binary는 메모리를 검색하는 것 = 뜰채
        MemoryStream stream = new MemoryStream();//stream은 메모리를 통째로 담은 것 = 양동이
        //파일로 저장하고 싶으면
        StreamWriter writer = new StreamWriter(location);

        formatter.Serialize(stream, mUser);//mUser를 stream에다가 넣은 것

        string data = Convert.ToBase64String(stream.GetBuffer()); //64비트짜리 데이터 파일로 변환(정확하진 않음)
        //ToBase64String은 일반적으로는 알 수 없는 문자열로 바꿔주는 것이며, GetBuffer는 담긴 덩어리를 통째로 빼는 것
        
        //유니티 게임 내장 저장방식
        //PlayerPrefs.SetString("SaveData", data); //SetString에 들어가는 것은 하나도 빠짐 없이 문자열이 일치해야한다.


        writer.Write(data);
        writer.Close();
    }
}
