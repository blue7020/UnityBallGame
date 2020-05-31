﻿using System.IO; //입출력 Input, Output
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonGenerator : MonoBehaviour
{

    //public void GeneratePlayerStatTextInfo()
    //{
    //    PlayerStatText[] infoArr = Player.Instance.GetTextInfoArr();
    //    string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
    //    WriteFile(data, "PlayerStatText.json"); //파일을 읽을 때는 확장자를 꼭 넣어야 인식이 된다.
    //}

    //메서드를 문자열로 바꾸기
    public void GeneratePlayerStatInfo()
    {
        PlayerStat[] infoArr = new PlayerStat[3];
        //스피나
        infoArr[0] = new PlayerStat();
        infoArr[0].Gold = 0;
        infoArr[0].ID = 0;

        infoArr[0].Hp = 5;
        infoArr[0].Atk = 1;
        infoArr[0].Def = 0;
        infoArr[0].AtkSpd = 1;
        infoArr[0].Spd = 10;

        infoArr[0].IsPercent = true;
        infoArr[0].Crit = 15;
        infoArr[0].CritDamage = 30;
        infoArr[0].CooltimeReduce = 0;
        infoArr[0].CCReduce = 0;
        infoArr[0].Damage = 1;

        infoArr[0].Skill1_Cooltime = 1;
        infoArr[0].Skill1_Duration = 0;
        infoArr[0].Skill2_Cooltime = 15;
        infoArr[0].Skill2_Duration = 5;

        //이앙카
        infoArr[1] = new PlayerStat();
        infoArr[1].Gold = 0;
        infoArr[1].ID = 1;

        infoArr[1].Hp = 5;
        infoArr[1].Atk = 1;
        infoArr[1].Def = 0;
        infoArr[1].AtkSpd = 1.3f;
        infoArr[1].Spd = 8;

        infoArr[1].IsPercent = true;
        infoArr[1].Crit = 15;
        infoArr[1].CritDamage = 30;
        infoArr[1].CooltimeReduce = 0;
        infoArr[1].CCReduce = 0;
        infoArr[1].Damage = 1;

        infoArr[1].Skill1_Cooltime = 1;
        infoArr[1].Skill1_Duration = 0;
        infoArr[1].Skill2_Cooltime = 15;
        infoArr[1].Skill2_Duration = 5;
        //카샤
        infoArr[2] = new PlayerStat();
        infoArr[2].Gold = 0;
        infoArr[2].ID = 2;

        infoArr[2].Hp = 5;
        infoArr[2].Atk = 1;
        infoArr[2].Def = 0;
        infoArr[2].AtkSpd = 1;
        infoArr[2].Spd = 8;

        infoArr[2].IsPercent = true;
        infoArr[2].Crit = 20;
        infoArr[2].CritDamage = 30;
        infoArr[2].CooltimeReduce = 0;
        infoArr[2].CCReduce = 0;
        infoArr[2].Damage = 1;

        infoArr[2].Skill1_Cooltime = 1;
        infoArr[2].Skill1_Duration = 0;
        infoArr[2].Skill2_Cooltime = 15;
        infoArr[2].Skill2_Duration = 5;




        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "PlayerStat.json"); //파일을 읽을 때는 확장자를 꼭 넣어야 인식이 된다.
    }

    public void GenerateMonsterStatInfo()
    {
        MonsterStat[] infoArr = new MonsterStat[3];
        //미믹
        infoArr[0] = new MonsterStat();
        infoArr[0].ID = 0;
        infoArr[0].Gold = 10;
        

        infoArr[0].Hp = 5;
        infoArr[0].Atk = 1;
        infoArr[0].AtkSpd = 2;
        infoArr[0].Spd = 3;

        //슬라임
        infoArr[1] = new MonsterStat();
        infoArr[1].ID = 1;
        infoArr[1].Gold = 5;


        infoArr[1].Hp = 5;
        infoArr[1].Atk = 1;
        infoArr[1].AtkSpd = 2;
        infoArr[1].Spd = 3;

        //고스트
        infoArr[2] = new MonsterStat();
        infoArr[2].ID = 1;
        infoArr[2].Gold = 20;


        infoArr[2].Hp = 3;
        infoArr[2].Atk = 1;
        infoArr[2].AtkSpd = 4;
        infoArr[2].Spd = 3;



        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "MonsterStat.json"); //파일을 읽을 때는 확장자를 꼭 넣어야 인식이 된다.
    }

    public void GenerateItemStatInfo()
    {
        ItemStat[] infoArr = new ItemStat[2];
        infoArr[0] = new ItemStat();
        infoArr[0].ID = 0;
        infoArr[0].Price = 5;

        infoArr[0].Heal = 1;
        infoArr[0].Atk = 0;
        infoArr[0].Def = 0;
        infoArr[0].AtkSpd = 0;
        infoArr[0].Spd = 0;

        infoArr[0].Damage = 0;
        infoArr[0].Duration = 0;

        infoArr[1] = new ItemStat();
        infoArr[1].ID = 1;
        infoArr[1].Price = 10;

        infoArr[1].Heal = 2;
        infoArr[1].Atk = 0;
        infoArr[1].Def = 0;
        infoArr[1].AtkSpd = 0;
        infoArr[1].Spd = 0;

        infoArr[1].Damage = 0;
        infoArr[1].Duration = 0;

        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "ItemStat.json"); //파일을 읽을 때는 확장자를 꼭 넣어야 인식이 된다.
    }

    public void GenerateItemTextStatInfo()
    {
        ItemTextStat[] infoArr = new ItemTextStat[2];
        infoArr[0] = new ItemTextStat();
        infoArr[0].ID = 0;

        infoArr[0].Title = "";
        infoArr[0].ContensFormat = "";

        infoArr[1] = new ItemTextStat();
        infoArr[1].ID = 1;

        infoArr[1].Title = "";
        infoArr[1].ContensFormat = "";

        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "ItemTextStat.json"); //파일을 읽을 때는 확장자를 꼭 넣어야 인식이 된다.
    }

    public void GenerateArtifactStatInfo()
    {
        ArtifactStat[] infoArr = new ArtifactStat[2];
        infoArr[0] = new ArtifactStat();
        infoArr[0].ID = 0;
        infoArr[0].Price = 5;

        infoArr[0].Atk = 5;
        infoArr[0].Def = 0;
        infoArr[0].AtkSpd = 0;
        infoArr[0].Spd = 0;

        infoArr[0].IsPercent = true;
        infoArr[0].Crit = 0;
        infoArr[0].CritDamage = 0;
        infoArr[0].CooltimeReduce = 0;
        infoArr[0].CCReduce = 0;

        infoArr[0].Skill_Cooltime = 0;
        infoArr[0].Skill_Duration = 0;


        infoArr[1] = new ArtifactStat();
        infoArr[1].ID = 1;
        infoArr[1].Price = 5;

        infoArr[1].Atk = 0;
        infoArr[1].Def = 1;
        infoArr[1].AtkSpd = 0;
        infoArr[1].Spd = 0;

        infoArr[1].IsPercent = true;
        infoArr[1].Crit = 0;
        infoArr[1].CritDamage = 0;
        infoArr[1].CooltimeReduce = 0;
        infoArr[1].CCReduce = 0;

        infoArr[1].Skill_Cooltime = 0;
        infoArr[1].Skill_Duration = 0;

        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "ArtifactStat.json"); //파일을 읽을 때는 확장자를 꼭 넣어야 인식이 된다.
    }

    public void GenerateArtifactTextStatInfo()
    {
        ArtifactTextStat[] infoArr = new ArtifactTextStat[2];
        infoArr[0] = new ArtifactTextStat();
        infoArr[0].ID = 0;

        infoArr[0].Title = "";
        infoArr[0].ContensFormat = "";

        infoArr[0].Flavortext = "";

        infoArr[1] = new ArtifactTextStat();
        infoArr[1].ID = 1;

        infoArr[1].Title = "";
        infoArr[1].ContensFormat = "";

        infoArr[1].Flavortext = "";

        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "ArtifactTextStat.json");
    }

    private void WriteFile(string data, string fileName)
    {
        //PC에서만 사용
        //세이브 파일 경로 생성
        string fileLocation = string.Concat(Application.dataPath, "/", fileName); //파일 데이터를 에디터가 명령하여 만드니까 dataPath를 사용하는 것임.

        //읽고 싶으면 StreamReader 사용
        StreamWriter writer = new StreamWriter(fileLocation); //없으면 생성, 있으면 덮어쓰기
                                                              //StreamWriter는 세이브 할 때 사용한다.

        writer.Write(data);
        writer.Close(); //파일을 열고 사용했으니 꺼줘야 한다. 그렇지 않으면 저장이 안된다.
    }
}
