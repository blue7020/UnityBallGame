using System.IO; //입출력 Input, Output
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonGenerator : MonoBehaviour
{
    public void GenerateStatInfo()
    {
        CodeStat[] infoArr = new CodeStat[1];

        infoArr[0] = new CodeStat();
        infoArr[0].Code = "";
        infoArr[0].CharacterID = -1;
        infoArr[0].SkillID = -1;
        infoArr[0].WeaponID = -1;
        infoArr[0].ItemID = -1;
        //for (int i=0; i<Constants.MATERIAL_COUNT;i++)
        //{
        //    infoArr[0].MaterialID[i] = -1;
        //    infoArr[0].MaterialAmount[i] = 0;
        //}
        infoArr[0].SyrupAmount = 0;
        infoArr[0].IsUse = false;
        infoArr[0].IsExpiration = false;
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "CodeStat.json");
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
