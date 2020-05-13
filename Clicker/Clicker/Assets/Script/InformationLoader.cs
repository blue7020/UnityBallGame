using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;//Unity의 내장 json은 enum을 못 넣기 때문에 Assets의 Plugin에 들어간 파일을 넣어줘야한다.

public class InformationLoader : MonoBehaviour
{
    //파일 로드 시키기
    protected void LoadJson<T>(out T[] dataArr, string fileLocation) //Json 파일을 Array 형식으로 넣어준다.
    {
        TextAsset dataAsset = Resources.Load<TextAsset>(fileLocation);
        string data = dataAsset.text;
        if (string.IsNullOrEmpty(data))//성공적로 불러오지 못했을 때
        {
            //TODO use Popup
            Debug.LogError("Empty string in " + fileLocation);
        }
        dataArr = JsonConvert.DeserializeObject<T[]>(data);
    }
}
