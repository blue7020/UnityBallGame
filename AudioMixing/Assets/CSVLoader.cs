using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVLoader : MonoBehaviour
{
    [SerializeField]
    StoryText[] InfoArr;
    private void Start()
    {
        LoadCSV(out InfoArr, "CsvFiles/StoryPage");
    }

    public void LoadCSV<T>(out T[] OutputArr, string path) where T : new() //new 키워드를 통해 만들어질 수 있는 애들(==데이터 클래스들 등)만 타입으로 넣을 수 있다 라는 뜻
    {
        //null일수도 있으니 Null체크를 꼭 해봐야 한다!
        TextAsset asset = Resources.Load<TextAsset>(path);
        if (asset == null)
        {
            Debug.LogError("wrong path: " + path);
            OutputArr = null;
            return;
        }
        string data = asset.text;
        Debug.Log(data);

        #region Split Line by Line
        //운영체제마다 줄바꿈 형식이 다르기 때문에 맞춰준다. 
        const string WINDOWS_DELIMITER = "\n";
        const string UNIX_DELEIMITER = "\r\n";
        const string MAC_DELIMITER = "\r";

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
        #endregion

        OutputArr = new T[lineData.Length - 2];
        string[] fieldNameArr = lineData[0].Split(',');

        for (int i=1;i<lineData.Length-2;i++)
        {
            string[] currentLineSplited = GenerateLineSplit(lineData[i+1]);
            OutputArr[i] = new T();
            Type type = typeof(T);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            int fieldID = 0;

            for (int j = 0; j < fields.Length; j++)
            {
                Type propertyType = fields[i].FieldType;
                if (fieldNameArr.Length > j && fields[i].Name==fieldNameArr[j])
                {
                    fieldID = j;
                }
                else
                {
                    bool skipFlag = true;
                    for (int k = 0; k < fieldNameArr.Length; k++)
                    {
                        if (fields[j].Name == fieldNameArr[k])
                        {
                            fieldID = k;
                            skipFlag = false;
                            break;
                        }
                    }
                    if (skipFlag)
                    {
                        continue;
                    }
                }
                if (propertyType.IsArray)
                {
                    string[] strList = GenerateLineSplit(currentLineSplited[fieldID]);
                    Type arrayFieldType = propertyType.GetElementType();

                    Array newArray = Array.CreateInstance(arrayFieldType, strList.Length);

                    if (strList !=null)
                    {
                        newArray = Array.CreateInstance(arrayFieldType, strList.Length);

                        for (int k=0; k<newArray.Length;k++)
                        {
                            if (arrayFieldType.IsGenericType && arrayFieldType.GetGenericTypeDefinition() ==typeof(List<>))
                            {
                                string[] listArr = GenerateLineSplit(strList[k]);
                                Type baseType = typeof(List<>);
                                Type itemType = arrayFieldType.GetGenericArguments()[0];
                                IList list = (IList)Activator.CreateInstance(baseType.MakeGenericType(arrayFieldType.GetGenericArguments()[0]));

                                for(int l=0; l<strList.Length; l++)
                                {
                                    if (itemType.IsEnum)
                                    {
                                        list.Insert(l, Enum.Parse(itemType, strList[l]));
                                    }
                                    else if (itemType ==typeof(int))
                                    {
                                        list.Insert(l, int.Parse(strList[l]));
                                    }
                                    else if (itemType == typeof(float))
                                    {
                                        list.Insert(l, float.Parse(strList[l]));
                                    }
                                    else if (itemType == typeof(bool))
                                    {
                                        list.Insert(l, bool.Parse(strList[l]));
                                    }
                                    else if (itemType == typeof(double))
                                    {
                                        list.Insert(l, double.Parse(strList[l]));
                                    }
                                    else if (itemType == typeof(string))
                                    {
                                        list.Insert(l, strList[l].Replace("\\n", "\n"));
                                    }
                                    else
                                    {
                                        list.Insert(l, null);
                                    }
                                    newArray.SetValue(list, k);
                                }
                            }
                            else if (arrayFieldType.IsEnum)
                            {
                                newArray.SetValue(Enum.Parse(arrayFieldType, strList[k]), k);
                            }
                            else if (arrayFieldType ==typeof(int))
                            {
                                newArray.SetValue(int.Parse(strList[k]),k);
                            }
                            else if (arrayFieldType == typeof(float))
                            {
                                newArray.SetValue(float.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(bool))
                            {
                                newArray.SetValue(bool.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(double))
                            {
                                newArray.SetValue(double.Parse(strList[k]), k);
                            }
                            else if (arrayFieldType == typeof(string))
                            {
                                newArray.SetValue(strList[k].Replace("\\n","\n"), k);
                            }
                            else
                            {
                                newArray.SetValue(null, k);
                            }
                        }
                    }
                }
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<T>))
                {
                    string[] strList = GenerateLineSplit(currentLineSplited[fieldID]);
                    Type baseType = typeof(List<>);
                    Type itemType = propertyType.GetGenericArguments()[0];

                    IList list = (IList)Activator.CreateInstance(baseType.MakeGenericType(propertyType.GetGenericArguments()[0]));

                    for (int k = 0; k < strList.Length; k++)
                    {
                        if (itemType.IsEnum)
                        {
                            list.Insert(k, Enum.Parse(itemType, strList[k]));
                        }
                        else if (itemType == typeof(int))
                        {
                            list.Insert(k, int.Parse(strList[k]));
                        }
                        else if (itemType == typeof(float))
                        {
                            list.Insert(k, float.Parse(strList[k]));
                        }
                        else if (itemType == typeof(bool))
                        {
                            list.Insert(k, bool.Parse(strList[k]));
                        }
                        else if (itemType == typeof(double))
                        {
                            list.Insert(k, double.Parse(strList[k]));
                        }
                        else if (itemType == typeof(string))
                        {
                            //대화를 하다가 줄바꿈을 할 때 엑셀에 \n을 넣어져 있으면 \\n으로 나오기 때문에 \n으로 바꿔주는 작업을 한다
                            list.Insert(k, strList[k].Replace("\\n", "\n"));
                        }
                        else
                        {
                            list.Insert(k, null);
                        }
                    }
                }
                else if (propertyType.IsEnum)
                {
                    fields[j].SetValue(OutputArr[i], Enum.Parse(propertyType, currentLineSplited[fieldID]));
                }
                //일반 타입
                else if (propertyType==typeof(int)|| propertyType == typeof(float)|| propertyType == typeof(bool)|| propertyType == typeof(double))
                {
                    fields[j].SetValue(OutputArr[i], Convert.ChangeType(currentLineSplited[fieldID], propertyType));
                }
                //문자열
                else
                {
                    //빈 문자열을 넣고 싶다면 쌍따옴표 없이 해당 엑셀 인덱스에 NONE을 입력하면 된다.
                    if(currentLineSplited[fieldID].Equals("NONE"))
                    {
                        currentLineSplited[fieldID] = string.Empty;
                    }
                    fields[j].SetValue(OutputArr[i], currentLineSplited[fieldID].Replace("\\n", "\n"));
                }
            }
            
        }
    }

    private string[] GenerateLineSplit(string currentLine)
    {
        Debug.Log("============Line=============");
        string[] currentLineSplited;
        char[] MarkArr = { '\"', '[', ']', '{', '}' };
        //{ "", "[, ]", "{, }" }

        if (currentLine.IndexOfAny(MarkArr) >= 0)
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


            while (true)
            {
                Debug.LogFormat("start {0}", startIndex);
                Debug.LogFormat("comma {0}", commaIndex);
                Debug.LogFormat("mark {0}", markIndex);

                //구분 시켜야 할 문자열 덩어리 판단
                if (commaIndex < markIndex && startIndex < commaIndex)
                {
                    string block = currentLine.Substring(startIndex, commaIndex - startIndex);
                    Debug.Log(block);
                    result.Add(block);
                    startIndex = commaIndex + 1;

                }
                else
                {
                    startIndex = markIndex;

                    if (markIndex < 0)
                    {
                        break;
                    }
                    isQuot = currentLine[markIndex].Equals(MarkArr[0]);//꼭 할 필요는 없지만 이해를 위해서
                    isCurly = currentLine[markIndex].Equals(MarkArr[3]);
                    isSquare = currentLine[markIndex].Equals(MarkArr[1]);

                    if (isSquare)
                    {
                        markIndex = currentLine.IndexOf(MarkArr[2], startIndex);
                    }
                    if (isCurly)
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
                    string block = currentLine.Substring(startIndex, markIndex + 1 - startIndex);
                    //remove를 하려면 꼭 대입을 해줘야 한다.
                    block = block.Remove(block.Length - 1, 1);
                    block = block.Remove(0, 1);
                    Debug.Log(block);
                    result.Add(block);
                    startIndex = markIndex + 1;

                    markIndex = currentLine.IndexOfAny(MarkArr, startIndex);
                }
                commaIndex = currentLine.IndexOf(',', startIndex);
            }
            currentLineSplited = result.ToArray();
        }
        else
        {
            currentLineSplited = currentLine.Split(',');
        }

        Debug.Log("=========================");

        return currentLineSplited;
    }

    
}
