using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    private void Start()
    {
        LoadCSV("CsvFiles/StoryPage");
    }

    public void LoadCSV(string path)
    {
        //운영체제마다 줄바꿈 형식이 다르기 때문에 맞춰준다. 
        const string WINDOWS_DELIMITER = "\n";
        const string UNIX_DELEIMITER = "\r\n";
        const string MAC_DELIMITER = "\r";



        //null일수도 있으니 Null체크를 꼭 해봐야 한다!
        TextAsset asset= Resources.Load<TextAsset>(path);
        if (asset ==null)
        {
            Debug.LogError("wrong path: "+path);
            return;
        }
        string data = asset.text;
        Debug.Log(data);

        if (data.Contains(UNIX_DELEIMITER))
        {
            data = data.Replace(UNIX_DELEIMITER, WINDOWS_DELIMITER);//Replace는 전체 변경이다. 1번 파라미터를 2번 파라미터로 바꾼다.
            //참고로 안드로이는 리눅스이다
        }
        else if(data.Contains(MAC_DELIMITER))
        {
            data = data.Replace(MAC_DELIMITER, WINDOWS_DELIMITER);
        }

        string[] lineData = data.Split('\n'); // \가 이스케이프 시퀀스기 때문에 n만 문자로 취급되므로 ''인 character로 저장한다.1       
        for(int i =0; i<lineData.Length;i++)
        {
            Debug.Log(lineData[i]);
        }

        for (int i=1;i<lineData.Length-1;i++)
        {
            Debug.Log("============Line=============");
            string currentLine = lineData[i];
            string[] currentLineSplited;
            char[] MarkArr = { '\"', '[', ']', '{', '}' };
            //{ "", "[, ]", "{, }" }

            

            Debug.Log(currentLine);
            
            if (currentLine.IndexOfAny(MarkArr)>=0)
            {
                currentLine = currentLine.Replace("\"\"", "\"");
                currentLine = currentLine.Replace("\"\"", "\"");
                currentLine = currentLine.Replace("\"[", "[");
                currentLine = currentLine.Replace("]\"", "]");
                currentLine = currentLine.Replace("\"{", "{");
                currentLine = currentLine.Replace("}\"", "}");

                List<string> result = new List<string>();

                int startIndex = 0;
                //쉼표 위치
                int commaIndex = currentLine.IndexOf(',', startIndex); //IndexOf = 문자열의 몇 번째에 있느냐
                int markIndex = currentLine.IndexOfAny(MarkArr, startIndex);
                bool isQuot, isCurly, isSquare; //쌍따옴표 Quot 대괄호 Square 중괄호 Curly

                Debug.Log(currentLine[startIndex] + "/" + startIndex);
                Debug.Log(currentLine[commaIndex] + "/" + commaIndex);
                Debug.Log(currentLine[markIndex] + "/" + markIndex);
                Debug.Log(currentLine.Substring(startIndex, commaIndex));
                while (true)
                {
                    //구분 시켜야 할 문자열 덩어리 판단
                    if (commaIndex <markIndex)
                    {
                        string block = currentLine.Substring(startIndex, markIndex - startIndex);
                        Debug.Log(block);
                        result.Add(block);
                    }
                    else
                    {
                        startIndex = markIndex;
                        isQuot = currentLine[markIndex].Equals(MarkArr[0]);//꼭 할 필요는 없지만 이해를 위해서
                        isCurly = currentLine[markIndex].Equals(MarkArr[3]);
                        isSquare = currentLine[markIndex].Equals(MarkArr[1]);

                        if (isSquare)
                        {
                            markIndex = currentLine.IndexOf(MarkArr[2], startIndex);
                        }
                        else if (isCurly)
                        {
                            markIndex = currentLine.IndexOf(MarkArr[4], startIndex);
                        }
                        else
                        {
                            markIndex = currentLine.IndexOf(MarkArr[0], startIndex);
                            while (markIndex < currentLine.Length - 1 && !currentLine[markIndex + 1].Equals(','))
                            {
                                markIndex = currentLine.IndexOf(MarkArr[0], markIndex + 1);
                            }
                        }
                        string block = currentLine.Substring(startIndex, markIndex - startIndex);
                        Debug.Log(block);
                        result.Add(block);
                    }

                    startIndex = commaIndex + 1;
                    commaIndex = currentLine.IndexOf(',', startIndex);
                }


                currentLineSplited = result.ToArray();
            }
            else
            {
                currentLineSplited = currentLine.Split(',');
            }

            Debug.Log("=========================");
        }
    }

    
}
