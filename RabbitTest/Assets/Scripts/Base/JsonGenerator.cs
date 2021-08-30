using System.IO; //입출력 Input, Output
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class JsonGenerator : MonoBehaviour
{
    public void GenerateStatInfo()
    {
        GuideText[] infoArr = new GuideText[3];

        infoArr[0] = new GuideText();
        infoArr[0].ID = 0;
        infoArr[0].title_kor = "0";
        infoArr[0].title_eng = "1";
        infoArr[0].text_kor = "2";
        infoArr[0].text_eng = "3";
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);//출력 시 보기 편하게 변환
        WriteFile(data, "GuideText.json");//json 파일을 열었을 때 한글이 깨지면 메모장으로 켜서 다른이름으로 저장에 파일형식 UTF-8로 바꾸고 덮어쓰기
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
